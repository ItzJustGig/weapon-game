using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public List<AttackQueueObject> attackQueue;

    private void Update()
    {
        for (int i = attackQueue.Count - 1; i >= 0; i--)
        {
            AttackQueueObject atk = attackQueue[i];
            atk.delay -= Time.deltaTime;

            if (atk.delay <= 0)
            {
                FindAnyObjectByType<EventManager>().BulletFired(atk.Spawn(0, 0, 0));
                attackQueue.RemoveAt(i);
            }
        }
    }
}
