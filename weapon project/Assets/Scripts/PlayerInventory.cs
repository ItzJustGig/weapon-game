using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Item item in items)
            {
                if (item is ActiveItem activeItem) // Pattern matching: Cast and check in one step
                {
                    activeItem.Active(); // Call the method defined in ActiveItem
                }
            }
        }
    }
}
