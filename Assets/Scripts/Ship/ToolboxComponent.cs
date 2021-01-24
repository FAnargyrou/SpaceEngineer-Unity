using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToolboxComponent : ShipComponent
{
    public InventoryHUD toolboxHUD;
    public InventoryHUD inventoryHUD;
    public string toolboxCloseSound;


    public override void Interact()
    {
        // If toolbox is already enabled there is no reason the replay the sound
        if (!toolboxHUD.isActiveAndEnabled)
            PlaySound(interactSound);

        toolboxHUD.gameObject.SetActive(true);
        inventoryHUD.ToggleButtons(true);
        toolboxHUD.ToggleButtons(true);
    }

    public override void Cancel()
    {
        toolboxHUD.gameObject.SetActive(false);
        inventoryHUD.ToggleButtons(false);
        toolboxHUD.ToggleButtons(false);

        PlaySound(toolboxCloseSound);
    }
}