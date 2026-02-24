using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelFiller : MonoBehaviour
{
    [SerializeField] private Image itemImagePanel;
    [SerializeField] private TextMeshProUGUI itemNamePanel;
    [SerializeField] private TextMeshProUGUI itemDesc;

    public void setPanelNewItem(GameObject item)
    {
        itemImagePanel.sprite = item.GetComponent<ItemData>().Image.GetComponent<Image>().sprite;
        itemNamePanel.text = item.GetComponent<ItemData>().Name;
        itemDesc.text = item.GetComponent<ItemData>().Description;
    }

    public void setPanelNextLevelItem(GameObject item)
    {
        itemImagePanel.sprite = item.GetComponent<ItemData>().Image.GetComponent<Image>().sprite;
        itemNamePanel.text = item.GetComponent<ItemData>().Name + " " + item.GetComponent<CurrentItemLevel>().level + " >> " + (item.GetComponent<CurrentItemLevel>().level+1) + " NEW LEVEL!";
        itemDesc.text = item.GetComponent<ItemData>().Description;
    }
}