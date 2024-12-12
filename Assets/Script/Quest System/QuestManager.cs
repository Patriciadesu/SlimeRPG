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
        currentQuest = quest;
        isFinish = false;
        Debug.Log($"Started quest: {quest.name}");
        UpdateQuestUI();
        for (int i = 0; i < currentQuest.objectives.Count; i++)
        {
            var objective = currentQuest.objectives[i];
            objective.StartObjective();
        }
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
        //currentQuest = null; may by use | Quest? currentQuest = null;
        //Add Reward
        //Add remove quest duay naa
    }

    public void UpdateQuestUI()
    {
        /*if (currentQuest != null)*/
        {
            questNameText.text = $"Quest: {currentQuest.name}";
            questDescriptionText.text = $"Description: {currentQuest.description}";
            moneyRewardText.text = $"Money Reward: {currentQuest.moneyReward}";
            expRewardText.text = $"EXP Reward: {currentQuest.expReward}";

            string objectivesTextString = "Objectives:\n";
            foreach (var objective in currentQuest.objectives)
            {
                objectivesTextString += $"{objective.name}: {objective.currentAmount}/{objective.requiredAmount} completed\n";
            }
            objectivesText.text = objectivesTextString;
        }
        /*else
        {
            questNameText.text = "No current quest";
            questDescriptionText.text = "";
            moneyRewardText.text = "Money Reward: 0";
            expRewardText.text = "EXP Reward: 0";
            objectivesText.text = "No objectives available.";
        }*/
    }
}
