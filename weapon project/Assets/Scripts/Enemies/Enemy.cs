using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public float maxHealth = 5;

    private float currentHealth;

    private void Start()   
    {
        currentHealth = maxHealth;
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Projectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>(); // Referencia à bala que bateu neste indivíduo
            
            if (projectile.owner != this.gameObject) // Outros inimigos podem dar dano a inimigos? Mas não a eles próprios
                TakeDamage(projectile.damage);
        }
    }*/

    public void TakeDamage(float amount)
    {
        Debug.Log($"{name} took {amount} damage and has {currentHealth} HP.");
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EventManager.Instance.EnemyKilled(); // Avisa o peeps que morri
        Destroy(this.gameObject);
    }
}
