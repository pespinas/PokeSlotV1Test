using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject panelCredit;    
    public GameObject panelPayout;
    public GameObject panelLabel;
    private GameObject[] pCredit;
    private GameObject[] pPayout;
    private GameObject[] labelResult;
    private SpriteRenderer spriteRenderer;
    public int Dineros;
    private string pathNumbers = "Sprites/";
    private string[] imgNumbers= {"N0","N1","N2","N3","N4","N5","N6","N7","N8","N9"};

    // Start is called before the first frame update
    void Start()
    {
        int childCountCredit = panelCredit.transform.childCount;
        pCredit = new GameObject[childCountCredit];
        for (int i = 0; i < childCountCredit; i++)
        {
            pCredit[i] = panelCredit.transform.GetChild(i).gameObject;
        }
        int childCountPayout = panelPayout.transform.childCount;
        pPayout = new GameObject[childCountPayout];
        for (int i = 0; i < childCountPayout; i++)
        {
            pPayout[i] = panelPayout.transform.GetChild(i).gameObject;
        }
        CreditStart(Dineros);
    }

    // Update is called once per frame
    void CreditStart(int Dinero)
    {   
        string numberAsString = Dinero.ToString();
        int longCredit = numberAsString.Length;
        if (numberAsString != "0")
        {
            foreach (char digit in numberAsString)
            {
                longCredit--;
                int separatedDigit = int.Parse(digit.ToString());
                spriteRenderer = pCredit[longCredit].GetComponent<SpriteRenderer>();
                Sprite newSprite = Resources.Load<Sprite>(pathNumbers + imgNumbers[separatedDigit]);
                spriteRenderer.sprite = newSprite;
            }
        }
    }

    public void PayoutCheck(int Win)
    {
        int startValue = 0;
        startValue = int.Parse(CurrentCredit());
        int newDineros = startValue + Win;
        PayoutWon(Win);
        DOTween.To(() => startValue, x => 
        {
            startValue = x;
            UpdateNumberDisplay(startValue);
        }, newDineros, 1f).OnComplete(() => 
        {
            CreditStart(newDineros); 
        }).SetEase(Ease.Linear);
    }

    public void PayoutWon(int win)
    {
        string numberAsString = win.ToString();
        int longCredit = numberAsString.Length;
        
        if (win != 0)
        {
            foreach (char digit in numberAsString)
            {
                longCredit--;
                int separatedDigit = int.Parse(digit.ToString());
                spriteRenderer = pPayout[longCredit].GetComponent<SpriteRenderer>();
                Sprite newSprite = Resources.Load<Sprite>(pathNumbers + imgNumbers[separatedDigit]);
                spriteRenderer.sprite = newSprite;
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                int separatedDigit = 0;
                spriteRenderer = pPayout[i].GetComponent<SpriteRenderer>();
                Sprite newSprite = Resources.Load<Sprite>(pathNumbers + imgNumbers[separatedDigit]);
                spriteRenderer.sprite = newSprite;
            }
        }
        
    }
    public void BetUpdate(int negativeValue)
    {
        int currentCredit = int.Parse(CurrentCredit());
        int newCredit = currentCredit + negativeValue;
    
        // Actualiza la visualización de los sprites.
        UpdateNumberDisplay(newCredit);
        CreditStart(newCredit);
    }
    
    string CurrentCredit()
    {
        String totalCredit= "";
        for (int i = pCredit.Length - 1; i >= 0; i--)
        {
            SpriteRenderer sr = pCredit[i].GetComponent<SpriteRenderer>();
            string spriteName = sr.sprite.name;
            string numberString = spriteName.Replace("N", "");
            totalCredit += numberString;
        }

        return totalCredit;
    }
    void UpdateNumberDisplay(int dineros)
    {
        string numberAsString = dineros.ToString();
        int longCredit = numberAsString.Length;
        foreach (char digit in numberAsString)
        {
            longCredit--;
            int separatedDigit = int.Parse(digit.ToString());
            spriteRenderer = pCredit[longCredit].GetComponent<SpriteRenderer>();
            Sprite newSprite = Resources.Load<Sprite>(pathNumbers + imgNumbers[separatedDigit]);
            spriteRenderer.sprite = newSprite;
        }
    }

    public void LabelResult(string Result)
    {
        spriteRenderer = panelLabel.GetComponentInChildren<SpriteRenderer>();
        Sprite newSprite = Resources.Load<Sprite>(pathNumbers + Result);
        spriteRenderer.sprite = newSprite;
    }
}

