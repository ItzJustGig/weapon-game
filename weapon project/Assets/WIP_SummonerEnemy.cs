using UnityEngine;

public class SummonerEnemy : EnemyController
{
    public GameObject minionPrefab;
    public int maxMinions = 3;
    private int currentMinions = 0;

    public override void Attack()
    {
        if (currentMinions < maxMinions && Time.time - lastAttackTime >= attackCooldown)
        {
            SummonMinion();
            lastAttackTime = Time.time;
        }
    }

    private void SummonMinion()
    {
        Instantiate(minionPrefab, transform.position + Random.insideUnitSphere * 2, Quaternion.identity);
        currentMinions++;
    }

    public void MinionDestroyed()
    {
        currentMinions = Mathf.Max(0, currentMinions - 1);
    }
}
