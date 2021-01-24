using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGenerator : BreakableComponent
{
    // Update is called once per frame
    void Update()
    {
        EventLoop();            
    }

    public override void ActivateEvent()
    {
        base.ActivateEvent();

        gameMode.ToggleGravity(false);
    }

    public override void DisableEvent()
    {
        base.DisableEvent();

        gameMode.ToggleGravity(true);
    }

    public override string GetDescription()
    {
        return $"{componentName} Failure";
    }
}
