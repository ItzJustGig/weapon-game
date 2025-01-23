using UnityEngine;

public class ThreeOfAKindProjectile : Projectile
{
    [SerializeField] float cardSpinSpeed = 360f; // Degrees per second

    private void Start() {
        if (Random.value > 0.5f) cardSpinSpeed *= -1; // 50% de hípoteses para a carta rodar na direção inversa
    }
    
    private void Update() {
        transform.Rotate(Vector3.forward * cardSpinSpeed * Time.deltaTime); // Spin to win!!
    }
}
