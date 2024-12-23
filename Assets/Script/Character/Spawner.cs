using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRange;
    public float spawnDelay;
    public List<string> enemyIDs;
    public int maxNearbyEnemy;
    public float requiredPlayerRange;
    public List<Enemy> storedEnemy = new List<Enemy>();


    public IEnumerator SpawnEnemy()
    {
        //Debug.Log("Start spawn function");
        while (storedEnemy.Count < maxNearbyEnemy && isPlayerInRange())
        {
            //Debug.Log("Start spawning enemy in");
            Vector2 randomSpawnPosition = CalculateSpawnPosition();
            int randomenemy = Random.Range(0, enemyIDs.Count);

            Enemy enemyspawning = Instantiate(EnemyDataManager.Instance.GetEnemy(enemyIDs[randomenemy]),
             randomSpawnPosition,
              Quaternion.identity,
               this.transform)
               .GetComponent<Enemy>();
            storedEnemy.Add(enemyspawning);
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
        Collider2D collider2D = Physics2D.OverlapCircle(this.transform.position, requiredPlayerRange);
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
            //Debug.Log($"Stored enemy count ={storedEnemy.Count}");
        }
    }

    public IEnumerator ForceSpawn(){
        while (storedEnemy.Count < maxNearbyEnemy)
        {
            //Debug.Log("Start spawning enemy in");
            Vector2 randomSpawnPosition = CalculateSpawnPosition();
            int randomenemy = Random.Range(0, enemyIDs.Count);

            Enemy enemyspawning = Instantiate(EnemyDataManager.Instance.GetEnemy(enemyIDs[randomenemy]),
             randomSpawnPosition,
              Quaternion.identity,
               this.transform)
               .GetComponent<Enemy>();
            storedEnemy.Add(enemyspawning);
            //Debug.Log("spawning should be right now");
            yield return new WaitForSeconds(spawnDelay);
        }
        yield return null;
    }

    public void ForceDie(){
        foreach(Enemy enemy in storedEnemy){
            storedEnemy.Remove(enemy);
            Destroy(enemy);
        }
    }
}
