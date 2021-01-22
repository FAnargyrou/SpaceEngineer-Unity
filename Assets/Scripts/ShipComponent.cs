using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipComponent : MonoBehaviour
{
    public float timer = 0f;
    public bool active;
    public string componentName;
    protected float currentTimer;
    public abstract void Fix();
    public float GetCurrentTimer() { return currentTimer; }
}
