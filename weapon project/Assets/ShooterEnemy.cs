using System.Collections;
using UnityEngine;

public class ShooterEnemy : EnemyController
{
    public float attackPower = 10f;
    public int numberOfShots = 3;
    public float timeBetweenShots = 1;

    public override void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown && IsPlayerInAttackRange(attackRange))
        {
            StopMoving();
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
        //shooting logic here
        Debug.Log("Pew");
    }
}
