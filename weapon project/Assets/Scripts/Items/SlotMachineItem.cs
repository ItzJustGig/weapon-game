using UnityEngine;

public class SlotMachineItem : ActiveItem
{
    [Header("Special Stats")]
    public float minGold;
    public float maxGold;
    public float minKeys;
    public float maxKeys;
    public GameObject enemy;

    public override void Active(Vector2 direction)
    {
        //Direction is not used here
        if (cooldown <= 0)
        {
            int result = Random.Range(0, 4 + 1);

            switch (result)
            {
                case 1:
                    FindAnyObjectByType<PlayerInventory>().gold += (int)Random.Range(minGold, maxGold + 1);
                    break;
                case 2:
                    FindAnyObjectByType<PlayerInventory>().keys += (int)Random.Range(minKeys, maxKeys + 1);
                    break;
                case 3:
                    Instantiate(enemy, transform.position, transform.rotation);
                    break;
            }
            cooldown = maxCooldown;
        }
    }
}
