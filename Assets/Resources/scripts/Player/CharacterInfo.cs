using System;
using System.Collections;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [Header("Player Info")]
    public Characters character;
    public Sprite icon;
    public Sprite preview;
    public string desc;
    [HideInInspector] public string characteristics;

    private PlayerHPDefenseController _hpController;
    private PlayerLevelingController _levelingController;
    private PlayerInventory _inventoryController;
    private PlayerMovement _movementController;

    private void Start()
    {
        initializateComponents();
        StartCoroutine(characteristicsUpdate());
    }

    private IEnumerator characteristicsUpdate()
    {
        characteristics = "HP: " + _hpController.maxHitPoints + "\n"+
                          "DEF: " + _hpController.defense + "\n"+
                          "SPD: " + _movementController.speed + "\n"+
                          "INVCAP: " + _inventoryController.inventoryCapacity;
        yield return new WaitForSeconds(1f);
    }

    public void initBasicCharacteristics()
    {
        initializateComponents();
        characteristics = "HP: " + _hpController.maxHitPoints + "\n"+
                           "DEF: " + _hpController.defense + "\n"+
                           "SPD: " + _movementController.speed + "\n"+
                           "INVCAP: " + _inventoryController.inventoryCapacity;
    }

    private void initializateComponents()
    {
        _hpController = GetComponent<PlayerHPDefenseController>();
        _levelingController = GetComponent<PlayerLevelingController>();
        _inventoryController = GetComponent<PlayerInventory>();
        _movementController = GetComponent<PlayerMovement>();
    }
}