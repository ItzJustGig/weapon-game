using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ChargerEnemy : EnemyController
{
    
    public float windUpTime = 0.5f; // Time the enemy winds up before charging
    public float overshootDistance = 1f; // Distance to overshoot past the player's position
    public float chargeSpeedMultiplier = 3f;
    public float chargeDuration = 1.5f;
    public float deceleration = 5f; // Rate at which the enemy slows down after the charge

    private Vector2 targetPosition;// Position to charge toward



    public override void Follow()
    {
        if (!isCharging)
        {
            base.Follow();
        }

        if (IsPlayerInAttackRange(attackRange)) {
            Attack();        
        }
    }

    public override void Attack()
    {
        if (!isCharging && Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.Log("Preparing to charge...");
            StartCoroutine(Charge());
            lastAttackTime = Time.time;
        }
        else
        {
            Debug.Log("Charge on cooldown");
        }
    }

    private IEnumerator Charge()
    {

        // Wind-up phase
        isCharging = true;
        Vector2 targetPosition = player.transform.position; // Lock the player's current position
        yield return new WaitForSeconds(windUpTime);

        // Calculate the direction and overshoot
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 finalPosition = targetPosition + direction * overshootDistance;

        

        float originalSpeed = speed;
        speed *= chargeSpeedMultiplier;

        Vector2 velocity = direction * speed;

        // Charge towards the final position
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;
        float acceleration = 2f; // Adjust this for faster/slower acceleration
        float currentLerpTime = 0f; // Tracks the interpolated "Lerp time" with acceleration

        /* charge without sliding 
        while (elapsedTime < chargeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Increase Lerp time gradually to simulate acceleration
            currentLerpTime += acceleration * Time.deltaTime;

            // Clamp Lerp time to the range [0, 1] for Lerp
            float lerpFactor = Mathf.Clamp01(currentLerpTime / chargeDuration);


            transform.position = Vector2.Lerp(startPosition, finalPosition, lerpFactor);
            yield return null;
        }
        */

        while(velocity.magnitude > 0.1f) {

            transform.position += (Vector3)(velocity * Time.deltaTime);
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.deltaTime);
            yield return null;
        }
        

        // Reset speed and state
        speed = originalSpeed;
        isCharging = false;

        Debug.Log("Charge complete");

        /* old charge
        
        isCharging = true;
        float originalSpeed = speed;
        speed *= chargeSpeedMultiplier;

        yield return new WaitForSeconds(chargeDuration);

        speed = originalSpeed;
        isCharging = false;*/
    }
}
