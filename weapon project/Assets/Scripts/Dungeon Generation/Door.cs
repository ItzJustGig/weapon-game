using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 myDestinationPos; // A posição central do quarto a que esta porta lida.
    private GameObject camera; // Referência à camera para mudar a posição dela quando player muda de quarto
    private GameObject player; // Referência ao player para mudar a posição dele quando mudar de quarto

    private void Start() {
        camera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            ChangeRooms();
        }
    }

    private void ChangeRooms()
    {
        camera.transform.position = new Vector3(myDestinationPos.x, myDestinationPos.y, -10);
        player.transform.position = myDestinationPos;
    }
}
