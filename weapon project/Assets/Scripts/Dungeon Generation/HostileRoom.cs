using System.Collections.Generic;
using UnityEngine;

public class HostileRoom : Room
{
    private bool hasBeenCleared = false;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("Player entered room "+ name);
            if (!hasBeenCleared)
            {
                Debug.Log("Player's first visit, locking doors!");
                LockMyDoors();
            }
        }
    }

    private void LockMyDoors()
    {
        if (topDoor.activeInHierarchy) topDoor.GetComponent<Door>().ToggleLock(false);
        if (rightDoor.activeInHierarchy) rightDoor.GetComponent<Door>().ToggleLock(false);
        if (bottomDoor.activeInHierarchy) bottomDoor.GetComponent<Door>().ToggleLock(false);
        if (leftDoor.activeInHierarchy) leftDoor.GetComponent<Door>().ToggleLock(false);
    }

    private void UnlockMyDoors()
    {
        if (topDoor.activeInHierarchy) topDoor.GetComponent<Door>().ToggleLock(true);
        if (rightDoor.activeInHierarchy) rightDoor.GetComponent<Door>().ToggleLock(true);
        if (bottomDoor.activeInHierarchy) bottomDoor.GetComponent<Door>().ToggleLock(true);
        if (leftDoor.activeInHierarchy) leftDoor.GetComponent<Door>().ToggleLock(true);
    }
    
}
