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


    [Header("Stats")]
    public Stats bonusStats;

    [Header("Others")]
    [SerializeField]
    GameObject owner;
    [SerializeField]
    private GameObject dropItem;

    private float dropTimer = 1f;
    private float curDropTimer = 0f;

    private void Start()
    {
        owner = this.gameObject;
        //Ititialize the player's inventory

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
            item.SetOwner(owner);
            item.Initialize();
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
            item.SetOwner(owner);
            item.Initialize();
        }
    }

    private void Update()
    {
        Vector2 direction = new Vector2();

        //get where player is aiming
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        //use item actives if its not trying to drop
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

            //waits to get which item to drop, if its escape cancels
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

                //creates a item pedestal with the dropped item data
                GameObject drop = Instantiate(dropItem, temp, this.transform.rotation);
                drop.GetComponent<FloorItem>().ReplaceItem(actives[tmp]);

                //saves the instance of the dropped item, then destroys it
                ActiveItem toDiscard = actives[tmp];
                actives.Remove(actives[tmp]);
                Destroy(toDiscard.gameObject);

                //resets the drop timer
                curDropTimer = 0;
            }
        }
    }
}
