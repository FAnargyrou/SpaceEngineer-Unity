using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medbay : BreakableComponent
{
    public string refillO2Sound;

    public override string GetDescription()
    {
        return "Medbay Failure\n";
    }

    // Update is called once per frame
    void Update()
    {
        EventLoop();
    }

    public override void ActivateEvent()
    {
        base.ActivateEvent();
    }

    public override void DisableEvent()
    {
        base.DisableEvent();
    }

    public override void Interact()
    {
        if (active && gameMode.playerInventory.items.Contains(requiredItem))
        {
            isFixing = true;
            PlaySound(interactSound);
        }
        else if (!active)
        {
            gameMode.RefillO2();
            PlaySound(refillO2Sound);
        }
    }
}
