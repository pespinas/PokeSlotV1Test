using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReelMovement : MonoBehaviour
{
    private float spinDuration = 2f; 
    private float topPosition = 669f;
    private float bottomPosition = -445f;
    private float distance;

    private float dynamicDuration ;
    private RectTransform[] symbols;
    void Start()
    {
        symbols = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            symbols[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }
    }

    public void ReelStartMove(){
        foreach (RectTransform symbol in symbols)
        {
            MoveSymbols(symbol);
        }
    }
   public void MoveSymbols(RectTransform symbol)
    {   
        Debug.Log("asdasdasdas");
        Vector2 endPos = new Vector2(symbol.anchoredPosition.x, bottomPosition);
        distance = symbol.anchoredPosition.y - bottomPosition ;
        dynamicDuration = distance / 1000f;
        symbol.DOAnchorPosY(endPos.y, dynamicDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
           if (symbol.anchoredPosition.y >= bottomPosition)
            {
                symbol.anchoredPosition = new Vector2(symbol.anchoredPosition.x, topPosition);
            }
            MoveSymbols(symbol);
        });
           
    }
}
