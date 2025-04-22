using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private UIController uicontroller;
    void Start()
    {
       uicontroller = GetComponent<UIController>();
    }
    public void StartAnimPrincipal()
    {
        animator.SetTrigger("StartAnim");
        uicontroller.LabelResult("");
    }
    public void OnWin()
    {
        animator.SetTrigger("PlayWin");
        uicontroller.LabelResult("win");
    }

    public void OnLose()
    {
        animator.SetTrigger("PlayLose");
        uicontroller.LabelResult("lose");
    }
    
}
