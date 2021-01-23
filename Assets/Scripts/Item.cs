using UnityEngine;


[CreateAssetMenu(fileName = "Tool", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    Sprite icon;

    public Sprite GetIcon()
    {
        return icon;
    }
}
