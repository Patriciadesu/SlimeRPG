using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;

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