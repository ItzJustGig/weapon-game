using UnityEngine;

public abstract class PassiveItem : Item
{
    public enum ChargingMethod { COOLDOWN, ROOMS }
    public ChargingMethod charging;
    public float cooldown;
    public Stats modifier;

    public override void Initialize()
    {
        EventManager.OnItemPickedUp += OnPickUp;
    }

    public override void OnPickUp()
    {
        FindAnyObjectByType<PlayerInventory>().bonusStats.AddStats(modifier);
    }

    protected override void OnDestroy()
    {
        EventManager.OnItemPickedUp -= OnPickUp;
    }
}
