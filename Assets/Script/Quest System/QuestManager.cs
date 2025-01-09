using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> allQuests = new List<Quest>();
    public List<QuestObjective> allObjectives = new List<QuestObjective>();

    private List<sQuest> _questsFromDatabase = new List<sQuest>();
    private List<sObjective> _objectivesFromDatabase = new List<sObjective>();

    public Quest currentQuest;
    private bool isFinish = false;


    [Header("Current Quest UI")]
    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text moneyRewardText;
    public TMP_Text expRewardText;
    public TMP_Text objectivesText;


    private bool questsLoaded = false;
    private bool objectivesLoaded = false;

    private void Awake()
    {
        allQuests.Clear();
        allObjectives.Clear();
        Debug.Log(API.getAllQuest);

        StartCoroutine(LoadDataAndCreateQuests());
    }

    private IEnumerator LoadDataAndCreateQuests()
    {
        if (DatabaseManager.Instance == null)
        {
            Debug.LogError("DatabaseManager.Instance is null!");
            yield break;
        }

        DatabaseManager.Instance.GetDataObejct<sQuest[]>(API.getAllQuest, GetQuestsFromDatabase);
        DatabaseManager.Instance.GetDataObejct<sObjective[]>(API.getAllObjective, GetObjectivesFromDatabase);

        yield return new WaitUntil(() =>
        {
            Debug.Log($"Waiting for data... Quests: {_questsFromDatabase.Count}, Objectives: {_objectivesFromDatabase.Count}");
            return _questsFromDatabase.Count > 0 && _objectivesFromDatabase.Count > 0;
        });

        CreateAllQuests();
    }


    private void Start()
    {
        currentQuest = default(Quest);
    }

    public void GetQuestsFromDatabase(sQuest[] quests)
    {
        _questsFromDatabase = quests.ToList();
    }

    public void GetObjectivesFromDatabase(sObjective[] objectives)
    {
        _objectivesFromDatabase = objectives.ToList();
    }

    /// <summary>
    /// Get All Quest Data from database to QuestManager
    /// </summary>
    public void CreateAllQuests()
    {
        allQuests.Clear();

        foreach (sQuest squest in _questsFromDatabase)
        {
            Quest quest = new Quest(squest);

            foreach (string objectiveID in squest.Objective)
            {
                sObjective sObjective = _objectivesFromDatabase.Find(o => o._id == objectiveID);

                if (sObjective != null)
                {
                    QuestObjective newObjective = new QuestObjective(
                        sObjective._id,
                        sObjective.name,
                        sObjective.enemyID,
                        sObjective.requiredAmount,
                        0
                    );
                    quest.objectives.Add(newObjective);
                }
                else
                {
                    Debug.LogWarning($"Objective ID {objectiveID} not found for quest: {squest._id}");
                }
            }

            allQuests.Add(quest);
        }

        Debug.Log($"All quests loaded from database. Total quests: {allQuests.Count}");
        //Debug.Log($"All objectives loaded from database. Total objectives: {allObjectives.Count}");
    }

    

    public void GetQuest(string[] npcQuestIDs, int questIndex)
    {
        if(questIndex < 0 || questIndex >= npcQuestIDs.Length)
        {
            Debug.LogError("Invalid quest index.");
            return;
        }

        string questID = npcQuestIDs[questIndex];
        Quest? selectedQuest = allQuests.Find(quest => quest.questID == questID);
        if (selectedQuest != null)
        {
            StartQuest((Quest)selectedQuest);
        }
    }
    /// <summary>
    /// Start current Quest
    /// </summary>
    /// <param name="quest">current Quest</param>
    /// 



    public void LoadQuestProgress(string currentQuestId, sPlayerProgress[] progress)
    {
        Quest currentQuest = allQuests.Find(q => q.questID == currentQuestId);

        if (!currentQuest.Equals(default(Quest)))
        {
            Debug.LogWarning($"Quest ID {currentQuestId} not found in allQuests");
            return;
        }

        foreach (QuestObjective objective in currentQuest.objectives)
        {
            sPlayerProgress objectiveProgress = Array.Find(progress, p => p._id == objective.objectiveID);

            if (objectiveProgress != null)
            {
                objective.currentAmount = objectiveProgress.currentProgress;
            }
            else
            {
                Debug.LogWarning($"No progress found for objective ID {objective.objectiveID} in quest: {currentQuestId}");
                objective.currentAmount = 0;
            }
        }

        Debug.Log($"Quest progress loaded for quest: {currentQuestId}");
    }


    public void StartQuest(Quest quest)
    {
        if (!currentQuest.Equals(default(Quest)))
        {
            Debug.LogWarning($"Cannot start a new quest. Current quest '{currentQuest.name}' is still active.");
            return;
        }

        currentQuest = quest;
        isFinish = false;
        Debug.Log($"Started quest: {quest.name}");
        for (int i = 0; i < currentQuest.objectives.Count; i++)
        {
            var objective = currentQuest.objectives[i];
            objective.StartObjective();
        }
        UpdateQuestUI();
    }
    
    public void CheckObjective()
    {
        if (currentQuest.objectives == null)
        {
            Debug.LogError("No objectives found.");
            return;
        }

        bool allObjectivesCompleted = true;

        for (int i = 0; i < currentQuest.objectives.Count; i++)
        {
            var objective = currentQuest.objectives[i];

            if (objective.currentAmount < objective.requiredAmount)
            {
                allObjectivesCompleted = false;
            }
        }
        if (allObjectivesCompleted) CompleteQuest();
        
    }
    public void CompleteQuest()
    {
        Debug.Log($"Quest '{currentQuest.name}' completed!");
        isFinish = true;
        RewardManager.Instance.GiveReward(currentQuest.reward);
        currentQuest = default(Quest);
        UpdateQuestUI();
        //Add Reward
    }

    public void UpdateQuestUI()
    {
        questNameText.text = $"Quest: {currentQuest.name}";
        questDescriptionText.text = $"Description: {currentQuest.description}";
        moneyRewardText.text = $"Money Reward: ";/*{currentQuest.reward.moneyReward}*/
        expRewardText.text = $"EXP Reward: ";/*{currentQuest.reward.expReward}*/

        if (isFinish)
        {
            objectivesText.text = "Quest Completed!";
        }
        else
        {
            string objectivesTextString = "Objectives:\n";
            foreach (var objective in currentQuest.objectives)
            {
                if (objective.currentAmount >= objective.requiredAmount)
                {
                    objectivesTextString += $"<color=green>{objective.name}: {objective.currentAmount}/{objective.requiredAmount} completed</color>\n";
                }
                else
                {
                    objectivesTextString += $"{objective.name}: {objective.currentAmount}/{objective.requiredAmount} in progress\n";
                }
            }
            objectivesText.text = objectivesTextString;
        }
    }
    /// <summary>
    /// Get Quest name by quest ID
    /// </summary>
    /// <param name="ID">questID</param>
    /// <returns></returns>
    public string GetName(string ID){
        foreach(Quest quest in allQuests){
            if(quest.questID == ID){
                return quest.name;
            }
        }
        return null;
    }
}
