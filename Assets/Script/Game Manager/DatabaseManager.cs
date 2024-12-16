using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;

public static class API
{
    //const string www = "http://localhost:4500/";
    //const string questPath = www + "quest";
    //const string getAllQuest = www + "quest/get";
    //const string userPath = www + "user";
}

public class DatabaseManager : MonoBehaviour
{
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
    ///     GetDataObject<sQuest[]>(myApi,OnGetQuestData);
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
    /// You have to replace {} with the parameter (In this case is Quest Id)
    /// So to use this api you have to do like this http://localhost:4500/getRewards?questID=64c9e52aabf4a73983fbd681
    /// You see? i replace {} with my quest id which is 64c9e52aabf4a73983fbd681 then its gonna work
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

    #endregion

}