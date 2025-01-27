using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class SniperEnemy : EnemyController
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

   

   

    public override void Follow()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 1.15f * Time.deltaTime); ; // Move closer
        }
        else if (distance < attackRange)
        {
            // Move away from the player to maintain a safe distance
            Vector3 fleeDirection = (transform.position - player.transform.position).normalized;
            transform.position += fleeDirection * speed * 1.3f * Time.deltaTime;
        }
        else
        {
            // Enemy is within the desired range; stop moving
            // Optional: Add idle animation or aim logic here
        }
    }

    public override void Attack()
    {

        //ADD HERE USE ITEM LOGIC FOR ENEMIES ATTACK
        //I MADE THESE TO TEST THEIR BEHAVIOUR
        //
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            FireProjectile(); //Doesnt do shit right now!
            lastAttackTime = Time.time;
        }
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = (player.transform.position - transform.position).normalized * projectileSpeed;
    }
}
