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


    public IEnumerator SpawnEnemy()
    {
        //Debug.Log("Start spawn function");
        while (storedEnemies.Count < maxNearbyEnemy && isPlayerInRange())
        {
            //Debug.Log("Start spawning enemy in");
            Vector2 randomSpawnPosition = CalculateSpawnPosition();
            int randomEnemy = RandomInRange(enemyIDs.Count);

            Enemy enemyspawning = SpawnEnemyOnPosition(randomEnemy , randomSpawnPosition);
            storedEnemies.Add(enemyspawning);
            //Debug.Log("spawning should be right now");
            yield return new WaitForSeconds(spawnDelay);
        }
        yield return null;
    }

    private Vector2 CalculateSpawnPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle * spawnRange;
        Vector2 spawnPosition = (Vector2)this.transform.position + randomPosition;

        return spawnPosition;
    }

    private bool isPlayerInRange()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(this.transform.position, triggerZoneRange);
        bool isInRange = collider2D != null && collider2D.CompareTag("Player");
        Debug.Log($"Is player in range: {isInRange}");
        return isInRange;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in Spawner Trigger");
            StartCoroutine(SpawnEnemy());
            //Debug.Log($"Stored enemy count ={storedEnemies.Count}");
        }
    }

    public IEnumerator ForceSpawn(){
        while (storedEnemies.Count < maxNearbyEnemy)
        {
            //Debug.Log("Start spawning enemy in");
            Vector2 randomSpawnPosition = CalculateSpawnPosition();
            int randomEnemy = RandomInRange(enemyIDs.Count);

            Enemy enemyspawning = SpawnEnemyOnPosition(randomEnemy , randomSpawnPosition);
            storedEnemies.Add(enemyspawning);
            //Debug.Log("spawning should be right now");
            yield return new WaitForSeconds(spawnDelay);
        }
        yield return null;
    }
    private int RandomInRange(int count){
        return Random.Range(0, count);
    }
    private Enemy SpawnEnemyOnPosition(int ID , Vector2 pos){
        return Instantiate(EnemyDataManager.Instance.GetEnemy(enemyIDs[ID]),
        pos,
        Quaternion.identity,
        this.transform)
        .GetComponent<Enemy>();
    }

    public void ForceDie(){
        foreach(Enemy enemy in storedEnemies){
            storedEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }
}
