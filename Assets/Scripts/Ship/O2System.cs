using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2System : BreakableComponent
{
    // Update is called once per frame
    void Update()
    {
        EventLoop();
    }

    public override void ActivateEvent()
    {
        base.ActivateEvent();

        gameMode.ToggleO2(false);
    }

    public override void DisableEvent()
    {
        base.DisableEvent();
        gameMode.ToggleO2(true);
    }

    public override string GetDescription()
    {
        return "O2 Flow is clogged; New filter required";
    }

}
