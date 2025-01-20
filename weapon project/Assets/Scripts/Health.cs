using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    // Número de Corações
    public int numOfHearts;

    // Imagem de corações
    public Image[] hearts;
    // Sprites de vida
    public Sprite fullHearts;
    public Sprite emptyHearts;
    

    void Update()
    {
    }

    void UpdateHealth()
    {
        // Garante que o valor de health não exceda numOfHearts
        health = Mathf.Clamp(health, 0, numOfHearts);

        for (int i = 0; i < hearts.Length; i++)
        {
            // Atualiza o sprite baseado na vida
            hearts[i].sprite = i < health ? fullHearts : emptyHearts;

            // Ativa e desativa corações com base no número máximo de corações
            hearts[i].enabled = i < numOfHearts;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateHealth();

        if (health <= 0)
        {
            // Por enquanto vai destruir o objeto quando a quantidade de vida chegar a zero
            Destroy(gameObject);
        }
    }

}
