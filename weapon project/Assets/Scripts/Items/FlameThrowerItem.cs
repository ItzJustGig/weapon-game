using System.Collections;
using UnityEngine;

public class FlameThrowerItem : ActiveItem
{
    public float lifeTime;
    public float travelSpeed;
    public float damage;
    public float fireDuration;
    float bulletInterval;
    float bulletNumber;
    public GameObject bullet;

    public override void Active()
    {
        bulletNumber = fireDuration/lifeTime;
        bulletNumber *= 5;
        bulletInterval = fireDuration / bulletNumber;

        for (int i = 0; i < bulletNumber; i++)
        {
            FindAnyObjectByType<PlayerAttackManager>().attackQueue.Add(new AttackQueueObject(bulletInterval*i, lifeTime, travelSpeed, damage, bullet));
        }
    }
}
