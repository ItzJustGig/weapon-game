using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyInventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<ActiveItem> actives;
    public List<PassiveItem> passives;

    List<ActiveItem> act;
    List<PassiveItem> pass;

    [SerializeField]
    public float attackInterval = 0;

    private void Start()
    {
        //Ititialize the player's inventory
        foreach (ActiveItem item in actives.ToArray())
        {
            act.Add(item);
        }

        foreach (ActiveItem item in act)
        {
            item.Initialize();
            item.SetOwner(this.gameObject);
        }

        foreach (PassiveItem item in passives.ToArray())
        {
            pass.Add(item);
        }

        foreach (PassiveItem item in pass)
        {
            item.Initialize();
        }
    }

    private void Update()
    {
        if (attackInterval > 0)
        {
            attackInterval -= Time.deltaTime;
        }
        else
        {
            Vector2 direction = FindAnyObjectByType<PlayerCtrl>().gameObject.transform.position- this.gameObject.transform.position;
            direction = direction.normalized;

            actives[0].Active(direction);
            attackInterval = actives[0].cooldown;
        }
    }
}
