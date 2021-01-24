using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items;
    public int space = 2;

    public Inventory deposit;

    public void Awake()
    {
        items = new List<Item>();
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            return false;
        }

        items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    }

    public void RemoveItem(Item item)
    {
        
        if(deposit.Add(item))
        {
            items.Remove(item);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        
    }

}
