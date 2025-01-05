using System.Collections.Generic;
using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public static EnemyDataManager Instance {get; private set;}
    public List<GameObject> availableEnemyPrefab;

    public void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
        DontDestroyOnLoad(this);
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
}
