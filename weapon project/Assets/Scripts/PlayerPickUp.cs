using TMPro;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private Animator textAnimator;
    [SerializeField]
    private TextMeshProUGUI textTitle;
    [SerializeField]
    private TextMeshProUGUI textDesc;

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

                textTitle.text = floorItem.item.name;
                textDesc.text = floorItem.item.desc;
                textAnimator.SetTrigger("pickup");

                floorItem.item.OnPickUp();
                floorItem.item.Initialize();

                Destroy(collision.gameObject);
            }
        }
    }
}
