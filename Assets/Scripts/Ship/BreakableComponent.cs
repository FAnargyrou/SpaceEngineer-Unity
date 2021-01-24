using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreakableComponent : ShipComponent
{
    protected bool active;
    protected bool isFixing;

    public float fixRequired = 100f;
    // Some ship components should allow the player to walk over them if not broken (ie. Hull/Floor)
    public bool collidable = true;
    float fixProgress = 0f;

    Sprite defaultSprite;
    public Sprite brokenSprite;

    protected override void Start()
    {
        base.Start();
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        if (!collidable)
            GetComponent<BoxCollider2D>().enabled = false;
    }

    protected void EventLoop()
    {
        if (active)
        {
            if (isFixing)
            {
                fixProgress = Mathf.Clamp(fixProgress + Time.deltaTime * 10f, 0f, fixRequired);
                if (fixProgress >= fixRequired)
                {
                    DisableEvent();
                }
            }

            currentTimer = Mathf.Clamp(currentTimer - Time.deltaTime, 0f, timer);
            if (currentTimer <= 0f)
                Debug.Log("Player failed to disarm");
        }
    }

    public virtual void ActivateEvent()
    {
        active = true;
        currentTimer = timer;
        if (brokenSprite)
        {
            GetComponent<SpriteRenderer>().sprite = brokenSprite;
            if (!collidable)
                GetComponent<BoxCollider2D>().enabled = true;
        }
        PlaySound(eventSound);
    }

    public virtual void DisableEvent()
    {
        active = false;
        if (brokenSprite)
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
        if (!collidable)
            GetComponent<BoxCollider2D>().enabled = false;

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
        Debug.Log("interacting with " + componentName);
        if (gameMode.playerInventory.items.Contains(requiredItem) && active)
        {
            isFixing = true;
            Debug.Log(interactSound);
            PlaySound(interactSound);
        }
    }

    public abstract string GetDescription();
}
