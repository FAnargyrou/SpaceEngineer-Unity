using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableComponent : ShipComponent
{
    bool active;
    bool isFixing;

    public float fixRequired = 100f;
    float fixProgress = 0f;

    public void ActivateEvent()
    {
        active = true;
        currentTimer = timer;
    }

    public bool IsActive()
    {
        return active;
    }

    public float GetProgress()
    {
        return fixProgress / fixRequired;
    }

    public override void Cancel()
    {
        fixProgress = 0f;
        isFixing = false;
    }

    public override void Interact()
    {
        if (gameMode.playerInventory.items.Contains(requiredItem))
            isFixing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (isFixing)
            {
                Debug.Log(fixProgress);
                fixProgress = Mathf.Clamp(fixProgress + Time.deltaTime * 10f, 0f, fixRequired);
                if (fixProgress >= fixRequired)
                    active = false;
            }

            currentTimer = Mathf.Clamp(currentTimer - Time.deltaTime, 0f, timer);
            if (currentTimer <= 0f)
                Debug.Log("Player failed to disarm");
        }
    }
}
