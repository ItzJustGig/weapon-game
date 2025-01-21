using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemieBehaviour : MonoBehaviour
{

    public GameObject player; // este vai ser o target do enemie
    public float speed; // velocidade com que o enemie se move
    public float directionChangeInterval = 2f; // intervalo para mudar de direção
    public float idleEnemieTimeInterval = 3f; // tempo parado
    public float movingEnemieTimeInterval = 2f; // tempo a mexer-se
    public float AggroRange;

    private float distance; // variavel utilizada para calcular a distancia entre este objecto e o seu target(player)

    private Vector2 randomDirection; // Variavel para guardar a direção random que este objecto vai tomar

    //Timers
    private float timeSinceLastDirectionChange = 0;
    private float timeIdle = 0;
    private float timeMoving = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    timeSinceLastDirectionChange = 0;
        timeIdle = 0;
        timeMoving = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Timers Update Here
        timeSinceLastDirectionChange += Time.deltaTime;
        //Debug.Log("Current timeSinceLastDirectionChange: " + timeSinceLastDirectionChange);

        //timeIdle += Time.deltaTime;
        //Debug.Log("Current timeIdle: " + timeIdle);

        //timeMoving += Time.deltaTime;
        //Debug.Log("Current timeMoving: " + timeMoving);
        //Timers Update here ends



        distance = Vector2.Distance(transform.position, player.transform.position); // Adquirir a distancia entre os objectos
        Vector2 direction = player.transform.position - transform.position; // Calculo do vector direcional deste objecto ao seu target (player)
        direction.Normalize();

        float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculo do angulo entre dois pontos, aqui usamos a variavel direction para nos orientarmos na sua direção
        float randomAngle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;

        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            ChangeRandomDirection();
            timeSinceLastDirectionChange = 0;
        }

        if (distance < AggroRange)
        {
            Debug.Log("I See player");
            PlayerInAggroRange(playerAngle);
            timeMoving = 6;
            timeIdle = 0;
        }
        else
        {
            if (timeMoving < 6)
            {
                IdleMovement(randomAngle);
            }
            else
            {
                Debug.Log("I'm iddle");

                timeIdle += Time.deltaTime;
                if (timeIdle > 3)
                {
                    timeMoving = 0;
                    timeIdle = 0;
                }
            }
        }

        
        
       // Debug.Log("timeIdle: "+timeIdle);
       // Debug.Log("timeMoving: " + timeMoving);
    }

    private void ChangeRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        float radians = randomAngle * Mathf.Deg2Rad;

        randomDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

        
    }

    private void PlayerInAggroRange(float angle)
    {
        Debug.Log("I See player");
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void IdleMovement(float angle)
    {
        transform.position = Vector2.MoveTowards(
                                                    this.transform.position,
                                                    (Vector2)this.transform.position + randomDirection, //fazemos um cast para garantir que continuamos a ter um vector2 atribute
                                                    speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        timeMoving += Time.deltaTime;

    }

}
