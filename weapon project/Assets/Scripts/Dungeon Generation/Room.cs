using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;

    public Vector2Int RoomIndex { get; set; }


    public void OpenDoor(Vector2Int doorDirection) 
    {
        
        if (doorDirection == Vector2Int.up) {
            topDoor.SetActive(true);
            return;
        }

        if (doorDirection == Vector2Int.right) {
            rightDoor.SetActive(true);
            return;
        }

        if (doorDirection == Vector2Int.down) {
            bottomDoor.SetActive(true);
            return;
        }

        if (doorDirection == Vector2Int.left) {
            leftDoor.SetActive(true);
            return;
        }

        Debug.Log("Invalid door direction: " + doorDirection);
        
    }
}
