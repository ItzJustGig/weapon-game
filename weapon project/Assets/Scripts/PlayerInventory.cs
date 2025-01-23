using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.ShaderData;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public float gold;
    public float keys;
    public List<ActiveItem> actives;
    public List<PassiveItem> passives;

    List<ActiveItem> act;
    List<PassiveItem> pass;

    [Header("Stats")]
    public Stats bonusStats;

    [Header("Others")]
    [SerializeField]
    private GameObject dropItem;

    private float dropTimer = 1f;
    private float curDropTimer = 0f;

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
        Vector2 direction = new Vector2();

        // Find the player's position
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (curDropTimer < dropTimer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && (actives.Count >= 1 && actives[0]))
            {
                actives[0].Active(direction);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && (actives.Count >= 2 && actives[1]))
            {
                actives[1].Active(direction);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && (actives.Count >= 3 && actives[2]))
            {
                actives[2].Active(direction);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && (actives.Count >= 4 && actives[3]))
            {
                actives[3].Active(direction);
            }
        }

        if (Input.GetKey(KeyCode.G))
        {
            curDropTimer += Time.deltaTime;
        } else if (curDropTimer > 0 && curDropTimer < dropTimer)
        {
            curDropTimer = 0;
        } else if (curDropTimer >= dropTimer)
        {
            Debug.Log("READY TO DROP");

            int tmp = -1;
            if (Input.GetKeyDown(KeyCode.Alpha1) && (actives.Count >= 1 && actives[0]))
            {
                tmp = 0;
            }else if(Input.GetKeyDown(KeyCode.Alpha2) && (actives.Count >= 2 && actives[1]))
            {
                tmp = 1;
            }else if(Input.GetKeyDown(KeyCode.Alpha3) && (actives.Count >= 3 && actives[2]))
            {
                tmp = 2;
            }else if(Input.GetKeyDown(KeyCode.Alpha4) && (actives.Count >= 4 && actives[3]))
            {
                tmp = 3;
            }else if (Input.GetKeyDown(KeyCode.Escape))
            {
                tmp = -1;
                curDropTimer = 0;
            }

            if (tmp != -1) {
                Vector3 temp = this.transform.position;
                temp.y += 1.1f;
                GameObject drop = Instantiate(dropItem, temp, this.transform.rotation);
                drop.GetComponent<FloorItem>().item = actives[tmp];
                actives.Remove(actives[tmp]);
                drop.GetComponent<FloorItem>().ForceStart();
                curDropTimer = 0;
            }
        }
    }
}
