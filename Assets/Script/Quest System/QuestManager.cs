using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> allQuests = new List<Quest>();
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

    public void GetAllQuests() //get all quest from Database
    {
        //allQuests = questsFromDatabase;
        Debug.Log("All quests loaded from database.");
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

}
