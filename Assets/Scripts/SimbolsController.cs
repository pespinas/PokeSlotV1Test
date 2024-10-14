using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SimbolsController : MonoBehaviour
{
    static float[] probabilities = {1f, 2f, 20f, 10f, 10f, 5f, 30f};
    private string[,] reelsSymbols = new string[3, 3];
    private int columnN =0;
    // 7,7,pokeballs,pikachu,lotad,marill,replay
    private String symbol;

    static int selectedSymbolIndex;
    void Start()
    {
        
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
        columnN += 1;
        for (int row = 0; row < 3; row++)
        {
            reelsSymbols[columnN, row] = symbols.ToString();
        }
        if (columnN == 3)
        {
            CheckSymbol(reelsSymbols);
        }
    }

    private void CheckSymbol(string[,] reelsSymbols)
    {
        float valueCoin = 0;
        // Verificar líneas verticales y diagonales
        valueCoin += CheckLine(reelsSymbols[0, 0], reelsSymbols[1, 0], reelsSymbols[2, 0]);  
        valueCoin += CheckLine(reelsSymbols[0, 2], reelsSymbols[1, 2], reelsSymbols[2, 2]);  
        valueCoin += CheckLine(reelsSymbols[0, 0], reelsSymbols[1, 1], reelsSymbols[2, 2]);  
        valueCoin += CheckLine(reelsSymbols[0, 2], reelsSymbols[1, 1], reelsSymbols[2, 0]);

        // Verificar si S03 aparece en alguna de las posiciones clave
        valueCoin += CheckS03Symbol(reelsSymbols[0, 0]);
        valueCoin += CheckS03Symbol(reelsSymbols[0, 1]);
        valueCoin += CheckS03Symbol(reelsSymbols[0, 2]);

        // Añadir una funcion que use valueCoin para mostrarlo en pantalla, en otro archivo

    } 

    private float CheckLine(string symbol1, string symbol2, string symbol3)
    {   
        if (symbol1 == symbol2)
        {
            if (symbol2 == symbol3 && symbol3 != "S03")
            {
                symbol = symbol1;
                return CheckPrizes(symbol);
            }
            else if (IsMix7(symbol1, symbol3))
            {
                return CheckPrizes("Mix7");
            }
            else if (symbol2 == "S03")
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

    private float CheckS03Symbol(string symbol)
    {
        if (symbol == "S03")
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
                return 2;

            case "S03Double":
                return 4;

            case "S04":
                return 3; // "3 coin power" no está claro; se puede aclarar más.

            case "S05":
                return 6;

            case "S06":
                return 12;

            case "SRe":
                // Al obtener el símbolo "SRe", se activa una función de repetición (Replay).
                return 0.1f;

            default:
                Debug.Log("Símbolo desconocido");
                return 0;
        }
    }
}
