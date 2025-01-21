using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 2;
    private Health playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido é o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtém o componente Health do jogador
            if (playerHealth == null)
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
            }
        
            // Aplica dano, se o componente Health foi encontrado
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
