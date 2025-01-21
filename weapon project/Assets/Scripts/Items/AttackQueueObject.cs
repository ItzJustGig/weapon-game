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

    public AttackQueueObject(float delay, float lifeTime, float travelSpeed, float damage, GameObject projectile)
    {
        this.delay = delay;
        this.lifeTime = lifeTime;
        this.travelSpeed = travelSpeed;
        this.damage = damage;
        this.projectile = projectile;
    }

    public GameObject Spawn(float bonusLifeTime, float bonusTravelSpeed, float bonusDamage)
    {
        // Find the player's position
        Vector3 spawnPosition = GameObject.FindAnyObjectByType<PlayerCtrl>().transform.position;
        spawnPosition.x += Input.GetAxisRaw("Horizontal");
        spawnPosition.y += Input.GetAxisRaw("Vertical");

        // Instantiate the bullet at the player's position with no rotation
        GameObject obj = GameObject.Instantiate(projectile, spawnPosition, Quaternion.identity);
        obj.GetComponent<Projectile>().Setup(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0), lifeTime+bonusLifeTime, travelSpeed+bonusTravelSpeed, damage+bonusDamage);
        return obj;
    }
}
