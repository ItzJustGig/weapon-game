using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum DamageType { BULLET, ELEMENTAL, MAGIC, PHYSICAL, TRUE}
    public DamageType type;
    public Vector3 direction;
    public float lifeTime = 10;
    public float travelSpeed;
    public float damage;

    public void Setup(Vector2 direction, float lifeTime, float travelSpeed, float damage)
    {
        this.direction = direction;
        this.lifeTime = lifeTime;
        this.travelSpeed = travelSpeed;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        Vector3 dir = this.direction.normalized * travelSpeed * Time.fixedDeltaTime; // Scale direction by speed and time
        this.gameObject.transform.position += dir; // Apply the scaled direction to the position

        lifeTime -= Time.fixedDeltaTime; // Use FixedDeltaTime for FixedUpdate

        if (lifeTime <= 0)
            Destroy(this.gameObject);
    }
}
