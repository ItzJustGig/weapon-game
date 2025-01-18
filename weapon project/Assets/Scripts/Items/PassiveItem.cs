using UnityEngine;

public abstract class PassiveItem : Item
{
    public enum ChargingMethod { COOLDOWN, ROOMS }
    public ChargingMethod charging;
    public float cooldown;
    public Stats modifier;
}
