using UnityEngine;

public class ThreeCardItem : ActiveItem
{
    [Header("Special Stats")]
    public float lifeTime;
    public float travelSpeed;
    public float damage;
    public float adjacentCardsAngleOffset = 15f; // Ângulo de offset das 2 cartas adicionais
    public GameObject card;

    public override void Active(Vector2 direction)
    {
        if (cooldown <= 0)
        {
            Vector2 directionAbove = RotateVector(direction, adjacentCardsAngleOffset);
            Vector2 directionBelow = RotateVector(direction, -adjacentCardsAngleOffset);

            AttackManager.Instance.attackQueue.Add(new AttackQueueObject(0f, lifeTime, travelSpeed, damage, card, owner, directionAbove));
            AttackManager.Instance.attackQueue.Add(new AttackQueueObject(0f, lifeTime, travelSpeed, damage, card, owner, direction));
            AttackManager.Instance.attackQueue.Add(new AttackQueueObject(0f, lifeTime, travelSpeed, damage, card, owner, directionBelow));

            cooldown = maxCooldown;
        }
    }

    Vector2 RotateVector(Vector2 v, float degrees)
    {   // Roda um vetor numa direção baseado num ângulo
        float radians = degrees * Mathf.Deg2Rad; // Convert degrees to radians
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        // Perform the rotation
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }

}
