using System.Collections;
using UnityEngine;

public class MeleeWeapon : WeaponTemplate
{
    private Transform _firePoint;
    [SerializeField] private float rangeAttack;
    [SerializeField] private float attackDelay;
    [SerializeField] private float endAttackOffset;

    private GameObject weaponExempl;
    
    private float _rangeAttack;

    protected override void Start()
    {
        base.Start();
        _rangeAttack = rangeAttack;
        _firePoint = GameObject.Find("firePoint").transform;
    }

    protected override void Execute()
    {
        base.Execute();

        weaponExempl = Instantiate(weaponPrefab, _firePoint.position, _firePoint.rotation, transform);
        weaponExempl.transform.localScale = new Vector3(rangeAttack, rangeAttack, 0);
        Animator animatorWeapon = weaponExempl.GetComponent<Animator>();
        weaponExempl.GetComponent<Collider2D>().enabled = false;
        animatorWeapon.speed = speed;
        Destroy(weaponExempl, animatorWeapon.GetCurrentAnimatorClipInfo(0).Length);
        StartCoroutine(DelayEnableDamageArea(animatorWeapon.GetCurrentAnimatorClipInfo(0).Length * attackDelay / speed));
        StartCoroutine(DelayDisableDamageArea(animatorWeapon.GetCurrentAnimatorClipInfo(0).Length * endAttackOffset / speed));
    }

    protected override void RecalcStatsForCurrentLevel()
    {
        base.RecalcStatsForCurrentLevel();
        rangeAttack = _rangeAttack * currentItemLevel.levelCharsMultiplier;
    }

    private IEnumerator DelayEnableDamageArea(float delay)
    {
        yield return new WaitForSeconds(delay);
        weaponExempl.GetComponent<Collider2D>().enabled = true;
        if (Random.Range(0, 100) <= critChance)
        {
            weaponExempl.GetComponent<DamageExecute>().DmgSet(critDamage, true, true);
        }
        else
        {
            weaponExempl.GetComponent<DamageExecute>().DmgSet(damage, true, false);
        }
    }
    private IEnumerator DelayDisableDamageArea(float offset)
    {
        yield return new WaitForSeconds(offset);
        weaponExempl.GetComponent<Collider2D>().enabled = false;
    }
}