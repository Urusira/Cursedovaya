using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStats : MonoBehaviour
{
    [SerializeField] private Image CharPreview;
    [SerializeField] private TextMeshProUGUI CharName;
    [SerializeField] private TextMeshProUGUI LevelExp;
    [SerializeField] private TextMeshProUGUI hpStat;
    [SerializeField] private TextMeshProUGUI defStat;
    [SerializeField] private TextMeshProUGUI spdStat;
    [SerializeField] private TextMeshProUGUI dmgStat;
    [SerializeField] private TextMeshProUGUI critChanceStat;

    private PlayerHPDefenseController _hpController;
    private PlayerInventory _inventoryController;
    private CharacterInfo _characterInfo;
    private PlayerLevelingController _levelingController;
    private PlayerMovement _movementController;
    
    private GameObject _player;

    private void Start()
    {
        initializateComponents();
    }

    public void updateChars()
    {
        try
        {
            CharPreview.sprite = _player.GetComponent<CharacterInfo>().icon;
            CharName.text = _characterInfo.character.ToString();
            LevelExp.text = "Lvl " + _levelingController.currentLevel + " Exp " + _levelingController.currentExp;
            hpStat.text = _hpController.hitPoints + "/" + _hpController.maxHitPoints;
            defStat.text = _hpController.defense.ToString();
            spdStat.text = _movementController.speed.ToString();
            float currentDmg = 0;
            float currentCritChance = 0;
            foreach (GameObject item in _inventoryController.inventory)
            {
                if (item.GetComponent<ItemData>().Type == itemType.Weapon)
                {
                    WeaponTemplate weapoTemp = item.GetComponent<WeaponTemplate>();
                    currentDmg += weapoTemp.damage;
                    currentCritChance += weapoTemp.critChance;
                }
            }
            currentCritChance /= _inventoryController.inventoryWeaponsCount;
            dmgStat.text = Math.Round(currentDmg, 2).ToString();
            critChanceStat.text = Math.Round(currentCritChance, 2)+"%";
        }
        catch (NullReferenceException)
        {
            initializateComponents();
            updateChars();
        }
    }

    private void initializateComponents()
    {
        _player = GameObject.FindWithTag("Player");
        
        _hpController = _player.GetComponent<PlayerHPDefenseController>();
        _inventoryController = _player.GetComponent<PlayerInventory>();
        _movementController = _player.GetComponent<PlayerMovement>();
        _levelingController = _player.GetComponent<PlayerLevelingController>();
        _characterInfo = _player.GetComponent<CharacterInfo>();
    }
}
