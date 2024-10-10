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
            ChechSimbol(reelsSymbols);
        }
    }

    private void ChechSimbol(string[,] reelsSymbols)
    {
        if (reelsSymbols[0,0] == reelsSymbols[1,0])
        {
            if (reelsSymbols[1,0] == reelsSymbols[2,0] && reelsSymbols[2,0] != "SO3" )
            {
                symbol = reelsSymbols[0,0];
            }
            else if ((reelsSymbols[0,0] == "S01" && reelsSymbols[2,0] == "S02") || (reelsSymbols[0,0] == "S02" && reelsSymbols[2,0] == "S01") )
            {
                symbol = "Mix7";
            }
            else if (reelsSymbols[1,0] == "S03")
            {
                symbol = "S03Double";
            } 
        }

        if (reelsSymbols[0, 2] == reelsSymbols[1, 2])
        {
            if (reelsSymbols[1, 2] == reelsSymbols[2, 2] && reelsSymbols[2, 2] != "S03")
            {
                symbol = reelsSymbols[0, 2];
            }
            else if ((reelsSymbols[0, 2] == "S01" && reelsSymbols[2, 2] == "S02") || 
                    (reelsSymbols[0, 2] == "S02" && reelsSymbols[2, 2] == "S01"))
            {
                symbol = "Mix7";
            }
            else if (reelsSymbols[1, 2] == "S03")
            {
                symbol = "S03Double";
            }
        }

        if (reelsSymbols[0, 0] == reelsSymbols[1, 1])
        {
            if (reelsSymbols[1, 1] == reelsSymbols[2, 2] && reelsSymbols[2, 2] != "S03")
            {
                symbol = reelsSymbols[0, 0];
            }
            else if ((reelsSymbols[0, 0] == "S01" && reelsSymbols[2, 2] == "S02") || 
                    (reelsSymbols[0, 0] == "S02" && reelsSymbols[2, 2] == "S01"))
            {
                symbol = "Mix7";
            }
            else if (reelsSymbols[1, 1] == "S03")
            {
                symbol = "S03Double";
            }
        }

        if (reelsSymbols[0, 2] == reelsSymbols[1, 1])
        {
            if (reelsSymbols[1, 1] == reelsSymbols[2, 0] && reelsSymbols[2, 0] != "S03")
            {
                symbol = reelsSymbols[0, 2];
            }
            else if ((reelsSymbols[0, 2] == "S01" && reelsSymbols[2, 0] == "S02") || 
                    (reelsSymbols[0, 2] == "S02" && reelsSymbols[2, 0] == "S01"))
            {
                symbol = "Mix7";
            }
            else if (reelsSymbols[1, 1] == "S03")
            {
                symbol = "S03Double";
            }
        }
        //S03 check
        if (reelsSymbols[0, 0] == "S03")
        {
            symbol = "S03";
        }
        if (reelsSymbols[0, 1] == "S03")
        {
            symbol = "S03";
        }
        if (reelsSymbols[0, 2] == "S03")
        {
            symbol = "S03";
        }
                
    }

    private void CheckPrizes(String symbol)
    {
        switch (symbol)
        {
            case "S01":
                // Al obtener el símbolo "S01", se otorgan 300 puntos.
                break;

            case "S02":
                // Al obtener el símbolo "S02", se otorgan 200 puntos.
                break;

            case "Mix7":
                // Al obtener una combinación de "S01" y "S02", se otorgan 90 puntos.
                break;

            case "S03":
                // Al obtener el símbolo "S03", se otorgan 2 puntos.
                break;

            case "S03Double":
                // Al obtener el símbolo "S03" en una combinación doble, se otorgan 4 puntos.
                break;

            case "S04":
                // Al obtener el símbolo "S04", se otorgan 3 puntos.
                break; // Comentario sobre "3 coin porwe" no está claro; se puede aclarar más.

            case "S05":
                // Al obtener el símbolo "S05", se otorgan 6 puntos.
                break;

            case "S06":
                // Al obtener el símbolo "S06", se otorgan 12 puntos.
                break;

            case "SRe":
                // Al obtener el símbolo "SRe", se activa una función de repetición (Replay).
                break;

            default:
                // Si el símbolo no coincide con ninguno de los casos anteriores, se muestra un mensaje de error.
                Debug.Log("Símbolo desconocido");
                break;
        }
    }
}
