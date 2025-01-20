using UnityEngine;

public class GlockItem : ActiveItem
{
    [Header("Special Stats")]
    public float lifeTime;
    public float travelSpeed;
    public float damage;
    public GameObject bullet;

    public override void Active()
    {
        FindAnyObjectByType<PlayerAttackManager>().attackQueue.Add(new AttackQueueObject(0f, lifeTime, travelSpeed, damage, bullet));
    }
}
