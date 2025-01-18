using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Rarity { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }
    public string name;
    public Rarity rarity;
}
