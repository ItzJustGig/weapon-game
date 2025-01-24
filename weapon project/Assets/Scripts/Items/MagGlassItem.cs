using System.Linq;
using UnityEngine;

public class MagGlassItem : PassiveItem
{
    public float bonusSize;

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
            && proj.GetComponent<Projectile>().type is Projectile.DamageType.ELEMENTAL 
            && proj.GetComponent<Projectile>().owner == owner)
        {
            Vector3 temp = proj.transform.localScale;

            temp.x += temp.x * bonusSize;
            temp.y += temp.y * bonusSize;

            proj.transform.localScale = temp;
        }
    }

}
