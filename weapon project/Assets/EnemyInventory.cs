using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyInventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<ActiveItem> actives;
    public List<PassiveItem> passives;

    [SerializeField]
    public float attackInterval = 0;

    private void Start()
    {
        //Ititialize the enemy's inventory

        //creates an instance of the items that it started with, and saves the instance. then rewrites the start list with
        //the list of instances and initializes the items
        List<ActiveItem> act = new List<ActiveItem>();

        foreach (ActiveItem item in actives)
        {
            ActiveItem m = Instantiate(item);
            act.Add(m);
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
            PassiveItem m = Instantiate(item);
            pass.Add(m);
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
        //wait for interval
        if (attackInterval > 0)
        {
            attackInterval -= Time.deltaTime;
        }
        else
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (actives.Count > 0)
        {
            //gets which direction the player is from the enemy
            Vector2 direction = FindAnyObjectByType<PlayerCtrl>().gameObject.transform.position - this.gameObject.transform.position;
            direction = direction.normalized;

            //uses the active
            actives[0].Active(direction);
            attackInterval = actives[0].cooldown;
        }
    }
}
