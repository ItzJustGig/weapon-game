using System.Collections;
using UnityEngine;


public enum EnemyState
{
    Wander,

    Follow,

    Die,
};

public class EnemyController : MonoBehaviour
{

    GameObject player;

    public EnemyState currState = EnemyState.Wander;

    public float range;
    public float attackRange;
    public float speed;
    public float attackCooldown;
    public float lastAttackTime = 0f;

    public bool ableToMove = true;

    private bool chooseDirection = false;
    private bool dead = false;
    private Vector3 randomDirection;
    




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {

            case (EnemyState.Wander):
                Wander();
            break;

            case (EnemyState.Follow):
                Follow();
                if (IsPlayerInAttackRange(attackRange))
                {
                    Attack();
                }
            break;

            case (EnemyState.Die):
            break;
        }

        if(IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;

        }else if(!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }
             
    }

    private bool IsPlayerInRange(float range)
    {
        
        return Vector3.Distance(transform.position, player.transform.position) <= range;

    }

    protected bool IsPlayerInAttackRange(float attackRange)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= attackRange;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDirection = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDirection = false;
    }
    public virtual void Wander() 
    {
        if (!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

        if (IsPlayerInRange(range))
        {
            currState = EnemyState.Follow;        
        }

    }
    public virtual void Follow()
    {

        if(ableToMove)
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }

    public virtual void StopMoving()
    {
        ableToMove = false;
    }
    public virtual void ResumeMoving()
    {
        ableToMove = true;
    }

    public virtual void Attack()
    {
    
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
