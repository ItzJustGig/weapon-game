using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

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
            Pick();
        }
    }

    public void Pick()
    {
        // Generate a random number between 0 and 100
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        float[] probabilities = 
        {
            .55f,
            .26f,
            .12f,
            .06f,
            .01f
        };

        float val = 0;
        Item.Rarity pick = Item.Rarity.COMMON;
        for (int i = 0; i < probabilities.Count(); i++)
        {
            val += probabilities[i];
            if (randomValue <= val)
            {
                switch (i)
                {
                    case 0:
                        pick = Item.Rarity.COMMON; break;
                    case 1:
                        pick = Item.Rarity.UNCOMMON; break;
                    case 2:
                        pick = Item.Rarity.RARE; break;
                    case 3:
                        pick = Item.Rarity.EPIC; break;
                    case 4:
                        pick = Item.Rarity.LEGENDARY; break;
                }
                break;
            }
        }

        List<Item> items = new List<Item>();
        items = activeItemsGO.GetComponent<ItemList>().items.Union(passiveItemsGO.GetComponent<ItemList>().items).ToList();

        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].rarity != pick)
                items.Remove(items[i]);
        }

        floorItem.item = items[UnityEngine.Random.Range(0, items.Count)];
        floorItem.ForceStart();

    }
}
