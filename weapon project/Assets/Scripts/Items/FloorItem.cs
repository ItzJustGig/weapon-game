using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorItem : MonoBehaviour
{
    public Item item;
    [SerializeField]
    SpriteRenderer itemIcon;

    private void Start()
    {
        ForceStart();
    }

    public void ForceStart()
    {
        itemIcon.sprite = item.icon;
    }
}
