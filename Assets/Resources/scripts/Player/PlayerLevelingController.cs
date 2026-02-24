using System;
using System.Collections;
using UnityEngine;

public class PlayerLevelingController : MonoBehaviour
{
    [Header("Player Leveling")]
    public float currentExp = 0;
    public int currentLevel = 1;
    public float nextLevelThreshold = 50;
    public float levelUpThresholdMultiplier = 1.5f;

    [Header("Level Up Award")]
    public GameObject LevelUpChest;

    [Header("Effects")]
    [SerializeField] private GameObject LevelUpEffect;
    
    private PlayerInterface interfaceController;
    private PlayerHPDefenseController hpController;

    private void Start()
    {
        interfaceController = GetComponent<PlayerInterface>();
        hpController = GetComponent<PlayerHPDefenseController>();
        interfaceController.updateLevel(currentLevel, currentExp, nextLevelThreshold);
    }

    public void giveExp(int getExp)
    {
        currentExp += getExp;
        interfaceController.updateExp(currentExp, nextLevelThreshold);
        
        if (currentExp >= nextLevelThreshold)
        {
            StartCoroutine(LevelUp());
        }
    }

    private IEnumerator LevelUp()
    {
        currentLevel++;
        currentExp = 0;
        nextLevelThreshold = (int)(nextLevelThreshold*levelUpThresholdMultiplier);

        hpController.hpRefill();
        
        Animator anim = Instantiate(LevelUpEffect, transform).GetComponent<Animator>();
        float animDuration = anim.GetCurrentAnimatorClipInfo(0).Length+0.2f;    // 0.2f - небольшая задержка, чтобы анимация успела проиграться полностью.
        Destroy(anim, animDuration);
        yield return new WaitForSeconds(animDuration);
        
        try
        {
            if (LevelUpChest.GetComponent<ChestTemplate>());
            {
                Instantiate(LevelUpChest, transform).GetComponent<ChestTemplate>().yieldOpen();
            }
        }
        catch (Exception e)
        {
            Debug.unityLogger.LogException(e);
            LevelUpChest = null;
        }

        interfaceController.updateLevel(currentLevel, currentExp, nextLevelThreshold);
    }
}