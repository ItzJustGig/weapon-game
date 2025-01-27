using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemy : EnemyController
{
    public float attackPower = 10f;
    public int numberOfShots = 3;
    public float timeBetweenShots = 1;

    //Adding projectile
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 5f;

    //Adding some strafe to our shooter guy
    public float preferredDistance = 5f;
    public float wiggleAmount = 2f;
    public float wiggleSpeed = 2f;

    public Vector3 offsetDirection;

    public override void Follow()
    {
        if(!ableToMove) return;

        //calcular distancia e direction ao player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        //manter a distancia ao player
        if(distanceToPlayer > preferredDistance)
        {
            //move closer
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                speed * Time.deltaTime
            );
        }
        else if (distanceToPlayer < preferredDistance)
        {
            //move away
            transform.position = Vector2.MoveTowards(
                transform.position,
                transform.position - directionToPlayer,
                speed * Time.deltaTime
            );
        }

        //Wiggle
        Vector3 perpendicularDirection = Vector3.Cross(directionToPlayer, Vector3.forward);
        offsetDirection = Mathf.Sin(Time.time * wiggleSpeed) * perpendicularDirection * wiggleAmount;

        transform.position += offsetDirection * Time.deltaTime;


    }

    public override void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown && IsPlayerInAttackRange(attackRange))
        {
            //StopMoving();
            StartCoroutine(ShootXNumberTimes(numberOfShots));
            lastAttackTime = Time.time;
        }
        else 
        {
            Debug.Log("Ataque em CD");
        }
        
    }


    private IEnumerator ShootXNumberTimes(int x)
    {
        for (int i = 0; i < x; i++)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
       
    }

    private void Shoot()
    {
        if (projectilePrefab == null || shootPoint == null)
        {
            return;
        }
        // Instantiate the projectile
        GameObject projectileInstance =  Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        // Calculate the direction
        Vector3 directionToPlayer = (player.transform.position - shootPoint.position).normalized;
        //shooting logic here
        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        if (projectile != null)
        {
           // projectile.Setup(directionToPlayer, projectileLifetime, projectileSpeed, attackPower);
        }

        Debug.Log("Pew");
    }
}
