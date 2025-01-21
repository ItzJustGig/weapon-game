using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateRandomItem : MonoBehaviour
{
    [SerializeField]
    private GameObject activeItemsGO;
    [SerializeField]
    private GameObject passiveItemsGO;

    [SerializeField]
    private FloorItem floorItem;

    private void Start()
    {
        if (floorItem.item == null)
        {
            List<Item> items = new List<Item>();
            items = activeItemsGO.GetComponent<ItemList>().items.Union(passiveItemsGO.GetComponent<ItemList>().items).ToList();

            floorItem.item = items[Random.Range(0, items.Count)];
            floorItem.ForceStart();
        }
    }
}
