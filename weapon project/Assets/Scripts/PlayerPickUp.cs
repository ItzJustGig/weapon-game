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
            if (collision.GetComponent<Collectable>())
            {
                switch (collision.GetComponent<Collectable>().type)
                {
                    case Collectable.Type.Coin:
                        this.gameObject.GetComponentInParent<PlayerInventory>().gold++;
                        break;
                    case Collectable.Type.Key:
                        this.gameObject.GetComponentInParent<PlayerInventory>().keys++;
                        break;
                    case Collectable.Type.Health:
                        this.gameObject.GetComponentInParent<PlayerHealth>().Heal(1);
                        break;
                }

                Destroy(collision.gameObject);
            }
            else
            {
                bool cancel = false;
                FloorItem floorItem = collision.GetComponent<FloorItem>();

                if (this.gameObject.GetComponentInParent<PlayerInventory>().actives.Count >= 4 && floorItem.item is ActiveItem)
                {
                    cancel = true;
                }

                if (floorItem is ShopItem shop && !cancel)
                {
                    if (shop.price <= this.gameObject.GetComponentInParent<PlayerInventory>().gold)
                    {
                        this.gameObject.GetComponentInParent<PlayerInventory>().gold -= shop.price;
                    }
                    else
                    {
                        cancel = true;
                    }
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

                    if (item is PassiveItem pas)
                    {
                        this.gameObject.GetComponentInParent<PlayerInventory>().passives.Add(pas);
                    }
                    else if (item is ActiveItem act)
                    {
                        this.gameObject.GetComponentInParent<PlayerInventory>().actives.Add(act);
                    }

                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
