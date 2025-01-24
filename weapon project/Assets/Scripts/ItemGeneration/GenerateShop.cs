using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateShop : GenerateRandomItem
{
    [Serializable]
    struct Gold
    {
        public int minGold;
        public int maxGold;

        public Gold (int minGold, int maxGold)
        {
            this.minGold = minGold;
            this.maxGold = maxGold;
        }
    }

    [SerializeField]
    Gold[] gold = {
        new Gold(3, 5),
        new Gold(4, 9),
        new Gold(8, 13),
        new Gold(12, 16),
        new Gold(17, 20)
    };

    int value = 0;
    [SerializeField]
    TextMeshPro text;

    public override void Pick()
    {
        base.Pick();

        Item.Rarity rarity = floorItem.item.rarity;
        int min = 0;
        int max = 0;
        switch (rarity)
        {
            case Item.Rarity.COMMON:
                (min, max) = (gold[0].minGold, gold[0].maxGold);
                break;
            case Item.Rarity.UNCOMMON:
                (min, max) = (gold[1].minGold, gold[1].maxGold);
                break;
            case Item.Rarity.RARE:
                (min, max) = (gold[2].minGold, gold[2].maxGold);
                break;
            case Item.Rarity.EPIC:
                (min, max) = (gold[3].minGold, gold[3].maxGold);
                break;
            case Item.Rarity.LEGENDARY:
                (min, max) = (gold[4].minGold, gold[4].maxGold);
                break;
        }

        value = UnityEngine.Random.Range(min, max+1);
        text.text = value.ToString("0G");
    }
}
