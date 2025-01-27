using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Wander,

    Follow,

    Die,
};

public class EnemyController : MonoBehaviour
{

    public GameObject player;

    public EnemyState currState = EnemyState.Wander;

    public float range;
    public float attackRange;
    public float speed;
    public float originalSpeed;
    public float attackCooldown;
    public float abilityCooldown;
    public float lastAttackTime = 0f;
    public float lastAbilityTime = 0f;

    public bool ableToMove = true;

    private bool chooseDirection = false;
    private bool dead = false;
    private Vector3 randomDirection;

    public bool isCharging = false;


    //navmesh things
    [SerializeField] public Transform target;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalSpeed = speed;
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
            break;

            case (EnemyState.Die):
            break;
        }

        if(IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;

        }
        
        if(!IsPlayerInRange(range) && currState != EnemyState.Die && !isCharging)
        {
            currState = EnemyState.Wander;
        }
             
    }

    protected bool IsPlayerInRange(float range)
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
        yield return new WaitForSeconds(Random.Range(2f, 5f));
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
        //agent.SetDestination(target.position); // Move closer with navmesh

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
