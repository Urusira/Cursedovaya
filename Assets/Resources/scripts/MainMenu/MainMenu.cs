using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private float startXPositionMenu;

    private void Start()
    {
        startXPositionMenu = transform.position.x;
        StartCoroutine(StartLoadButtons());
    }

    public void toSelectorAnimTrans()
    {
        StartCoroutine(delayedHideMenuButtons());
    }

    public void toMainMenuAnimTrans()
    {
        StartCoroutine(delayedGoIn());
    }

    private IEnumerator delayedHideMenuButtons()
    {
        foreach (Transform button in transform)
        {
            button.LeanMoveX(button.position.x * 1.25f, 0.25f);
            button.LeanMoveX(button.position.x - Screen.width, 1.0f).setEaseInOutExpo().delay = 0.25f;
            yield return new WaitForSeconds(0.15f);
        }

        StopCoroutine(delayedHideMenuButtons());
    }
    
    private IEnumerator StartLoadButtons()
    {
        yield return new WaitForSeconds(0f);
        foreach (Transform button in transform)
        {
            button.LeanMoveX(button.position.x - Screen.width, 0.0f);
        }

        toMainMenuAnimTrans();
        StopCoroutine(StartLoadButtons());
    }

    private IEnumerator delayedGoIn()
    {
        yield return new WaitForSeconds(0.25f);
        foreach (Transform button in transform)
        {
            button.LeanMoveX(startXPositionMenu, 1.0f).setEaseOutExpo().delay = 0.25f;
            yield return new WaitForSeconds(0.15f);
        }

        StopCoroutine(delayedGoIn());
    }
    
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
