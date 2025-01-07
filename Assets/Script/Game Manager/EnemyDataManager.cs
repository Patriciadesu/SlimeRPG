using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public static EnemyDataManager Instance {get; private set;}
    public List<Enemy> availableEnemyPrefab;
    
    private List<sEnemy> _enemiesFromDatabase = new List<sEnemy>();

    public int rewardloaded = 0;

    public void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
        DontDestroyOnLoad(this);
        SetUpEnemies();
    }
    /// <summary>
    /// Get enemy gameobject from enemy ID
    /// </summary>
    /// <param name="ID">enemyID</param>
    /// <returns></returns>
    public GameObject GetEnemy(string ID){
        foreach(Enemy enemy in availableEnemyPrefab){
            if(enemy.id.ToString() == ID){
                Debug.Log("Get Enemy from enemy datamanager");
                
                return enemy.gameObject;
            }
        }
        return null;
    }

    private void SetUpEnemies(){
        GetEnemyData();
        SetEnemiesReward();
    }
    private void SetEnemiesReward(){
        foreach(Enemy enemy in availableEnemyPrefab){
            AddRewardtoEnemy(enemy);
        }
    }
    public void AddRewardtoEnemy(Enemy enemy){
        foreach(sEnemy enemydata in _enemiesFromDatabase){
            if(enemy.id == enemydata._id){
                enemy.rewardID = enemydata.rewardID;
                return;
            }
        }
        Debug.LogWarning("Enemy is no data");
        return;
    }
    private void GetEnemiesFromDatabase(List<sEnemy> enemies){
        _enemiesFromDatabase = enemies;
    }

    private IEnumerator GetEnemyData(){
        if(DatabaseManager.Instance == null){
            Debug.LogError("DatabaseManager.Instance is null!");
            yield break;
        }
        DatabaseManager.Instance.GetDataObejct<List<sEnemy>>(API.getAllEnemy , GetEnemiesFromDatabase);

        yield return new WaitUntil(() => {
            Debug.Log($"{_enemiesFromDatabase.Count} enemies loaded");
            return _enemiesFromDatabase.Count == availableEnemyPrefab.Count;
        });
    }

}
