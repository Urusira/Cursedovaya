using UnityEngine;

public class ShootProjectile : WeaponTemplate
{
    private Transform _firePoint;
    public float destroyAfterSeconds;
    public GameObject shootAnim;

    protected override void Start()
    {
        base.Start();
        _firePoint = GameObject.Find("firePoint").transform;
    }

    protected override void Execute()
    {
        base.Execute();
        if (shootAnim != null)
        {
            GameObject startAnim = Instantiate(shootAnim, _firePoint.position, _firePoint.rotation, transform);
            Destroy(startAnim, startAnim.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        }
        
        GameObject projectile = Instantiate(weaponPrefab, _firePoint.position, _firePoint.rotation, transform);
        
        projectile.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * speed, ForceMode2D.Impulse);
        
        if (Random.Range(0, 100) <= critChance)
        {
            projectile.GetComponent<DamageExecute>().DmgSet(critDamage, true, true);
        }
        else
        {
            projectile.GetComponent<DamageExecute>().DmgSet(damage, true, false);
        }

        Destroy(projectile, destroyAfterSeconds);
    }
}
