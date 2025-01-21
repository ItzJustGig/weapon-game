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
        if (item != null)
            itemIcon.sprite = item.icon;
    }
}
