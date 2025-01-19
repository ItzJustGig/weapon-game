using UnityEngine;

public class TCGItem : PassiveItem
{
    public Stats bonusStatsOnPickUp;

    public override void Initialize()
    {
        EventManager.OnItemPickedUp += PickUpNewItem;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventManager.OnItemPickedUp -= PickUpNewItem;
    }

    private void PickUpNewItem()
    {
        FindAnyObjectByType<PlayerInventory>().bonusStats.AddStats(bonusStatsOnPickUp);
    }
}
