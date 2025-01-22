using System.Collections;
using UnityEngine;

public class MachineGunItem : ActiveItem
{
    [Header("Special Stats")]
    public float lifeTime;
    public float travelSpeed;
    public float damage;
    public float bulletInterval;
    public float bulletNumber;
    public GameObject bullet;

    public override void Active(Vector2 direction)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            AttackManager.Instance.attackQueue.Add(new AttackQueueObject(bulletInterval*i, lifeTime, travelSpeed, damage, bullet, owner, direction));
        }
    }
}
