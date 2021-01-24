using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public Inventory playerInventory;
    public Inventory toolboxInventory;
    public Item[] items;
    BreakableComponent[] shipComponents;

    // Start is called before the first frame update
    void Start()
    {
        shipComponents = FindObjectsOfType<BreakableComponent>();

        playerInventory.items.Clear();
        toolboxInventory.items.Clear();

        if (shipComponents != null && shipComponents.Length > 0)
        {
            int i = Random.Range(0, shipComponents.Length - 1);
            shipComponents[i].ActivateEvent();
        }

        foreach (Item item in items)
        {
            toolboxInventory.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
