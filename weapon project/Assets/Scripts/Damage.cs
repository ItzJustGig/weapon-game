using UnityEngine;

public class Damage : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido é o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
           collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
    } 
}
