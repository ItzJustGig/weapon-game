using System.Collections.Generic;
using UnityEngine;

public class ThreeOfAKindProjectile : Projectile
{
    [SerializeField] float cardSpinSpeed = 360f; // Degrees per second
    
    public List<Sprite> red;
    public List<Sprite> black;

    static int cardCounter = 0; // To know when a new set of 3 is being spawned
    static int randomCardRowIndex;

    private SpriteRenderer spriteRenderer;

    private void Start() 
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        CountCards();
        
        PickRandomSuit();

        if (Random.value > 0.5f) cardSpinSpeed *= -1; // 50% de hípoteses para a carta rodar na direção inversa
        
    }
    
    private void Update() 
    {
        transform.Rotate(Vector3.forward * cardSpinSpeed * Time.deltaTime); // Spin to win!!
    }

    private void CountCards()
    {
        // Usado para verificar quando uma nova set de 3 cartas é lançada para estarem todos com o mesmo valor "three of a kind"
        cardCounter += 1;

        if (cardCounter == 4)
        {
            cardCounter = 1;
            randomCardRowIndex = Random.Range(0, red.Count);
        }
    }

    private void PickRandomSuit()
    { // 50/50 odds red or black suit
        spriteRenderer.sprite = (Random.value > 0.5f) ?  red[randomCardRowIndex] : black[randomCardRowIndex];
    }
}
