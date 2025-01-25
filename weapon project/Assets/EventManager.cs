using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    /*
     * HOW TO USE
     * ---
     * NESTE SCRIPT
     * public static event Action EventName; <- criar evento
     * 
     * public void EventTriggered() <- criar uma funcao neste script para dar trigger ao evento
     * {
     *     EventName?.Invoke();
     * }
     * 
     * ---
     * ONDE SE QUER QUE O EVENTO SE APLIQUE (ver ElectricBullet.cs para exemplo)
     * EventManager.EventName += Funcao; <- dar attach de uma funcao ao evento, pode ser corrido num Start or similar, 
     *                                      mas tem de correr esta parte do codigo.
     * EventManager.EventName -= Funcao; <- num OnDestroy correr este codigo, pra evitar leaks de memoria
     * 
     * public void Funcao(){
     *   *CODIGO QUE VAI CORRER QUANDO O EVENTO EVENTNAME DER TRIGGER*
     * }
     * ---
    */

    public static EventManager Instance { get; private set; }

    public static event Action OnItemPickedUp;
    public static event Action<GameObject> OnBulletFired;
    public static event Action OnEnterNewRoom;
    public static event Action OnExitRoom;
    public static event Action OnBossKill;
    public static event Action OnEnemyKill;
    public static event Action OnPlayerDamaged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign the current instance
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Enforce Singleton pattern
        }
    }

    public void BulletFired(GameObject proj)
    {
        if (proj != null)
            OnBulletFired?.Invoke(proj);
    }
    
    public void PickUpItem()
    {
        OnItemPickedUp?.Invoke();
    }

    public void EnemyKilled()
    {
        OnEnemyKill?.Invoke();
    }

    public void OnDamage()
    {
        OnPlayerDamaged?.Invoke();
    }
}
