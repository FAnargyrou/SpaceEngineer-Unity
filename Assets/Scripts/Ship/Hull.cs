using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : BreakableComponent
{
    public static int hullCount = 0;
    public static int hullThreshold = 0;
    public static int brokenHullCount = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hullCount++;
        hullThreshold = hullCount / 2;
    }

    // Update is called once per frame
    void Update()
    {
        EventLoop();
    }

    public override void ActivateEvent()
    {
        base.ActivateEvent();

        brokenHullCount++;
        if (brokenHullCount >= hullThreshold)
            gameMode.GameOver();
    }

    public override void DisableEvent()
    {
        base.DisableEvent();

        brokenHullCount--;
    }

    public override string GetDescription()
    {
        return $"Hull damage ({brokenHullCount}/{hullThreshold})";
    }

}
