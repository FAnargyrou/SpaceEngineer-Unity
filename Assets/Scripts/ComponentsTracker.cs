using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComponentsTracker : MonoBehaviour
{
    ShipComponent[] _shipComponents;
    TMP_Text _textMesh;

    // Start is called before the first frame update
    void Start()
    {
        _shipComponents = FindObjectsOfType<ShipComponent>();
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
        foreach (ShipComponent component in _shipComponents)
        {
            _textMesh.text += $"{component.componentName} - {component.GetCurrentTimer().ToString("0")}s";
        }
    }
}
