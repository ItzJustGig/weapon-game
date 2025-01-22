using System.Collections.Generic;
using UnityEngine;

public class HostileRoom : Room
{
    [SerializeField] int minEnemiesToSpawn = 2;
    [SerializeField] int maxEnemiesToSpawn = 5;
    [SerializeField] GameObject spawnPoints;
    [SerializeField] GameObject enemyCupHolder;

    [SerializeField] GameObject testEnemyPrefab;

    private List<Transform> spawnPointObjects = new List<Transform>();
    private int totalEnemiesInRoom = 0;

    private void Start() {
        /* 
            Algures haverá o nível atual, talvez uma variável global?
            Com base nisso decide-se quantos inimigos spawnar.
            Eventualmente no código do inimigo haverá algo semelhante para
            decidir o seu poder (stats, quantidade de spells conhecidas)
        */

        foreach (Transform spawnPoint in spawnPoints.transform) 
            spawnPointObjects.Add(spawnPoint);

        if (maxEnemiesToSpawn > spawnPointObjects.Count) maxEnemiesToSpawn = spawnPointObjects.Count; 
        // Visto que apago os spawn points da array, se houver mais inimigos a serem spawned do que 
        // spawn points existentes vai haver um erro de index.
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("Player's first visit, locking doors!");
            LockMyDoors();
            totalEnemiesInRoom = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn); // Por enquanto random
            SpawnEnemies(totalEnemiesInRoom);
            EventManager.OnEnemyKill += EnemyKilledHandler; // Subscreve ao evento do inimigo morrer
            Destroy(GetComponentInChildren<Collider2D>()); // Já não precisamos disto
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

    private void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(testEnemyPrefab, GetUniqueRandomSpawnpoint(), Quaternion.identity);
            enemy.transform.SetParent(enemyCupHolder.transform);
        } 
    }

    private Vector3 GetUniqueRandomSpawnpoint()
    {   // Escolhe uma posição random e remove essa da lista para não ser novamente utilizada
        Vector3 spawnPoint;
        int randomIndex = Random.Range(0, spawnPointObjects.Count - 1);

        spawnPoint = spawnPointObjects[randomIndex].transform.position;
        spawnPointObjects.RemoveAt(randomIndex);

        return spawnPoint;
    }

    private void EnemyKilledHandler()
    {   // Função executada ao sinal dum inimigo morto
        totalEnemiesInRoom--;
        if (totalEnemiesInRoom <= 0)
            RoomCleared();
    }

    private void RoomCleared()
    {
        Debug.Log("Room Cleared!");
        EventManager.OnEnemyKill -= EnemyKilledHandler; // Retira a subscrição
        UnlockMyDoors();
    }
    
}
