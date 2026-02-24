using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SimpleEnemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private float _hitPoints = 0f;
    [SerializeField] private float immortalTime = 3.0f;
    [Header("Special Status Enemy")]
    [SerializeField] private bool _eventEnemy = false;
    [SerializeField] private bool _charmed = false; //TODO При true ищет ближайшего противника (реализовать ф-ю) и атакует
    [Header("Materials")]
    [SerializeField] private Material flashingMat;
    [SerializeField] private Material normalMat;
    [Header("Drop")]
    [SerializeField] private List<PairChestAndChance> chests;
    [SerializeField] private int expDrop;
    [Header("Sounds")]
    [SerializeField] private AudioSource asEnemy;
    [SerializeField] private AudioClip dmgTaken;
    [Header("Splashes")]
    [SerializeField] private Canvas enemyCanvas;
    [SerializeField] private GameObject dmgSplash;

    private SpriteRenderer _srEnemy;
    
    private float _immortalCooldown = 0f;
    
    private GameObject _target;
    
    private WavesSpawnScript wavesScript;

    private List<string> lastHitWeaponTypeCooldown = new List<string>();

    private void Awake()
    {
        _srEnemy = GetComponent<SpriteRenderer>();
        asEnemy = GameObject.Find("EnemiesSoundEffects").GetComponent<AudioSource>();
        _target = GameObject.FindWithTag("Player");;
        wavesScript = GameObject.Find("EnemySpawn").GetComponent<WavesSpawnScript>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        
        _immortalCooldown -= Time.fixedDeltaTime;

        if (_immortalCooldown <= 0)
        {
            lastHitWeaponTypeCooldown.Clear();
        }

        if (_target.transform.position.x < transform.position.x)
        {
            _srEnemy.flipX = true;
        }
        else
        {
            _srEnemy.flipX = false;
        }
    }

    public void TakeDamage(float damage, string weaponCurrentAttack, bool provocateImmortal, bool critical)
    {
        if (_immortalCooldown <= 0f | !lastHitWeaponTypeCooldown.Contains(weaponCurrentAttack))
        {
            asEnemy.PlayOneShot(dmgTaken);
            if (provocateImmortal)
            {
                lastHitWeaponTypeCooldown.Add(weaponCurrentAttack);
            }
            _immortalCooldown = immortalTime;

            StartCoroutine(DamageShimm());

            GameObject tempCanvasContainer = new GameObject("TempCanvasContainer");
            Canvas canvasCopy = Instantiate(enemyCanvas, tempCanvasContainer.transform);
            Destroy(tempCanvasContainer, 1.25f);
            GameObject splash = Instantiate(dmgSplash, transform.position, Quaternion.identity);
            splash.transform.SetParent(canvasCopy.transform);
            
            if (critical)
            {
                TextMeshProUGUI splashText = splash.GetComponent<TextMeshProUGUI>();
                splashText.text = "CRITICAL!\n-" + damage + "HP";
                splashText.color = new Color(255, 200, 200);
                splashText.fontSize = splashText.fontSize + 0.1f;
                splashText.fontStyle = FontStyles.Italic;
                splash.LeanMove(new Vector2(transform.position.x+Random.Range(-1.0f, 1.0f), transform.position.y+2f), 1.25f).setEaseOutExpo();
            }
            else
            {
                splash.GetComponent<TextMeshProUGUI>().text = "-" + damage + "HP";
                splash.LeanMove(new Vector2(transform.position.x+Random.Range(-1.0f, 1.0f), transform.position.y+2f), 1.25f).setEaseOutExpo();
            }
            
            _hitPoints -= damage;
            
            if (_hitPoints <= 0f)
            {
                Die();
            }
        }
    }

    private IEnumerator delayDamage(float damage)
    {
        yield return new WaitForSeconds(0.05f);

        StartCoroutine(DamageShimm());
        
        _hitPoints -= damage;
            
        if (_hitPoints <= 0f)
        {
            Die();
        }
    }

    private IEnumerator DamageShimm()
    {
        float timeEndShimm = Time.time + immortalTime;
        
        while (Time.time <= timeEndShimm) {
            _srEnemy.material = flashingMat;
            yield return new WaitForSeconds(0.15f);
            _srEnemy.material = normalMat;
            yield return new WaitForSeconds(0.15f);
        }
        _srEnemy.material = normalMat;
    }

    private void Die()
    {
        asEnemy.PlayOneShot(dmgTaken);
        
        int randomChestIndex = Random.Range(0, chests.Count);
        
        if(Random.Range(0, 100) < chests[randomChestIndex].dropChance)
        {
            Instantiate(chests[randomChestIndex].chest, transform.position, Quaternion.identity);
        }

        wavesScript.counterEnemies--;
        _target.GetComponent<PlayerLevelingController>().giveExp(expDrop);
        
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D targetAttack)
    {
        if (targetAttack.gameObject.CompareTag("Player"))
        {
            targetAttack.GetComponent<PlayerHPDefenseController>().TakeDamage(_damage);
        }
    }
}