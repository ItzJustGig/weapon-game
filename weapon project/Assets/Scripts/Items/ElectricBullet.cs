using System.Linq;
using UnityEngine;

public class ElectroBullet : PassiveItem
{
    public float electricChance = 0.3f;

    [Header("Electric Stats")]
    public Color color;
    public float chainRange = 2;
    public float chainTimes = 3;
    public float dmgReduc = 0.2f;
    public float bonusProjSpeed = 0.4f;

    public override void Initialize()
    {
        // Subscribe to the boss-killed event
        EventManager.OnBulletFired += OnBulletFired;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // Unsubscribe to avoid memory leaks
        EventManager.OnBulletFired -= OnBulletFired;
    }

    private void OnBulletFired(GameObject proj)
    {
        if (proj.GetComponent<Projectile>() != null 
            && proj.GetComponent<Projectile>().type is Projectile.DamageType.BULLET 
            && proj.GetComponent<Projectile>().owner == owner
            && (Random.Range(0f, 1f) <= electricChance))
        {
            proj.GetComponentInChildren<SpriteRenderer>().color = color;
            proj.AddComponent<ChainAddon>().Initialize(chainRange, chainTimes, dmgReduc, bonusProjSpeed);
        }
    }
}
