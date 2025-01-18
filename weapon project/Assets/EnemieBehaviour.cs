using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemieBehaviour : MonoBehaviour
{

    public GameObject player; // este vai ser o target do enemie
    public float speed; // velocidade com que o enemie se move
    public float directionChangeInterval = 2f; // intervalo para mudar de direção
    public float AggroRange;

    private float distance; // variavel utilizada para calcular a distancia entre este objecto e o seu target(player)

    private Vector2 randomDirection; // Variavel para guardar a direção random que este objecto vai tomar
    private float timeSinceLastDirectionChange = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceLastDirectionChange += Time.deltaTime;

        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            ChangeRandomDirection();
            timeSinceLastDirectionChange = 0;
        }

        distance = Vector2.Distance(transform.position, player.transform.position); // Adquirir a distancia entre os objectos
        Vector2 direction = player.transform.position - transform.position; // Calculo do vector direcional deste objecto ao seu target (player)
        direction.Normalize();

        float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculo do angulo entre dois pontos, aqui usamos a variavel direction para nos orientarmos na sua direção
        float randomAngle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;
        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);




        if (distance < AggroRange)
        {
            Debug.Log("I See player");
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * playerAngle);
        }
        else
        {
            Debug.Log("Where is that guy?");
            
            transform.position = Vector2.MoveTowards(
                                                    this.transform.position,
                                                    (Vector2)this.transform.position + randomDirection, //fazemos um cast para garantir que continuamos a ter um vector2 atribute
                                                    speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * randomAngle);
        }

        Debug.Log("Current distance: " + distance);
    }

    private void ChangeRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        float radians = randomAngle * Mathf.Deg2Rad;

        randomDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

        
    }
}
