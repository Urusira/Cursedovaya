
using System;
using System.Collections;
using UnityEngine;

public class PlayerHPDefenseController : MonoBehaviour
{
    private GameObject _player;
    private PlayerInterface _playerInterface;
    private EndGame _gameEnder;
    private SpriteRenderer _srPlayer;
    private SoundController _soundController;

    [Header("Stats")]
    public float maxHitPoints;
    public float hitPoints;
    public float defense;
    public float immortalTime;
    public bool immortal;
    
    [Header("Materials")]
    public Material shimmMaterial;
    public Material normalMaterial;
    
    // Constant start stats
    private float _baseMaxHitPoints;
    private float _baseDefense;
    private float _baseImmortalTime;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerInterface = _player.GetComponent<PlayerInterface>();
        _srPlayer = _player.GetComponent<SpriteRenderer>();
        _gameEnder = _player.GetComponent<EndGame>();
        _soundController = _player.GetComponent<SoundController>();
        
        _baseMaxHitPoints = hitPoints;
        _baseDefense = defense;
        _baseImmortalTime = immortalTime;
        
        
        _playerInterface.updateHpBarMax(hitPoints);
        _playerInterface.updateHpBar(hitPoints);
    }
    
    public void TakeDamage(float damage)
    {
        if (!immortal)
        {
            _soundController.damage();
            immortal = true;
            StartCoroutine(delayDamage(damage));
        }
    }

    private IEnumerator delayDamage(float damage)
    {
        StartCoroutine(immortalTimer());
        StartCoroutine(DamageShimm());
        yield return new WaitForSeconds(0.1f);

        float goalDamage = damage-defense;
        if (goalDamage <= 0f || goalDamage == 0f)
        {
            goalDamage = damage * 0.1f;
        }

        hitPoints -= goalDamage;
            
        if (hitPoints <= 0f)
        {
            _gameEnder.GameOver();
        }

        _playerInterface.updateHpBar(hitPoints);
    }


    private IEnumerator immortalTimer()
    {
        yield return new WaitForSeconds(immortalTime);
        immortal = false;
    }

    private IEnumerator DamageShimm()
    {
        while (immortal) {
            _srPlayer.material = shimmMaterial;
            yield return new WaitForSeconds(0.15f);
            _srPlayer.material = normalMaterial;
            yield return new WaitForSeconds(0.15f);
        }
        _srPlayer.material = normalMaterial;
    }

    public void heal(float incallHeal)
    {
        hitPoints += incallHeal;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
        _playerInterface.updateHpBar(hitPoints);
    }

    public void hpRefill()
    {
        hitPoints = maxHitPoints;
        _playerInterface.updateHpBar(hitPoints);
    }
    
    public void newHpMax(float newHp)
    {
        float oldMaxHp = maxHitPoints;
        maxHitPoints = newHp;
        _playerInterface.updateHpBarMax(maxHitPoints);
        heal(oldMaxHp - newHp);
    }
    
    public void addHpMax(float addedHp)
    {
        float oldMaxHp = maxHitPoints;
        maxHitPoints += addedHp;
        _playerInterface.updateHpBarMax(maxHitPoints);
        heal(oldMaxHp - addedHp);
    }
    
    public void addDefense(float addedDef)
    {
        defense += addedDef;
    }
    
    public void addImmortalTime(float addedImmortalTime)
    {
        immortalTime += addedImmortalTime;
    }
}