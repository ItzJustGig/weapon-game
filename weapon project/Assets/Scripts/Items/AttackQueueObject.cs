using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AttackQueueObject
{
    public float delay; 
    public float lifeTime;
    public float travelSpeed;
    public float damage;
    public GameObject projectile;
    public GameObject owner;
    public Vector2 direction;

    public AttackQueueObject(float delay, float lifeTime, float travelSpeed, float damage, GameObject projectile, GameObject owner, Vector2 direction)
    {
        this.delay = delay;
        this.lifeTime = lifeTime;
        this.travelSpeed = travelSpeed;
        this.damage = damage;
        this.projectile = projectile;
        this.owner = owner;
        this.direction = direction;
    }

    public GameObject Spawn(float bonusLifeTime, float bonusTravelSpeed, float bonusDamage)
    {
        // Instantiate the bullet at the player's position with no rotation
        GameObject obj = GameObject.Instantiate(projectile, owner.transform.position, Quaternion.identity);
        obj.GetComponent<Projectile>().Setup(owner, new Vector3(direction.x, direction.y, 0), lifeTime+bonusLifeTime, travelSpeed+bonusTravelSpeed, damage+bonusDamage);
        return obj;
    }
}
