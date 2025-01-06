using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public static EnemyDataManager Instance {get; private set;}
    public List<GameObject> availableEnemyPrefab;
    
    private List<sEnemy> _enemiesFromDatabase = new List<sEnemy>();

    public int rewardloaded = 0;

    public void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
        DontDestroyOnLoad(this);
        GetEnemyData();
    }

    public GameObject GetEnemy(string ID){
        foreach(GameObject enemyGO in availableEnemyPrefab){
            if(enemyGO.GetComponent<Enemy>().id.ToString() == ID){
                Debug.Log("Get Enemy from enemy datamanager");
                
                return enemyGO;
            }
        }
        return null;
    }
    public void GetEnemiesFromDatabase(sEnemy[] enemies){
        _enemiesFromDatabase = enemies.ToList();
    }

    public IEnumerator GetEnemyData(){
        if(DatabaseManager.Instance == null){
            Debug.LogError("DatabaseManager.Instance is null!");
            yield break;
        }
        DatabaseManager.Instance.GetDataObejct<sEnemy[]>(API.getAllEnemy , GetEnemiesFromDatabase);


        yield return new WaitUntil(() => {
            Debug.Log($"{rewardloaded} reward loaded");
            return rewardloaded == availableEnemyPrefab.Count-1;
        });
    }

}
