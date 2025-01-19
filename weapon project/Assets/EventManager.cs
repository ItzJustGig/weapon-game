using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnItemPickedUp;
    public static event Action<GameObject> OnBulletFired;
    public static event Action OnEnterNewRoom;
    public static event Action OnExitRoom;
    public static event Action OnBossKill;
    public static event Action OnEnemyKill;

    public void BulletFired(GameObject proj)
    {
        OnBulletFired?.Invoke(proj);
    }

    public void PickUpItem()
    {
        OnItemPickedUp?.Invoke();
    }
}
