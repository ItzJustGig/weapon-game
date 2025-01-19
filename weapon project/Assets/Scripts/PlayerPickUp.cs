using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Items")){
            FloorItem floorItem = collision.GetComponent<FloorItem>();
            this.gameObject.GetComponentInParent<PlayerInventory>().items.Add(floorItem.item);

            FindAnyObjectByType<EventManager>().PickUpItem();

            floorItem.item.OnPickUp();
            floorItem.item.Initialize();

            Destroy(collision.gameObject);
        }
    }
}
