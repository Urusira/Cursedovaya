using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChestTemplate : MonoBehaviour
{
    public GameObject[] droppedItems;
    public int droppedAmount;
    
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject itemsContainter;
    [SerializeField] private GameObject PanelTemplate;

    private PlayerInventory _inventoryController;
    private PlayerInterface _interfaceController;
    private PlayerSightHandler shightController;

    public void OnTriggerEnter2D(Collider2D chestOpener)
    {
        if (chestOpener.CompareTag("Player"))
        {
            _inventoryController = chestOpener.GetComponent<PlayerInventory>();
            _interfaceController = chestOpener.GetComponent<PlayerInterface>();
            shightController = chestOpener.GetComponent<PlayerSightHandler>();

            List<GameObject> itemsForDrop = GetRandomItems();

            if (itemsForDrop.Count == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                ChestMenuItemsDraw(itemsForDrop);
            
                Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);

                menuShow();
            }
        }
    }

    private List<GameObject> GetRandomItems()
    {
        List<GameObject> currentDroppedItems = new List<GameObject> ();
        
        List<int> randomItemsIndexes = new List<int>();

        while(randomItemsIndexes.Count < droppedAmount)
        {
            int randomIndex = Random.Range(0, droppedItems.Length);
            if (!randomItemsIndexes.Contains(randomIndex))
            {
                if (_inventoryController.canPush(droppedItems[randomIndex]))
                {
                    randomItemsIndexes.Add(randomIndex);
                }
            }
        }

        foreach (int randomItemIndex in randomItemsIndexes)
        {
            currentDroppedItems.Add(droppedItems[randomItemIndex]);
        }
        
        return currentDroppedItems;
    }

    private void menuShow()
    {
        _interfaceController.disableInterface();
        menu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ChestMenuItemsDraw(List<GameObject> itemsList)
    {
        foreach (GameObject item in itemsList)
        {
            GameObject alreadyHaveItem = _inventoryController.alreadyHave(item);

            if (alreadyHaveItem != null)
            {
                GameObject itemPanel = Instantiate(PanelTemplate, itemsContainter.transform);
                itemPanel.GetComponent<ItemPanelFiller>().setPanelNextLevelItem(alreadyHaveItem);
                itemPanel.GetComponent<Button>().onClick.AddListener(() =>ItemChoice(alreadyHaveItem, true));
            }
            else
            {
                GameObject itemPanel = Instantiate(PanelTemplate, itemsContainter.transform);
                itemPanel.GetComponent<ItemPanelFiller>().setPanelNewItem(item);
                itemPanel.GetComponent<Button>().onClick.AddListener(() =>ItemChoice(item, false));
            }
        }
    }

    private void ItemChoice(GameObject item, bool execLevelUp)
    {
        switch (execLevelUp)
        {
            case false:
            {
                _inventoryController.securedInventoryPush(item);
                Time.timeScale = 1f;
                shightController.resetCursor();
                _interfaceController.enableInterface();
                Destroy(gameObject);
                break;
            }
            case true:
            {
                _inventoryController.LevelUpItem(item);
                Time.timeScale = 1f;
                shightController.resetCursor();
                _interfaceController.enableInterface();
                Destroy(gameObject);
                break;
            }
        }
    }

    public void yieldOpen()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
        
        GameObject _player = GameObject.FindWithTag("Player");
        
        _inventoryController = _player.GetComponent<PlayerInventory>();
        _interfaceController = _player.GetComponent<PlayerInterface>();
        shightController = _player.GetComponent<PlayerSightHandler>();

        List<GameObject> itemsForDrop = GetRandomItems();

        if (itemsForDrop.Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            ChestMenuItemsDraw(itemsForDrop);
            
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);

            menuShow();
        }
    }
}
