using UnityEngine;

public class PlayerHealth : MonoBehaviour
{  
    public float health, maxHealth;

    public void TakeDamage(float amount)
    {
        health -= amount;
        EventManager.Instance.OnDamage();
        if (health <= 0)
        {
            // Por enquanto vai destruir o objeto quando a quantidade de vida chegar a zero
            Destroy(gameObject);
        } 
    } 
}
