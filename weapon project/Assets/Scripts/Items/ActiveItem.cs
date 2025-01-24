using UnityEngine;

public abstract class ActiveItem : Item
{
    public enum ChargingMethod { COOLDOWN, ROOMS, MANA }
    public ChargingMethod charging;
    public float cooldown;

    public override void Initialize()
    {
        EventManager.OnItemPickedUp += OnPickUp;
    }
    public override void OnPickUp()
    {
    }

    public abstract void Active(Vector2 direction);

    protected override void OnDestroy()
    {
        EventManager.OnItemPickedUp -= OnPickUp;
    }
}
