using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 10;
    public float travelSpeed;
    public float damage;

    public void Setup(float lifeTime, float travelSpeed, float damage)
    {
        this.lifeTime = lifeTime;
        this.travelSpeed = travelSpeed;
        this.damage = damage;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            Destroy(this.gameObject);
    }
}
