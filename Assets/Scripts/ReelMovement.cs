using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System;

public class ReelMovement : MonoBehaviour
{
    private float topPosition = 660f;
    private float bottomPosition = -440f;
    private float distance;
    private float[] positionFinal = {660f, 440f, 220f, 0f, -220f};
    private float dynamicDuration ;
    float elapsedTime;
    int countSimbol;
    public event Action OnReelStop;
    private SpriteRenderer spriteRenderer;
    private RectTransform[] symbols;
    private string pathSymbol = "Sprites/";
    private string[] imgSymols= {"S01","S02","S03","S04","S05","S06","SRe"};
    void Start()
    {
        symbols = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            symbols[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }
    }
    //Funcion que es llamada desde SlotController para comenzar el movimiento
    public void ReelStartMove(float time ){
        elapsedTime = 0;
        countSimbol = 0;
        foreach (RectTransform symbol in symbols)
        {
            MoveSymbols(symbol,time);
        }
    }
   public void MoveSymbols(RectTransform symbol, float delayEnd)
    {   

        elapsedTime += Time.deltaTime * 100 ;
        Vector2 endPos = new Vector2(symbol.anchoredPosition.x, bottomPosition);
        distance = symbol.anchoredPosition.y - bottomPosition ;
        dynamicDuration = distance / 3000f;
        Debug.Log(dynamicDuration);
        symbol.DOAnchorPosY(endPos.y, dynamicDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            //Mueve el simbolo a la parte superior para el bucle
           if (symbol.anchoredPosition.y >= bottomPosition)
            {
                int randNumber = Random.Range(0, 7);
                spriteRenderer = symbol.GetComponent<SpriteRenderer>();
                Sprite newSprite = Resources.Load<Sprite>(pathSymbol + imgSymols[randNumber]);
                spriteRenderer.sprite = newSprite;
                symbol.anchoredPosition = new Vector2(symbol.anchoredPosition.x, topPosition);
            }
            //Despues de x tiempo paramos el tween y añadimos una animación para la parada de reels. Se paran primero los 2 simbolos de arriba que no se ven
            if (elapsedTime >= delayEnd)
            {
                symbol.DOKill();
                symbol.DOAnchorPosY(positionFinal[countSimbol], 0.3f).SetEase(Ease.OutBack);
                countSimbol++;
                if (countSimbol == 5)
                {
                    StopReel();
                }
            }
            else
            {
                MoveSymbols(symbol,delayEnd);
            }
            
        });
    }

    public void StopReel()
    {
        OnReelStop.Invoke();
    }
}
