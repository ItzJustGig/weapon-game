using System.Collections.Generic;
using UnityEngine;

public class ChainAddon : MonoBehaviour
{
    float chainRange;
    [SerializeField]
    float chainTimes;
    float dmgReduc;
    float bonusProjSpeed;

    [SerializeField]
    List<GameObject> hitTargets = new List<GameObject>();

    public void Initialize(float chainRange, float chainTimes, float dmgReduc, float bonusProjSpeed)
    {
        this.chainRange = chainRange;
        this.chainTimes = chainTimes;
        this.dmgReduc = dmgReduc;
        this.bonusProjSpeed = bonusProjSpeed;
    }

    private void Start()
    {
        EventManager.OnBulletHitEnemy += Chain;
        this.gameObject.GetComponent<Projectile>().destroyOnContact = false;
    }

    private void OnDestroy()
    {
        EventManager.OnBulletHitEnemy -= Chain;
    }

    public GameObject GetClosestTarget()
    {
        List<GameObject> objectsInRadius = new List<GameObject>();

        // Perform a sphere overlap to find colliders within the radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitTargets[hitTargets.Count-1].transform.position, chainRange);

        // Add the game objects of the colliders to the list
        foreach (Collider2D col in colliders)
        {
            if (!hitTargets.Contains(col.gameObject) && col.gameObject != this.gameObject && col.tag == "Enemy")
            {
                objectsInRadius.Add(col.gameObject);
                Debug.Log(col.gameObject.transform.position);
            }
        }

        // Sort the list by distance to this object
        objectsInRadius.Sort((a, b) =>
        {
            float distA = Vector3.Distance(transform.position, a.transform.position);
            float distB = Vector3.Distance(transform.position, b.transform.position);
            return distA.CompareTo(distB); // Compare distances
        });

        if (objectsInRadius.Count > 0)
            return objectsInRadius[0];

        return null;
    }

    private void Chain(GameObject target, GameObject proj)
    {
        if (proj == this.gameObject)
        {
            if (!hitTargets.Contains(target))
                hitTargets.Add(target.gameObject);

            chainTimes--;
            if (chainTimes <= 0)
            {
                Destroy(this.gameObject);
            }
            else if (chainTimes == 2)
            {
                proj.GetComponent<Projectile>().travelSpeed += proj.GetComponent<Projectile>().travelSpeed * bonusProjSpeed;
            }

            GameObject closeTarget = GetClosestTarget();

            if (closeTarget == null)
            {
                Destroy(proj);
                return;
            }

            proj.GetComponent<Projectile>().direction = closeTarget.transform.position - this.gameObject.transform.position;
            proj.GetComponent<Projectile>().lifeTime += 2f;
            proj.GetComponent<Projectile>().damage -= proj.GetComponent<Projectile>().damage * dmgReduc;
        }
    }
}
