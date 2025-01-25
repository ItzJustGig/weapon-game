using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyInventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<ActiveItem> actives;
    public List<PassiveItem> passives;

    [SerializeField]
    private GameObject activeItemsGO;
    [SerializeField]
    private GameObject passiveItemsGO;

    [SerializeField]
    bool generateInventory = false;
    [SerializeField]
    float attackInterval = 0;

    private void Start()
    {
        if (generateInventory)
        {
            int activeItems = Random.Range(1, 4 + 1);
            int passiveItems = Random.Range(0, 2 + 1);

            for (int i = 0; i < activeItems; i++)
            {
                Item it = activeItemsGO.GetComponent<ItemList>().items[Random.Range(0, activeItemsGO.GetComponent<ItemList>().items.Count)];

                if (it.isEnemyCompatible)
                    actives.Add(it as ActiveItem);
                else
                    i--;
            }

            for (int i = 0; i < passiveItems; i++)
            {
                Item it = passiveItemsGO.GetComponent<ItemList>().items[Random.Range(0, passiveItemsGO.GetComponent<ItemList>().items.Count)];

                if (it.isEnemyCompatible)
                    passives.Add(it as PassiveItem);
                else
                    i--;
            }
        }
        
        //Ititialize the enemy's inventory

        //creates an instance of the items that it started with, and saves the instance. then rewrites the start list with
        //the list of instances and initializes the items
        List<ActiveItem> act = new List<ActiveItem>();

        foreach (ActiveItem item in actives)
        {
            if (item.isEnemyCompatible)
            {
                ActiveItem m = Instantiate(item);
                act.Add(m);
            }
        }

        actives = act;

        foreach (ActiveItem item in actives)
        {
            item.gameObject.SetActive(false);
            item.Initialize();
            item.SetOwner(this.gameObject);
        }

        List<PassiveItem> pass = new List<PassiveItem>();

        foreach (PassiveItem item in passives)
        {
            if (item.isEnemyCompatible)
            {
                PassiveItem m = Instantiate(item);
                pass.Add(m);
            }
        }

        passives = pass;

        foreach (PassiveItem item in passives)
        {
            item.gameObject.SetActive(false);
            item.Initialize();
            item.SetOwner(this.gameObject);
        }
    }

    private void Update()
    {
        //temporary, will be replaced by the enemy's logic in another script
        if (attackInterval > 0)
        {
            attackInterval -= Time.deltaTime;
        }
        else
        {
            Attack(0);
        }
    }

    public void Attack(int itemId)
    {
        if (actives.Count > 0 && actives[itemId])
        {
            //gets which direction the player is from the enemy
            Vector2 direction = FindAnyObjectByType<PlayerCtrl>().gameObject.transform.position - this.gameObject.transform.position;
            direction = direction.normalized;

            //uses the active
            actives[itemId].Active(direction);
            attackInterval = actives[itemId].cooldown;
        }
    }
}
