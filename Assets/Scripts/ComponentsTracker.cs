﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComponentsTracker : MonoBehaviour
{
    BreakableComponent[] _shipComponents;
    TMP_Text _textMesh;

    // Start is called before the first frame update
    void Start()
    {
        _shipComponents = FindObjectsOfType<BreakableComponent>();
        _textMesh = GetComponent<TMP_Text>();
        if (_textMesh == null)
        {
            Debug.LogWarning("Text missing");
        }

        foreach (ShipComponent component in _shipComponents)
        {
            _textMesh.text += component.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _textMesh.text = "Components to be Fixed:\n";
        foreach (BreakableComponent component in _shipComponents)
        {
            if (component.IsActive())
                _textMesh.text += $"{component.GetDescription()}";
        }
    }
}
