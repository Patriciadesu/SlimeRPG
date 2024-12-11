using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> allQuests = new List<Quest>();
    public Quest currentQuest;
    public bool isFinish = false;

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
    }

    public void CheckObjective(int enemyID)
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

            if (objective.enemyID == enemyID && objective.currentAmount < objective.requiredAmount)
            {
                objective.currentAmount++;
                currentQuest.objectives[i] = objective;
                Debug.Log($"Objective '{objective.name}' progressed: {objective.currentAmount}/{objective.requiredAmount}");
            }

            if (objective.currentAmount < objective.requiredAmount)
            {
                allObjectivesCompleted = false;
            }
        }

        if (allObjectivesCompleted)
        {
            CompleteQuest();
        }
    }



    public void CompleteQuest()
    {
        Debug.Log($"Quest '{currentQuest.name}' completed!");
        isFinish = true;
    }
}
