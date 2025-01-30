using UnityEngine;

public abstract class ActiveItem : Item
{
    public enum ChargingMethod { COOLDOWN, ROOMS }
    public ChargingMethod charging;
    public float cooldown;
    public float maxCooldown;

    private void Update()
    {
        if (charging == ChargingMethod.COOLDOWN && cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public override void Initialize()
    {
        EventManager.OnEnterNewRoom += OnEnterRoom;
        EventManager.OnItemPickedUp += OnPickUp;
    }

    public void OnEnterRoom()
    {
        if (charging == ChargingMethod.ROOMS && cooldown > 0)
        {
            cooldown--;
        }
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
