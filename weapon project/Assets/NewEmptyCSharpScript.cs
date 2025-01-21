using UnityEngine;

public class MeleeEnemy1 : EnemyController
{
    public float attackPower = 20f;

    public override void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Ataquei");
            lastAttackTime = Time.time;
        }
        else 
        {
            Debug.Log("Ataque em CD");
        }
        
    }
}
