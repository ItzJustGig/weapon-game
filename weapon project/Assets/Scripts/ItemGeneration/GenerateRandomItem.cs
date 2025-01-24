using NUnit.Framework;
using NUnit.Framework.Interfaces;
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

    //probability rates
    public float[] probabilities =
    {
        .55f,
        .26f,
        .12f,
        .06f,
        .01f
    };

    [SerializeField]
    protected FloorItem floorItem;

    private void Start()
    {
        if (floorItem.item == null)
        {
            Pick();
        }
    }

    public virtual void Pick()
    {
        // Generate a random number between 0 and 100, this will determite the rarity of the item
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        //based on the comulative values of the probabilities, it will determine which rarity was picked
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

        //unifies the passive and active items lists
        List<Item> items = new List<Item>();
        items = activeItemsGO.GetComponent<ItemList>().items.Union(passiveItemsGO.GetComponent<ItemList>().items).ToList();

        //goes through every item, and removes the ones that are not of the picked rarity
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].rarity != pick)
                items.Remove(items[i]);
        }

        //generates a random item, from those that were left out, and spawns it
        PickItem(items[UnityEngine.Random.Range(0, items.Count)]);

    }

    protected virtual void PickItem(Item item)
    {
        floorItem.item = item;
        floorItem.ForceStart();
    }

    public List<Item> GetItems()
    {
        return activeItemsGO.GetComponent<ItemList>().items.Union(passiveItemsGO.GetComponent<ItemList>().items).ToList();
    }
}
