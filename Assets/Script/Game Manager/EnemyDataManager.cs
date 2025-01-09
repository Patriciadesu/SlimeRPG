using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public static EnemyDataManager Instance {get; private set;}
    public List<GameObject> availableEnemyPrefab;
    
    public List<sEnemy> _enemiesFromDatabase = new List<sEnemy>();

    public void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
        DontDestroyOnLoad(this);
        GetEnemyData();
    }
    /// <summary>
    /// Get enemy gameobject from enemy ID
    /// </summary>
    /// <param name="ID">enemyID</param>
    /// <returns></returns>
    public GameObject GetEnemy(string ID){
        foreach(GameObject enemyobj in availableEnemyPrefab){
            if(enemyobj.GetComponent<Enemy>().id.ToString() == ID){
                Debug.Log("Get Enemy from enemy datamanager");
                
                return enemyobj;
            }
        }
        return null;
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

    private void GetEnemyData(){
        if(DatabaseManager.Instance == null){
            Debug.LogError("DatabaseManager.Instance is null!");
            return;
        }
        DatabaseManager.Instance.GetDataObejct<List<sEnemy>>(API.getAllEnemy , GetEnemiesFromDatabase);
    }

}
