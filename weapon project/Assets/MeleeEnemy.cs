using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : EnemyController
{

    public float burstSpeedMultiplier = 2f; // How much faster the enemy moves during the burst
    public float burstDuration = 3f; // How long the speed burst lasts
    private bool isBursting = false; // Tracks whether the enemy is in its speed burst phase
    


    
    public override void Follow()
    {
        if (!isCharging)
        {
            base.Follow();
        }

        // Check if the player is in spotting range and not already bursting
        if (IsPlayerInRange(range) && !isBursting && currState != EnemyState.Die)
        {
            if (Time.time - lastAbilityTime >= abilityCooldown)
            {
                Debug.Log("ability used at" + Time.time);
                lastAbilityTime = Time.time;
                StartCoroutine(SpeedBurst());
            }
            
        }

        if (IsPlayerInAttackRange(attackRange))
        {
            Attack();
        }
    }

    public override void Wander()
    {
        //ResetSpeed();
        base.Wander();
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.Log("Melee attack!");
            lastAttackTime = Time.time;

            // Implement your melee attack logic here (e.g., damage the player)
        }
        else
        {
            Debug.Log("Attack on cooldown.");
        }
    }

    private IEnumerator SpeedBurst()
    {
        isBursting = true;

        // Increase the speed
        float originalSpeed = speed;
        speed = speed * burstSpeedMultiplier;

        Debug.Log("Speed burst activated!");

        // Wait for the burst duration
        yield return new WaitForSeconds(burstDuration);

        // Reset the speed
        ResetSpeed();

        Debug.Log("Speed burst ended.");
    }

    private void ResetSpeed()
    {
        speed = originalSpeed;
        isBursting = false;
    }


}
