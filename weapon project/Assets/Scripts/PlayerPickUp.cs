using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Items")){
            this.gameObject.GetComponentInParent<PlayerInventory>().items.Add(collision.GetComponent<FloorItem>().item);
            Destroy(collision.gameObject);
        }
    }
}
