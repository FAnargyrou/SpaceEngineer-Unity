using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSystem : BreakableComponent
{
    // Update is called once per frame
    void Update()
    {
        EventLoop();
    }

    public override void ActivateEvent()
    {
        base.ActivateEvent();

        gameMode.ToggleShield(false);
    }

    public override void DisableEvent()
    {
        base.DisableEvent();
        gameMode.ToggleShield(true);
    }

    public override string GetDescription()
    {
        return $"{componentName} is down";
    }
}
