using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool spawnable;
    public float spawnRange;
    public float spawnDelay;
    public string enemyID;
    public int maxNearbyEnemy;
    public float requiredPlayerRange;
    private List<Enemy> storedEnemy;


    public IEnumerator SpawnEnemy()
    {
        while (storedEnemy.Count < maxNearbyEnemy && isPlayerInRange())
        {
            Vector2 randomSpawnPosition = CalculateSpawnPosition();

            Enemy enemyspawning = Instantiate(EnemyDataManager.Instance.GetEnemy(enemyID),
             randomSpawnPosition,
              Quaternion.identity,
               this.transform)
               .GetComponent<Enemy>();
            storedEnemy.Add(enemyspawning);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle * spawnRange;
        Vector2 spawnPosition = (Vector2)this.transform.position + randomPosition;

        return spawnPosition;
    }

    private bool isPlayerInRange(){
        Collider2D collider2D = Physics2D.OverlapCircle(this.transform.position , requiredPlayerRange);
        return collider2D != null && collider2D.CompareTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            StartCoroutine(SpawnEnemy());
        }
    }
}
