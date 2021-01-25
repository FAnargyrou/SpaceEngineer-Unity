using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : BreakableComponent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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

    public override string GetDescription()
    {
        return $"Hull damage\n";
    }

}
