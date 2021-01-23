using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHUD : MonoBehaviour
{
    public Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        if (!inventory)
            Debug.LogError($"Inventory HUD {name} was not assigned an Inventory");
        inventory.onItemChangedCallback += UpdateUI;

        slots = GetComponentsInChildren<InventorySlot>();
        Debug.Log($"Length={slots.Length}");

        CreateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateUI()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
