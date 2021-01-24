using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToolboxComponent : ShipComponent
{
    public InventoryHUD toolboxHUD;
    public InventoryHUD inventoryHUD;


    public override void Interact()
    {
        toolboxHUD.gameObject.SetActive(true);
        inventoryHUD.ToggleButtons(true);
        toolboxHUD.ToggleButtons(true);
    }

    public override void Cancel()
    {
        toolboxHUD.gameObject.SetActive(false);
        inventoryHUD.ToggleButtons(false);
        toolboxHUD.ToggleButtons(false);
    }
}