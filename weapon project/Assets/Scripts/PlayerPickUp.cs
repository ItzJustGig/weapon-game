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
            {
                PassiveItem temp = pas;
                this.gameObject.GetComponentInParent<PlayerInventory>().passives.Add(temp);
            }
            else if (floorItem.item is ActiveItem act && this.gameObject.GetComponentInParent<PlayerInventory>().actives.Count < 4)
            {
                ActiveItem temp = act;
                this.gameObject.GetComponentInParent<PlayerInventory>().actives.Add(temp);
            }
            else if (this.gameObject.GetComponentInParent<PlayerInventory>().actives.Count >= 4)
            {
                 cancel = true;
            }

            if (!cancel)
            {
                EventManager.Instance.PickUpItem();

                textTitle.text = floorItem.item.name;
                textDesc.text = floorItem.item.desc;
                textAnimator.SetTrigger("pickup");

                Item item = Instantiate(floorItem.item);
                item.gameObject.SetActive(false);
                item.OnPickUp();
                item.Initialize();
                item.SetOwner(this.gameObject.transform.parent.gameObject);

                Destroy(collision.gameObject);
            }
        }
    }
}
