using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2System : BreakableComponent
{
    static int brokenO2Count = 0;

    // Update is called once per frame
    void Update()
    {
        EventLoop();
    }

    public override void ActivateEvent()
    {
        base.ActivateEvent();

        brokenO2Count++;
        gameMode.ToggleO2(false);
        gameMode.SetO2Multiplier(brokenO2Count);
    }

    public override void DisableEvent()
    {
        base.DisableEvent();

        brokenO2Count--;
        if (brokenO2Count <= 0)
            gameMode.ToggleO2(true);
        gameMode.SetO2Multiplier(brokenO2Count);
    }

    public override string GetDescription()
    {
        return "O2 Flow is clogged; New filter required";
    }

}
