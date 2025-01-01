using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.Playables;

public static class API
{
    const string www = "http://152.42.196.107:3000/";
    const string createPlayer                                           = www + "createPlayer";
    public static string getUser                                        = www + "getPlayer?playerID="+DatabaseManager.Instance.playerId; // player id
    public static string getUserItem                                    = www + "getItem?playerID="+DatabaseManager.Instance.playerId;
    public static string addUserItem(string itemId, int amount = 1)     => www + "addItem?playerID="+DatabaseManager.Instance.playerId+"&itemID="+itemId+"&amount="+amount;
    public static string deleteUserItem(string itemId , int amount=1)   => www + "deleteItem?playerID="+DatabaseManager.Instance.playerId+"&itemID="+itemId+"&amount="+amount;
    public static string getUserSkill                                   = www + "getSkill?playerID="+DatabaseManager.Instance.playerId;
    public static string addUserSkill(string skillId)                   => www + "addSkill?playerID="+DatabaseManager.Instance.playerId+"&skillID="+skillId;
    public static string deleteUserSkill(string skillId)                => www + "deleteSkill?playerID="+DatabaseManager.Instance.playerId+"&skillID="+skillId;
    public static string addUserQuest(string questId)                   => www + "addCurrentQuest?playerID="+DatabaseManager.Instance.playerId+"&questID="+questId;
    public static string updateObjective(string objectiveId)            => www + "getQuestProgress?playerID="+DatabaseManager.Instance.playerId+"&objectiveID="+objectiveId;
    public static string addUserProgress(string objectiveId)            => www + "addProgress?playerID="+DatabaseManager.Instance.playerId+"&objectiveID="+objectiveId;
    public static string getAllItem                                     = www + "getItem";
    public static string getAllQuest                                    = www + "getQuest";
    public static string getAllSkill                                    = www + "getSkill";
    public static string getAllReward                                   = www + "getRewards";
    public static string getAllEnemySpawner                             = www + "getEnemySpawner";
    public static string getAllEnemy                                    = www + "getEnemy";
    public static string getEnemyById(string enemyId)                   => www + $"getEnemy?enemyID={enemyId}";
    public static string getAllBossEvent                                = www + "getBossEvent";
    public static string getAllGrindingEvent                            = www + "getGrindingEvent";


}

public class DatabaseManager : SingletonPersistent<DatabaseManager>
{
    public string playerId;
    public void Start()
    {
        
    }

    #region Get
    /// <summary>
    ///
    /// PLS READ NA EVERYONE
    /// 
    /// To use you have to make OnGetData method to recieve data and assign to you variable
    ///
    /// OnGetData method will HAVE TO BE "VOID"
    ///
    /// OnGetData method NEED "ONLY 1 PARAM" AND IT HAVE TO BE YOUR DATAOBJECT CLASS
    ///
    /// Ex.
    ///
    /// public void Awake()
    /// {
    ///     DatabaseManager.Instance.GetDataObject<sQuest[]>(myApi,OnGetQuestData);
    /// }
    ///
    /// public void OnGetQuestData(sQuest[] questDatas)
    /// {
    ///     if( questDatas == null) Debug.Log("ERROR WA"); return;
    ///     myVariable = questDatas
    ///     CreateQuest();
    /// }
    ///
    /// About API :
    /// Here a example of API that Butter have sent http://localhost:4500/getRewards?questID={}
    /// So to use this api you have to do like this     string Api = "http://localhost:4500/getRewards?questID=64c9e52aabf4a73983fbd681"
    /// Or like this                                    string Api = $"http://localhost:4500/getRewards?questID={64c9e52aabf4a73983fbd681}" (Recommended)
    ///
    /// 
    /// Explanation Here :
    /// 1. Dont worry that your OnGetQuestData will not run, it wall AUTOMATICALLY run after get response from database :)
    /// 2. Puting OnGetQuestData as GetDataObject's parameter without having () is correct
    /// 3. from 2. That mean DONT PUT () AFTER OnGetQuestData
    /// 4. this GetDataObject method can be use with EVERY DataObject BUT you have to ALWAYS create your new "callback function" (in this Ex. is OnGetQuestData)
    /// 5. I will organize usage of API later just copy/paste what Butter send to the main chat oop
    /// 6. If you still curious about how to use this methods DM ma
    /// 
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Api"></param>
    /// <param name="callback"></param>

    public void GetDataObejct<T>(string Api ,Action<T> callback) where T : class
    {
        StartCoroutine(GetData<T>(Api, callback));
    }
    private IEnumerator GetData<T>(string Api, Action<T> callback) where T : class
    {
        using (UnityWebRequest request = UnityWebRequest.Get(Api))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error while fetching data from API: {Api}, Error: {request.error}");
                callback(null); 
            }
            else
            {
                try
                {
                    string jsonResponse = request.downloadHandler.text;
                    T data = JsonConvert.DeserializeObject<T>(jsonResponse);

                    callback(data);
                }
                catch (Exception ex) 
                {
                    Debug.LogError($"Make Sure your DataObject variable name match with backend na : {ex.Message}");
                    callback(null);
                }
            }
        }
    }

    #endregion

    #region Post
    /// <summary>
    ///
    /// This one is very easy to use wa
    ///
    /// About API :
    /// Here a example of API that Butter have sent                    http://localhost:4500/addItem?playerID={}&itemID={}&amount={} 
    /// So to use this api you have to do like this     string Api = $"http://localhost:4500/addItem?playerID={64c9e52aabf4a73983fbd681}&itemID={675c442db0fc615388eb4d04}&amount={}"
    /// 
    /// </summary>
    /// <param name="Api"></param>
    public void Post(string Api)
    {
        Debug.Log("Send Request To : " + Api);
        StartCoroutine(SendRequest(Api));
    }
    public IEnumerator SendRequest(string Api)
    {
        using (UnityWebRequest request = new UnityWebRequest(Api, "POST"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            // Add Content-Type header (important for POST requests)
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for it to complete
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error: {request.responseCode} - {request.error}");
            }
        }
        
    }

    #endregion

}