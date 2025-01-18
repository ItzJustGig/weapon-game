using UnityEngine;

public class AttackQueueObject : MonoBehaviour
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

    public void Spawn()
    {
        // Find the player's position
        Vector3 spawnPosition = FindAnyObjectByType<PlayerCtrl>().transform.position;

        // Instantiate the bullet at the player's position with no rotation
        GameObject obj = Instantiate(projectile, spawnPosition, Quaternion.identity);
        obj.GetComponent<Projectile>().Setup(lifeTime, travelSpeed, damage);
    }
}
