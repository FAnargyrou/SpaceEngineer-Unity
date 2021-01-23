﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    Item _item;
    Inventory inventory;
    public Button button;

    public void Start()
    {
        inventory = GetComponentInParent<InventoryHUD>().inventory;
    }

    public void AddItem(Item item)
    {
        _item = item;
        icon.sprite = item.GetIcon();
        icon.enabled = true;
        Debug.Log(name);
        button.interactable = true;
    }

    public void ClearSlot()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        button.interactable = false;
    }

    public void DepositItem()
    {
        inventory.RemoveItem(_item);
    }
}
