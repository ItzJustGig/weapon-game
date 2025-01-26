using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            EventManager.Instance.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
