using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectorMenu : MonoBehaviour
{
    private float unhiddenSelectorPos;
    private float hiddenSelectorPos;

    private void Start()
    {
        unhiddenSelectorPos = transform.position.x;
        transform.position = new Vector3(Screen.width*2, transform.position.y, 0);
        hiddenSelectorPos = transform.position.x;
    }

    public void goIn()
    {
        StartCoroutine(delayedGoIn());
    }

    public void goOutLeft()
    {
        transform.LeanMoveX(-hiddenSelectorPos, 0.75f).setEaseInExpo();
    }

    public void goOutRight()
    {
        transform.LeanMoveX(hiddenSelectorPos, 0.75f).setEaseInExpo();
    }

    private IEnumerator delayedGoIn()
    {
        yield return new WaitForSeconds(0.1f);
        transform.LeanMoveX(unhiddenSelectorPos, 1.0f).setEaseOutExpo().delay = 0.75f;
        StopCoroutine(delayedGoIn());
    }
}
