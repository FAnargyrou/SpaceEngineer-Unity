using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public Inventory playerInventory;
    public Inventory toolboxInventory;
    BreakableComponent[] shipComponents;

    // Start is called before the first frame update
    void Start()
    {
        shipComponents = FindObjectsOfType<BreakableComponent>();
        
        if (shipComponents != null && shipComponents.Length > 0)
        {
            int i = Random.Range(0, shipComponents.Length - 1);
            shipComponents[i].ActivateEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
