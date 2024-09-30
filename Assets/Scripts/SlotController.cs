using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject prefabReel;
    public int numReel;
    public Canvas targetCanvas;
    public ReelMovement ReelMovement;
    private RectTransform[] symbols;
    private List<ReelMovement> reelMovements = new List<ReelMovement>();
    private int xpReel = -500;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numReel; i++)
        {
            GameObject newPrefab = Instantiate(prefabReel, targetCanvas.transform);
            Transform prefabPosition = newPrefab.GetComponent<Transform>();
            prefabPosition.localPosition = new Vector2(xpReel, 0);
            xpReel += 300;
            ReelMovement reelMovement = newPrefab.GetComponent<ReelMovement>();
            if (reelMovement != null)
            {
                reelMovements.Add(reelMovement);
            }
        }
    }

    // Update is called once per frame
    public void ButtonStartReel()
    {
        foreach (ReelMovement reelMovement in reelMovements)
        {
            reelMovement.ReelStartMove(); 
        }
       
    }
}
