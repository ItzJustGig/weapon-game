using UnityEngine;

public abstract class ActiveItem : Item
{
    public enum ChargingMethod { COOLDOWN, ROOMS, MANA }
    public ChargingMethod charging;
    public float cooldown;

    public abstract void Active();
}
