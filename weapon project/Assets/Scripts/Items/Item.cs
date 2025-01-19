using UnityEditor;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public enum Rarity { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }
    public string name;
    public Sprite icon;
    public Rarity rarity;

    public abstract void Initialize(); // Called when the item is equipped or activated
    public abstract void OnPickUp();
    protected abstract void OnDestroy();
}
