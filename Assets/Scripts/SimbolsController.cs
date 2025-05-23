using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SimbolsController : MonoBehaviour
{
    static float[] probabilities = {1f, 3f, 35f, 15f, 15f, 10f, 21f};
    private string[,] reelsSymbols = new string[3, 3];
    private int columnN =0;
    // 7,7,pokeballs,pikachu,lotad,marill,replay
    private String symbol;
    private UIController ui;
    private AnimationController anim;
    static int selectedSymbolIndex;
    void Start()
    {
        GameObject obj = GameObject.Find("Controller");
        ui = obj.GetComponent<UIController>();
        anim = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    public static int RandSimbol()
    {
        float randomValue = UnityEngine.Random.Range(0f, 100f);
        float cumulative = 0f;
         for (int i = 0; i < probabilities.Length; i++)
        {
            cumulative += probabilities[i];
            if (randomValue < cumulative)
            {
                selectedSymbolIndex = i;
                return selectedSymbolIndex;
            }
        }
        return 0;
    }

    public void SavePosition(List<string> symbols)
    {
        for (int row = 0; row < 3; row++)
        {
            reelsSymbols[columnN, row] = symbols[row];
        }
        columnN += 1;
        if (columnN == 3)
        {
            CheckSymbol(reelsSymbols);
            reelsSymbols = new string[3, 3];
            columnN = 0;
        }
    }

    private void CheckSymbol(string[,] reelsSymbols)
    {
        float valueCoin = 0;
        // Verificar líneas horizontales
        valueCoin += CheckLine(reelsSymbols[0, 0], reelsSymbols[1, 0], reelsSymbols[2, 0]);
        valueCoin += CheckLine(reelsSymbols[0, 1], reelsSymbols[1, 1], reelsSymbols[2, 1]);
        valueCoin += CheckLine(reelsSymbols[0, 2], reelsSymbols[1, 2], reelsSymbols[2, 2]);
        // Diagonales
        valueCoin += CheckLine(reelsSymbols[0, 0], reelsSymbols[1, 1], reelsSymbols[2, 2]);  
        valueCoin += CheckLine(reelsSymbols[0, 2], reelsSymbols[1, 1], reelsSymbols[2, 0]);
        // Horizontal 2 
        valueCoin += CheckS03Symbol(reelsSymbols[0, 0], reelsSymbols[1, 0], reelsSymbols[1, 1]);
        valueCoin += CheckS03Symbol(reelsSymbols[0, 1], reelsSymbols[1, 1], null);
        valueCoin += CheckS03Symbol(reelsSymbols[0, 2], reelsSymbols[1, 2], reelsSymbols[1, 1]);

        int money = (int)valueCoin;
        // Añadir una funcion que use valueCoin para mostrarlo en pantalla, en otro archivo
        if (money >= 6)
        {
            anim.OnWin();
        }
        else
        {
            anim.OnLose();
        }
        ui.PayoutCheck(money);
        
    } 

    private float CheckLine(string symbol1, string symbol2, string symbol3)
    {   
        if (symbol1 == symbol2)
        {
            if (symbol2 == symbol3)
            {
                if(symbol3 == "S03")
                {
                    return CheckPrizes("S03Triple");
                }
                else
                {
                    symbol = symbol1;
                    return CheckPrizes(symbol);
                }
            }
            else if (IsMix7(symbol1, symbol3))
            {
                return CheckPrizes("Mix7");
            }
            
            else if (symbol1 == "S03" && symbol2 == "S03" && symbol3 != "S03")
            {
                return CheckPrizes("S03Double");
            }
            
        }
        return 0;
    }   

    private bool IsMix7(string symbol1, string symbol3)
    {
        return (symbol1 == "S01" && symbol3 == "S02") || (symbol1 == "S02" && symbol3 == "S01");
    }

    private float CheckS03Symbol(string symbol1, string symbol2, string symbol3)
    {
        if (symbol1 == "S03" && (symbol2 != "S03" && symbol3 != "S03"))
        {
            return CheckPrizes("S03");
        }
        return 0;
    }
    private float CheckPrizes(String symbol)
    {
        switch (symbol)
        {
            case "S01":
                return 300;
            case "S02":
                return 200;

            case "Mix7":
                return 90;

            case "S03":
                return 1;

            case "S03Double":
                return 4;
            
            case "S03Triple":
                return 6;
            
            case "S04":
                return 3; // "3 coin power" no está claro; se puede aclarar más.

            case "S05":
                return 6;

            case "S06":
                return 12;

            case "SRe":
                // Al obtener el símbolo "SRe", se activa una función de repetición (Replay) No está implementada.
                return 0.1f;

            default:
                Debug.Log("Símbolo desconocido");
                return 0;
        }
    }

    
}
