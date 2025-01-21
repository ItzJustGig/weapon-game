using UnityEngine;

[System.Serializable]
public class Stats
{
    public float hp;
    public float damage;
    public float movSpeed;

    public float attackSpeed;
    public float projectileSpeed;
    public float projectileLifeTime;
    public float projectileTravelSpeed;

    public void AddStats(Stats bonus)
    {
        hp += bonus.hp;
        damage += bonus.damage;
        movSpeed += bonus.movSpeed;
        attackSpeed += bonus.attackSpeed;
        projectileSpeed += bonus.projectileSpeed;
        projectileLifeTime += bonus.projectileLifeTime;
        projectileTravelSpeed += bonus.projectileTravelSpeed;
    }
}
