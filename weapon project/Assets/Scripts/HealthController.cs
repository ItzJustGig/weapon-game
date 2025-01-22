using UnityEngine;
using System.Collections.Generic;

public class HealthController : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth health; // Classe player que contem vida atual e máxima
    private List<Health> hearts = new List<Health>();
    
    private void OnEnable()
    {
        EventManager.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        // Calcula quantos corações são necessários
        float maxHealthRemainder = health.maxHealth % 2; // Use a propriedade maxHealth do objeto health
        int heartsToMake = (int)((health.maxHealth / 2) + maxHealthRemainder);

        // Aqui ele faz os corações
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int HeartStatusRemainder = (int)Mathf.Clamp(health.health - (i*2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)HeartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        Health heartComponent = newHeart.GetComponent<Health>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hearts = new List<Health>();
    }
}
