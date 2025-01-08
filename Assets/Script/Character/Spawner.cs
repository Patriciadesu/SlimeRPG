using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRange;
    public float spawnDelay;
    public List<string> enemyIDs;
    public int maxNearbyEnemy;
    public float triggerZoneRange;
    public List<Enemy> storedEnemies = new List<Enemy>();
    private bool canSpawn = true; // Flag to control spawning
    public bool isInRange = false;

    public IEnumerator SpawnEnemy()
    {
        while (storedEnemies.Count < maxNearbyEnemy && isInRange)
        {
            if (canSpawn)
            {
                canSpawn = false; // Set the flag to prevent spawning during the delay
                Vector2 randomSpawnPosition = CalculateSpawnPosition();
                int randomEnemy = Random.Range(0, enemyIDs.Count);

                Enemy enemyspawning = SpawnEnemyOnPosition(randomEnemy, randomSpawnPosition);
                storedEnemies.Add(enemyspawning);

                yield return StartCoroutine(StartSpawnDelay());
            }
            else
            {
                yield return null; // Wait for the delay to finish
            }
        }
        yield return null;
    }

    private IEnumerator StartSpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true; // Allow spawning again after the delay
    }

    private Vector2 CalculateSpawnPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle * spawnRange;
        Vector2 spawnPosition = (Vector2)this.transform.position + randomPosition;

        return spawnPosition;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in Spawner Trigger");
            StartCoroutine(SpawnEnemy());
        }
    }

    public IEnumerator ForceSpawn()
    {
        while (storedEnemies.Count < maxNearbyEnemy)
        {
            if (canSpawn)
            {
                canSpawn = false; // Set the flag to prevent spawning during the delay
                Vector2 randomSpawnPosition = CalculateSpawnPosition();
                int randomEnemy = Random.Range(0, enemyIDs.Count);

                Enemy enemyspawning = SpawnEnemyOnPosition(randomEnemy, randomSpawnPosition);
                storedEnemies.Add(enemyspawning);

                yield return StartCoroutine(StartSpawnDelay());
            }
            else
            {
                yield return null; // Wait for the delay to finish
            }
        }
        yield return null;
    }

    private Enemy SpawnEnemyOnPosition(int ID, Vector2 pos)
    {
        return Instantiate(EnemyDataManager.Instance.GetEnemy(enemyIDs[ID]), pos, Quaternion.identity, this.transform).GetComponent<Enemy>();
    }

    public void ForceDie()
    {
        foreach (Enemy enemy in storedEnemies)
        {
            storedEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }
}
