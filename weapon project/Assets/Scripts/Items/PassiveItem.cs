using UnityEngine;

public abstract class PassiveItem : Item
{
    public enum ChargingMethod { NONE, COOLDOWN, ROOMS }
    public ChargingMethod charging;
    public float cooldown;
    public float maxCooldown;
    public Stats modifier;

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
        FindAnyObjectByType<PlayerInventory>().bonusStats.AddStats(modifier);
    }

    protected override void OnDestroy()
    {
        EventManager.OnItemPickedUp -= OnPickUp;
    }
}
