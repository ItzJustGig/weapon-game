using System.Linq;
using UnityEngine;

public class ElectroBullet : PassiveItem
{
    public Color color;

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
        if (proj.GetComponent<Projectile>() != null && proj.GetComponent<Projectile>().type is Projectile.DamageType.BULLET)
        {
            proj.GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }

}
