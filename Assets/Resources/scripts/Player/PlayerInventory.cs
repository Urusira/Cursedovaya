using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Player Inventory")]
    public int inventoryCapacity;
    public List<GameObject> startInventory;
    
    // Containers (parent transforms)
    [HideInInspector] public GameObject itemsContainer;

    // Player inventory
    public List<GameObject> inventory = new List<GameObject>();
    
    // Controllers
    private PlayerInterface interfaceController;
    
    // Hidden variables
    [HideInInspector] public int inventoryWeaponsCount;
    [HideInInspector] public int inventoryWearablesCount;

    private void Awake()
    {
        interfaceController = GetComponent<PlayerInterface>();
        
        itemsContainer = GameObject.Find("itemsContainer");
            
        startInventoryInitiate(startInventory);
    }
    
    public void securedInventoryPush(GameObject item)
    {
        GameObject alreadyHaveItem = inventory.Find(obj => obj.name == item.name+"(Clone)");
        
        if (alreadyHaveItem == null)
        {
            switch (item.GetComponent<ItemData>().Type)
            {
                case itemType.Weapon:
                {
                    if (inventoryWeaponsCount < inventoryCapacity)
                    {
                        GameObject newAddedItem = Instantiate(item, itemsContainer.transform);
                        inventory.Add(newAddedItem);
                        interfaceController.inventoryRedraw(inventory);
                        inventoryWeaponsCount++;
                    }
                    break;
                }
                case itemType.Wearable:
                {
                    if (inventoryWearablesCount < inventoryCapacity)
                    {
                        GameObject newAddedItem = Instantiate(item, itemsContainer.transform);
                        inventory.Add(newAddedItem);
                        interfaceController.inventoryRedraw(inventory);
                        inventoryWearablesCount++;
                    }
                    break;
                }
            }
        }
        else
        {
            alreadyHaveItem.GetComponent<CurrentItemLevel>().LevelUp();
            interfaceController.inventoryRedraw(inventory);
        }
    }
    
    public void unsecuredInventoryPush(GameObject item)
    {
        switch (item.GetComponent<ItemData>().Type)
        {
            case itemType.Weapon:
            {
                if (inventoryWeaponsCount < inventoryCapacity)
                {
                    GameObject newAddedItem = Instantiate(item, itemsContainer.transform);
                    inventory.Add(newAddedItem);
                    interfaceController.inventoryRedraw(inventory);
                    inventoryWeaponsCount++;
                }
                break;
            }
            case itemType.Wearable:
            {
                if (inventoryWearablesCount < inventoryCapacity)
                {
                    GameObject newAddedItem = Instantiate(item, itemsContainer.transform);
                    inventory.Add(newAddedItem);
                    interfaceController.inventoryRedraw(inventory);
                    inventoryWearablesCount++;
                }
                break;
            }
        }
    }

    public void LevelUpItem(GameObject alreadyHaveItem)
    {
        if (alreadyHaveItem != null)
        {
            alreadyHaveItem.GetComponent<CurrentItemLevel>().LevelUp();
            interfaceController.inventoryRedraw(inventory);
        }
    }

    private void startInventoryInitiate(List<GameObject> Items)
    {
        foreach (GameObject item in Items)
        {
            securedInventoryPush(item);
        }
    }

    public void expandInventory(int addedSlotsAmount)
    {
        inventoryCapacity += addedSlotsAmount;
    }

    public bool canPush(GameObject item)
    {
        GameObject alreadyHaveItem = alreadyHave(item);
        if (alreadyHaveItem != null && alreadyHaveItem.GetComponent<CurrentItemLevel>().level < 10)
        {
            return true;
        }
        else if (alreadyHaveItem == null)
        {
            switch (item.GetComponent<ItemData>().Type)
            {
                case itemType.Weapon:
                {
                    if (inventoryWeaponsCount < inventoryCapacity)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
                case itemType.Wearable:
                {
                    if (inventoryWearablesCount < inventoryCapacity)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
        }

        return false;
    }

    public GameObject alreadyHave(GameObject itemForCheck)
    {
        return inventory.Find((obj => obj.name == itemForCheck.name + "(Clone)"));
    }
}