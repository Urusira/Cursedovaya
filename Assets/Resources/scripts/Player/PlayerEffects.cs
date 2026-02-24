using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private PlayerHPDefenseController hpController;
    private PlayerInventory inventoryController;
    private PlayerLevelingController levelingController;
    private PlayerMovement movementController;

    private void Start()
    {
        hpController = GetComponent<PlayerHPDefenseController>();
        inventoryController = GetComponent<PlayerInventory>();
        levelingController = GetComponent<PlayerLevelingController>();
        movementController = GetComponent<PlayerMovement>();
    }

    public void hpBuff(float buff)
    {
        hpController.addHpMax(buff);
    }
    
    public void defBuff(float buff)
    {
        hpController.addDefense(buff);
    }

    public void imrtBuff(float buff)
    {
        hpController.addImmortalTime(buff);
    }
    
    public void spdBuff(float buff)
    {
        movementController.addSpeed(buff);
        
    }

    public void invBuff(int buff)
    {
        inventoryController.expandInventory(buff);
    }
}
