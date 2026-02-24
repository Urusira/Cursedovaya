using System.Collections;
using UnityEngine;

public class WeaponTemplate : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected GameObject weaponPrefab;
    [SerializeField] public float damage;
    [SerializeField] public float critChance;
    [SerializeField] public float critDamage;
    [SerializeField] protected float speed;
    [SerializeField] protected float cooldownDur;
    protected CurrentItemLevel currentItemLevel;

    private float _damage;
    private float _critChance;
    private float _critDamage;
    private float _speed;
    private float _cooldownDur;

    virtual protected void Start()
    {
        currentItemLevel = GetComponent<CurrentItemLevel>();
    
        _damage = damage;
        _critChance = critChance;
        _critDamage = critDamage;
        _speed = speed;
        _cooldownDur = cooldownDur;

        StartCoroutine(attackOnCD());
    }

    protected IEnumerator attackOnCD()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownDur);
            Execute();
        }
    }

    virtual protected void Execute()    
    {
        RecalcStatsForCurrentLevel();
    }

    virtual protected void RecalcStatsForCurrentLevel()
    {
        damage = _damage * currentItemLevel.levelCharsMultiplier;
        critChance = _critChance * currentItemLevel.levelCharsMultiplier;
        critDamage = _critDamage * currentItemLevel.levelCharsMultiplier;
        speed = _speed * currentItemLevel.levelCharsMultiplier;
        cooldownDur = _cooldownDur / currentItemLevel.levelCharsMultiplier;
    }
}
