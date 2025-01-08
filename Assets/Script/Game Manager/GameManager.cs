using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Networking;
using System;

public class GameManager : Singleton<GameManager>
{
    public static event OnEnemyDeath BattleReport;

    public delegate void OnEnemyDeath(Enemy enemy);

    public static void OnEnemyKilled(Enemy enemy)
    {
        BattleReport?.Invoke(enemy);
    }
    public void OnApplicationQuit()
    {
        StartCoroutine(UpdatePlayerData(CreateUpdatedData()));
    }

    public sPlayer CreateUpdatedData()
    {
        List<sPlayerProgress> playerProgresses = new List<sPlayerProgress>();
        foreach(QuestObjective objective in QuestManager.Instance.currentQuest.objectives)
        {
            playerProgresses.Add(new sPlayerProgress
            {
                _id = objective.objectiveID,
                currentProgress = objective.currentAmount
            });
        }
        sPlayer player = new sPlayer
        {
            _id = DatabaseManager.Instance.playerData._id,
            discordId = DatabaseManager.Instance.playerData.discordId,
            coin = (int)Math.Round(Player.Instance.coin, MidpointRounding.ToEven),
            skillInventory = new sPlayerSkill[]
            {
                new sPlayerSkill { _id = Player.Instance.activeSkill1.skillID, level = Player.Instance.activeSkill1.Level },
                new sPlayerSkill { _id = Player.Instance.activeSkill2.skillID, level = Player.Instance.activeSkill2.Level },
                new sPlayerSkill { _id = Player.Instance.mobilitySkill.skillID, level = Player.Instance.mobilitySkill.Level }
            },
            itemInventory = DatabaseManager.Instance.playerData.itemInventory,
            stats = new sPlayerStat
            {
                level = Player.Instance.Level,
                xp = Player.Instance.Exp,
                currentHp = Player.Instance.health
            },
            lastPos = new sPlayerPosition
            {
                x = Player.Instance.gameObject.transform.position.x,
                y = Player.Instance.gameObject.transform.position.y
            },
            lastScene = 1,
            currentQuest = QuestManager.Instance.currentQuest.questID,
            questProgress = playerProgresses.ToArray()
        };
        return player;
    }

    public IEnumerator UpdatePlayerData(sPlayer player)
    {
        string url = "http://localhost:3000/api/updatePlayer"; // Replace with your API URL

        string jsonData = JsonUtility.ToJson(player);
        Debug.Log("JSON Data: " + jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player data updated successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error updating player data: " + request.error);
            }
        }
    }
}
