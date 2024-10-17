using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject prefabReel;
    public int numReel;
    public Button myButton;
    private static Button staticButton;
    private float delay;
    private int reelStopCount;
    public Canvas targetCanvas;
    private List<ReelMovement> reelMovements = new List<ReelMovement>();
    private int xpReel = -323;
    public event Action OnReelStop;

    // Start is called before the first frame update
    void Start()
    {
        staticButton = myButton;
        for (int i = 0; i < numReel; i++)
        {
            GameObject newPrefab = Instantiate(prefabReel, targetCanvas.transform);
            Transform prefabPosition = newPrefab.GetComponent<Transform>();
            prefabPosition.localPosition = new Vector2(xpReel, 15);
            xpReel += 180;
            ReelMovement reelMovement = newPrefab.GetComponent<ReelMovement>();
            if (reelMovement != null)
            {
                reelMovements.Add(reelMovement);
            }
            reelMovement.OnReelStop += ReelStopped;
        }
    }

    // Llamamos al reelmovement añadiendole más tiempo para que haya delay en el final de cada reel. Tambien desactivamos y activamos el botón
    public void ButtonStartReel()
    {
        myButton.interactable = false;
        delay= 7;
        foreach (ReelMovement reelMovement in reelMovements)
        {
            reelMovement.ReelStartMove(delay);
            delay += 2; 
        }
    }

    public void ReelStopped()
    {
        reelStopCount++;
        if (reelStopCount >= 3)
        {
            StartCoroutine(ActivateButtonWithDelay(0.5f));
            reelStopCount = 0;
        }

    }
     private IEnumerator ActivateButtonWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        staticButton.interactable = true; // Activa el botón
    }
}