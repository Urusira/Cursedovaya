using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AuraWeapon : WeaponTemplate
{
    [SerializeField] private CircleCollider2D aura ;
    [SerializeField] private LineRenderer lineRenderer ;
    [SerializeField] private float attackRadius;
    
    private float _attackRadius;

    protected override void Start()
    {
        base.Start();
        _attackRadius = attackRadius;

        GetComponent<DamageExecute>().DmgSet(damage, false, Random.Range(0, 100) <= critChance);
        
        StartCoroutine(damagePerTime());
    }

    private void circleRedrawer()
    {
        lineRenderer.positionCount = 32;
        for (int currentStep = 0; currentStep < 32; currentStep++)
        {
            float circumferenceProgress = (float) currentStep / 32;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float x = Mathf.Cos(currentRadian) * attackRadius;
            float y = Mathf.Sin(currentRadian) * attackRadius;

            lineRenderer.SetPosition(currentStep, new Vector3(x, y, 0));
        }
    }

    protected override void Execute()
    {
        base.Execute();
        aura.radius = attackRadius;
        circleRedrawer();
    }

    private IEnumerator damagePerTime()
    {
        while (true)
        {
            aura.enabled = true;
            yield return new WaitForSeconds(0.1f);
            aura.enabled = false;
            yield return new WaitForSeconds(cooldownDur);
        }
    }

    protected override void RecalcStatsForCurrentLevel()
    {
        base.RecalcStatsForCurrentLevel();
        attackRadius = _attackRadius * currentItemLevel.levelCharsMultiplier;
        GetComponent<DamageExecute>().DmgSet(damage, false, Random.Range(0, 100) <= critChance);
    }
}