using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Door : MonoBehaviour
{
    [SerializeField] static private float cameraMoveSpeed = 125.0f;
    public Vector3 myDestinationPos; // A posição central do quarto a que esta porta lida.
    static private GameObject camera; // Referência à camera para mudar a posição dela quando player muda de quarto
    static private GameObject player; // Referência ao player para mudar a posição dele quando mudar de quarto
    private bool moveCamera = false;

    private void Start() {
        camera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
    }

    private void Update() 
    {
        if (moveCamera)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position , new Vector3(myDestinationPos.x, myDestinationPos.y, -10), cameraMoveSpeed * Time.deltaTime);
            
            if (camera.transform.position == new Vector3(myDestinationPos.x, myDestinationPos.y, -10))
                moveCamera = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            ChangeRooms();
        }
    }

    private void ChangeRooms()
    {
        moveCamera = true;
        //camera.transform.position = new Vector3(myDestinationPos.x, myDestinationPos.y, -10);
        player.transform.position = myDestinationPos;
    }
}
