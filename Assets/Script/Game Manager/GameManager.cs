using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public sPlayerProgress[] CreateUpdatedProgress()
    {
        List<sPlayerProgress> playerProgresses = new List<sPlayerProgress>();
        if (!QuestManager.Instance.currentQuest.Equals(default(Quest)))
        {
            foreach (QuestObjective objective in QuestManager.Instance.currentQuest.objectives)
            {
                playerProgresses.Add(new sPlayerProgress
                {
                    _id = objective.objectiveID,
                    currentProgress = objective.currentAmount
                });
            }
        }
        return playerProgresses.ToArray();
    }
    public sPlayerSkill[] CreateUpdatedSkill()
    {
        List<sPlayerSkill> playerSkill = new List<sPlayerSkill>();

        Skill[] skillHaves = SkillManager.Instance.skills.Where(s => s.Have).ToArray();
        skillHaves = skillHaves.Select(s => Skill.GetMaxLevel(s.GetType(), true)).ToArray();
        skillHaves = new HashSet<Skill>(skillHaves).ToArray();

        foreach(Skill skill in skillHaves)
        {
            playerSkill.Add(new sPlayerSkill
            {
                _id = skill.skillID,
                level = skill.Level
            });
        }

        return playerSkill.ToArray();
    }
    public sPlayer CreateUpdatedData()
    {
        var skillHaves = SkillManager.Instance.skills.Where(s => s.Have).ToArray();
        skillHaves = skillHaves.Select(s => Skill.GetMaxLevel(s.GetType(), true)).ToArray();
        Debug.Log(DatabaseManager.Instance.playerData._id);
        sPlayer player = new sPlayer
        {
            _id = DatabaseManager.Instance.playerData._id,
            discordId = DatabaseManager.Instance.playerData.discordId,
            coin = (int)Math.Round(Player.Instance.coin, MidpointRounding.ToEven),
            skillInventory = CreateUpdatedSkill(),
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
            currentQuest = QuestManager.Instance.currentQuest.Equals(default(Quest))?null : QuestManager.Instance.currentQuest.questID,
            questProgress = CreateUpdatedProgress()
        };
        if (player.currentQuest == "") player.currentQuest = null;
        return player;
    }

    public IEnumerator UpdatePlayerData(sPlayer player)
    {
        string url = $"http://152.42.196.107:3000/updatePlayer/{DatabaseManager.Instance.playerData._id}"; // Replace with your API URL

        string jsonData = JsonUtility.ToJson(player);
        Debug.Log("JSON Data: " + jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
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
