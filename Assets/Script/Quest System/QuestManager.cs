using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> allQuests = new List<Quest>();
    public List<QuestObjective> allObjectives = new List<QuestObjective>();
    public Quest currentQuest;
    public bool isFinish = false;


    [Header("Current Quest UI")]
    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text moneyRewardText;
    public TMP_Text expRewardText;
    public TMP_Text objectivesText;

    private void Start()
    {
        currentQuest = default(Quest);
    }
    /// <summary>
    /// Get All Quest Data from database to QuestManager
    /// </summary>
    public void GetAllQuests()
    {
        /*List<sQuest> questsFromDatabase = GetQuestsFromDatabase();
        List<sObjective> objectivesFromDatabase = GetObjectivesFromDatabase();

        List<Quest> allQuests = new List<Quest>();

        foreach (sQuest squest in questsFromDatabase)
        {
            Quest quest = new Quest(squest);

            foreach (string objectiveID in squest.Objective)
            {
                sObjective sObjective = objectivesFromDatabase.Find(o => o.objectiveID == objectiveID);

                if (sObjective != null)
                {
                    QuestObjective newObjective = new QuestObjective(
                        sObjective.objectiveID,
                        sObjective.name,
                        sObjective.enemyID,
                        sObjective.requiredAmount,
                        0
                    );
                    quest.objectives.Add(newObjective);
                }
                else
                {
                    Debug.LogWarning("Objective ID " + objectiveID + " not found for quest: " + squest._id);
                }
            }
            allQuests.Add(quest);
        }

        Debug.Log("All quests loaded from database.");*/
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
        UpdateQuestUI();
        currentQuest = default(Quest);
        //currentQuest = null; may by use | Quest? currentQuest = null;
        //Add Reward
        //Add remove quest duay naa
    }

    public void UpdateQuestUI()
    {
        questNameText.text = $"Quest: {currentQuest.name}";
        questDescriptionText.text = $"Description: {currentQuest.description}";
        moneyRewardText.text = $"Money Reward: {currentQuest.moneyReward}";
        expRewardText.text = $"EXP Reward: {currentQuest.expReward}";

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

    public class sQuest
    {
        public string _id;
        public string name;
        public string description;
        public sReward reward;
        public List<string> Objective;
    }

    public class sObjective
    {
        public string objectiveID;
        public string name;
        public string enemyID;
        public int requiredAmount;
    }


    public class sReward
    {
        public string _id;
        public string name;
        public int coin;
        public int xp;
        public List<sItem> item;
    }

    public class sItem
    {
        public string name;
        public int amount;
    }
}
