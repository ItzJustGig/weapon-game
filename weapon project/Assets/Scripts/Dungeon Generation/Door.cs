using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Door : MonoBehaviour
{
    [SerializeField] static private float cameraMoveSpeed = 125.0f;
    public Vector3 myDestinationPos; // A posição central do quarto a que esta porta lida.
    static private GameObject mainCamera; // Referência à camera para mudar a posição dela quando player muda de quarto
    static private GameObject minimapCamera; // Referência à camera do minimapa para mudar a posição dela quando player muda de quarto
    static private GameObject player; // Referência ao player para mudar a posição dele quando mudar de quarto
    private bool moveCameras = false;

    private void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        minimapCamera = GameObject.FindWithTag("Minimap");
        player = GameObject.FindWithTag("Player");
    }

    private void Update() 
    {
        if (moveCameras)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position , new Vector3(myDestinationPos.x, myDestinationPos.y, -10), cameraMoveSpeed * Time.deltaTime);
            minimapCamera.transform.position = Vector3.MoveTowards(minimapCamera.transform.position , new Vector3(myDestinationPos.x, myDestinationPos.y, -10), cameraMoveSpeed * Time.deltaTime);

            if (minimapCamera.transform.position == new Vector3(myDestinationPos.x, myDestinationPos.y, -10))
                moveCameras = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            ChangeRooms();
        }
    }

    public void ToggleLock(bool open)
    {
        GetComponentInChildren<Collider2D>().enabled = false;
    }

    private void ChangeRooms()
    {
        moveCameras = true;
        player.transform.position = myDestinationPos;
    }

}
