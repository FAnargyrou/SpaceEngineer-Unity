using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipComponent : MonoBehaviour
{
    public string componentName;
    public string interactSound;
    public string eventSound;
    public Item requiredItem;

    protected GameMode gameMode;

    public abstract void Interact();
    public abstract void Cancel();
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
