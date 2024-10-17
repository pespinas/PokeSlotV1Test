using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject panelCredit;    
    public GameObject panelPayout;
    private GameObject[] PCredit;
    private GameObject[] PPayout;
    private SpriteRenderer spriteRenderer;
    public int Dineros;
    private string pathNumbers = "Sprites/";
    private string[] imgNumbers= {"N0","N1","N2","N3","N4","N5","N6","N7","N8","N9"};

    // Start is called before the first frame update
    void Start()
    {
        int childCountCredit = panelCredit.transform.childCount;
        PCredit = new GameObject[childCountCredit];
        for (int i = 0; i < childCountCredit; i++)
        {
            PCredit[i] = panelCredit.transform.GetChild(i).gameObject;
        }
        int childCountPayout = panelPayout.transform.childCount;
        PPayout = new GameObject[childCountPayout];
        for (int i = 0; i < childCountPayout; i++)
        {
            PPayout[i] = panelPayout.transform.GetChild(i).gameObject;
        }
        CreditStart();
    }

    // Update is called once per frame
    void CreditStart()
    {   
        string numberAsString = Dineros.ToString();
        int longCredit = numberAsString.Length;
        if (numberAsString != "0")
        {
            foreach (char digit in numberAsString)
            {
                longCredit--;
                int separatedDigit = int.Parse(digit.ToString());
                spriteRenderer = PCredit[longCredit].GetComponent<SpriteRenderer>();
                Sprite newSprite = Resources.Load<Sprite>(pathNumbers + imgNumbers[separatedDigit]);
                spriteRenderer.sprite = newSprite;
            }
        }
    }
}

