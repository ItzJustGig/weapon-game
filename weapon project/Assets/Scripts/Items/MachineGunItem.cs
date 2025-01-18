using System.Collections;
using UnityEngine;

public class MachineGunItem : ActiveItem
{
    public float lifeTime;
    public float travelSpeed;
    public float damage;
    public float bulletInterval;
    public float bulletNumber;
    public GameObject bullet;

    public override void Active()
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            FindAnyObjectByType<PlayerAttackManager>().attackQueue.Add(new AttackQueueObject(bulletInterval*i, lifeTime, travelSpeed, damage, bullet));
        }
    }
}
