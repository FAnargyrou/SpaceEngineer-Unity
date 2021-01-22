using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Component : ShipComponent
{
    public override void Fix()
    {
        active = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = timer;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            currentTimer = Mathf.Clamp(currentTimer - Time.deltaTime, 0f, timer);
            if (currentTimer <= 0f)
                Debug.Log("Player failed to disarm");
        }
    }
}
