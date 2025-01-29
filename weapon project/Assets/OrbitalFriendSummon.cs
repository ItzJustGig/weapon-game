using UnityEngine;

public class OrbitalFriendSummon : MonoBehaviour
{
    GameObject owner;
    float rotatingSpeed;

    public void Initialize(GameObject owner, float rotatingSpeed)
    {
       this.owner = owner;
        this.rotatingSpeed = rotatingSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!owner)
        {
            Destroy(this);
            return;
        }

        gameObject.transform.position = owner.transform.position;
        Vector3 rt = new Vector3(0, 0, rotatingSpeed);
        gameObject.transform.Rotate(rt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Projectile" && collision.GetComponent<Projectile>().owner != owner) {
            if (collision.GetComponent<Projectile>().destroyOnContact)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
