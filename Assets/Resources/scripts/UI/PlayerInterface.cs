using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    private GameObject _player;
    
    [HideInInspector] public Slider expUI;
    [HideInInspector] public TextMeshProUGUI currentExpNum;
    [HideInInspector] public TextMeshProUGUI currentLevelNum;
    [HideInInspector] public TextMeshProUGUI nextLevelNum;
    [HideInInspector] public Slider hpBar;
    [HideInInspector] public GameObject menu;
    [HideInInspector] public GameObject WeaponsContainer;
    [HideInInspector] public GameObject WearablesContainer;
    [HideInInspector] public GameObject[] levelsIcons;
    [HideInInspector] public CanvasGroup playerGUI;

    private void Start()
    {
        interfaceInitialize();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menu.GetComponent<Menu>().gameMenuCall();
        }
    }

    public void GameOver()
    {
        disableInterface();
        menu.GetComponent<Menu>().gameOverShow();
    }

    public void updateHpBar(float newHp)
    {
        hpBar.value = newHp;
    }
    
    public void updateHpBarMax(float newHp)
    {
        hpBar.maxValue = newHp;
    }
    
    public void updateLevel(int level, float exp, float nextLevelThreshold)
    {
        expUI.maxValue = nextLevelThreshold;
        expUI.value = exp;
        
        currentExpNum.text = exp + "/" + nextLevelThreshold;
        currentLevelNum.text = level.ToString();
        nextLevelNum.text = (level+1).ToString();
    }
    
    public void updateExp(float exp, float nextLevelThreshold)
    {
        expUI.value = exp;
        
        currentExpNum.text = exp + "/" + nextLevelThreshold;
    }

    public void inventoryRedraw(List<GameObject> InventoryList)
    {
        try
        {
            foreach (Transform currentInventoryUiItem in WeaponsContainer.transform)
            {
                if(currentInventoryUiItem != null) Destroy(currentInventoryUiItem.gameObject);
            }
            foreach (Transform currentInventoryUiItem in WearablesContainer.transform)
            {
                if(currentInventoryUiItem != null) Destroy(currentInventoryUiItem.gameObject);
            }
            foreach (GameObject item in InventoryList)
            {
                ItemData itemData = item.GetComponent<ItemData>();
                if (itemData.Type == itemType.Weapon)
                {
                    GameObject newUiItem = Instantiate(itemData.Image, WeaponsContainer.transform);
                    Instantiate(levelsIcons[item.GetComponent<CurrentItemLevel>().level-1], newUiItem.transform);
                }
                else
                {
                    GameObject newUiItem = Instantiate(itemData.Image, WearablesContainer.transform);
                    Instantiate(levelsIcons[item.GetComponent<CurrentItemLevel>().level-1], newUiItem.transform);
                }
            }
        }
        catch (NullReferenceException)
        {
            interfaceInitialize();
            inventoryRedraw(InventoryList);
        }
        catch (UnassignedReferenceException)
        {
            interfaceInitialize();
            inventoryRedraw(InventoryList);
        }
    }

    private void interfaceInitialize()
    {
        _player = GameObject.FindWithTag("Player");
        
        playerGUI = GameObject.Find("PlayerGUI").GetComponent<CanvasGroup>();

        expUI = GameObject.Find("ExpBar").GetComponent<Slider>();
        currentExpNum = GameObject.Find("CurrentExp").GetComponent<TextMeshProUGUI>();
        currentLevelNum = GameObject.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        nextLevelNum = GameObject.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        hpBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        
        WeaponsContainer = GameObject.Find("Weapon");
        WearablesContainer = GameObject.Find("Wearables");
        
        menu = GameObject.Find("Menu");
        levelsIcons = Resources.LoadAll("prefab/UI/itemLevelsIcons", typeof(GameObject)).Cast<GameObject>().ToArray();
    }

    public void disableInterface()
    {
        try
        {
            playerGUI.alpha = 0;
        }
        catch (NullReferenceException)
        {
            interfaceInitialize();
            disableInterface();
        }
    }

    public void enableInterface()
    {
        try
        {
            playerGUI.alpha = 1;
        }
        catch (NullReferenceException)
        {
            interfaceInitialize();
            enableInterface();
        }
    }
}
