using System.Collections;
using UnityEngine;

public class PassiveItemTemplate : MonoBehaviour
{
    protected CurrentItemLevel currentItemLevel;
    
    protected PlayerEffects buffs;
    
    protected virtual void Start()
    {
        buffs = GameObject.FindWithTag("Player").GetComponent<PlayerEffects>();
        currentItemLevel = GetComponent<CurrentItemLevel>();
        StartCoroutine(timerRecalcStatsCycle());
    }

    protected virtual void BuffApply()
    {
        
    }

    private IEnumerator timerRecalcStatsCycle()
    {
        yield return new WaitForSeconds(1f);
        recalcItemStatsForCurrentLevel();
    }
    
    protected virtual void recalcItemStatsForCurrentLevel()
    {
        BuffApply();
    }
}