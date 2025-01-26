using UnityEngine;

public class PlayerHealth : MonoBehaviour
{  
    public float health, maxHealth;

    public void Heal(float amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        EventManager.Instance.OnDamage();
    }

    public void TakeDamage()
    {
        health -= 1;
        EventManager.Instance.OnDamage();
        if (health <= 0)
        {
            // Por enquanto vai destruir o objeto quando a quantidade de vida chegar a zero
            Destroy(gameObject);
        } 
    } 
}
