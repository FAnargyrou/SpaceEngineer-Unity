using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipComponent : MonoBehaviour
{
    public float timer = 0f;
    public string componentName;
    public Item requiredItem;

    protected GameMode gameMode;

    protected float currentTimer;
    public abstract void Interact();
    public abstract void Cancel();
    public float GetCurrentTimer() { return currentTimer; }
    public void Start()
    {
        gameMode = FindObjectOfType<GameMode>();
    }
}
