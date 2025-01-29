using UnityEditor;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public enum Rarity { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }
    public string name;
    public string desc;
    public bool isEnemyCompatible;
    public Sprite icon;
    public Rarity rarity;
    [SerializeField]
    protected GameObject owner;

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    public abstract void Initialize(); // Called when the item is equipped or activated
    public abstract void OnPickUp();
    protected abstract void OnDestroy();
}
