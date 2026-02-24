using System;
using Unity.VisualScripting;
using UnityEngine;

public class DamageExecute : MonoBehaviour
{
    private float Damage { get; set; }
    private bool ProvocateImmortal { get; set; }
    private string Weapon { get; set; }
    private bool Critical { get; set; }
    
    [SerializeField] public bool destroyAfterContact;
    [SerializeField] public GameObject destroyAnim;
    [SerializeField] public bool Explosive;
    [SerializeField] public float explosionRadius;
    
    private void Awake()
    {
        Weapon = name;
    }

    public void DmgSet(float dmg, bool provocateImmortal, bool critical)
    {
        Damage = dmg;
        Critical = critical;
        ProvocateImmortal = provocateImmortal;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        SimpleEnemy enem = hitInfo.GetComponent<SimpleEnemy>();
        if (enem != null)
        {
            enem.TakeDamage(Damage, Weapon, ProvocateImmortal, Critical);
        }

        if (destroyAfterContact && !hitInfo.CompareTag("Player") && !hitInfo.CompareTag("Weapon"))
        {
            if (destroyAnim != null)
            {
                GameObject destroyedAnim = Instantiate(destroyAnim, hitInfo.transform.position, Quaternion.identity);
                if (Explosive)
                {
                    explosion(hitInfo.transform.position);
                }
                Destroy(destroyedAnim, destroyedAnim.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
            }
            Destroy(gameObject);
        }
    }

    private void explosion(Vector2 explodePosition)
    {
        GameObject pointOfExplode = new GameObject("EEEEXPLOSION");
        Destroy(pointOfExplode, 0.1f);
        pointOfExplode.transform.position = explodePosition;
        CircleCollider2D explode = pointOfExplode.AddComponent<CircleCollider2D>();
        explode.radius = explosionRadius;
        explode.isTrigger = true;
        explode.AddComponent<DamageExecute>().DmgSet(Damage, ProvocateImmortal, Critical);
    }
}