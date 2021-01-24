using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipComponent : MonoBehaviour
{
    public float timer = 0f;
    public string componentName;
    public string interactSound;
    public string eventSound;
    public Item requiredItem;

    protected GameMode gameMode;

    protected float currentTimer;
    public abstract void Interact();
    public abstract void Cancel();
    public float GetCurrentTimer() { return currentTimer; }
    virtual protected void Start()
    {
        gameMode = FindObjectOfType<GameMode>();
    }
    // Helper function for child classes to avoid code duplication
    protected void PlaySound(string soundName)
    {
        if (gameMode && gameMode.audioManager && !string.IsNullOrEmpty(interactSound))
        {
            gameMode.audioManager.Play(soundName);
        }
    }
}
