using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum DamageType { BULLET, ELEMENTAL, MAGIC, PHYSICAL, TRUE }
    public GameObject owner;
    public DamageType type;
    public Vector3 direction;
    public float lifeTime = 10;
    public float travelSpeed;
    public float damage;
    public bool destroyOnContact = true;

    public void Setup(GameObject owner, Vector2 direction, float lifeTime, float travelSpeed, float damage)
    {
        this.owner = owner;
        this.direction = direction;
        this.lifeTime = lifeTime;
        this.travelSpeed = travelSpeed;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        Vector3 dir = this.direction.normalized * travelSpeed * Time.fixedDeltaTime; // Scale direction by speed and time
        this.gameObject.transform.position += dir; // Apply the scaled direction to the position

        lifeTime -= Time.fixedDeltaTime; // countdown

        if (lifeTime <= 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool ignore = false;
        if (collision.gameObject.tag == "PickArea")
        {
            ignore = true;
        }

        if (collision.tag != owner.gameObject.tag && !ignore)
        {
            if (collision.tag == "Player")
            {
                HitPlayer();
            } else if (collision.tag == "Enemy")
            {
                HitEnemy(collision.gameObject);
            }

            if (destroyOnContact)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void HitPlayer()
    {
        FindAnyObjectByType<PlayerHealth>().TakeDamage();
    }

    public void HitEnemy(GameObject target)
    {
        EventManager.Instance.BulletHitEnemy(target, this.gameObject);
    }
}
