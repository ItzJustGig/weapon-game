using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance { get; private set; }

    public List<AttackQueueObject> attackQueue;

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

    private void Update()
    {
        for (int i = attackQueue.Count - 1; i >= 0; i--)
        {
            AttackQueueObject atk = attackQueue[i];
            atk.delay -= Time.deltaTime;

            if (atk.owner == null) {
                attackQueue.RemoveAt(i);
                break;
            }

            if (atk.delay <= 0)
            {
                if (atk.owner != null)
                    EventManager.Instance.BulletFired(atk.Spawn(0, 0, 0));
                attackQueue.RemoveAt(i);
            }
        }
    }
}
