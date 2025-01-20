using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Items")){
            bool cancel = false;
            FloorItem floorItem = collision.GetComponent<FloorItem>();

            if (floorItem.item is PassiveItem pas)
                this.gameObject.GetComponentInParent<PlayerInventory>().passives.Add(pas);
            else if (floorItem.item is ActiveItem act && this.gameObject.GetComponentInParent<PlayerInventory>().actives.Count < 4)
            {
                this.gameObject.GetComponentInParent<PlayerInventory>().actives.Add(act);
            }
            else if (this.gameObject.GetComponentInParent<PlayerInventory>().actives.Count >= 4)
            {
                 cancel = true;
            }

            if (!cancel)
            {
                FindAnyObjectByType<EventManager>().PickUpItem();

                floorItem.item.OnPickUp();
                floorItem.item.Initialize();

                Destroy(collision.gameObject);
            }
        }
    }
}
