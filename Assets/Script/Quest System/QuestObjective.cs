using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    public string name;
    public int enemyID;
    public int requiredAmount;
    public int currentAmount;

    public void StartObjective()
    {
        GameManager.BattleReport += UpdateProgress;
        currentAmount = 0;
        Debug.Log($"The objective is kill {requiredAmount} Enemy");
    }
    public void UpdateProgress(Enemy enemy)
    {
        if (enemy.id == enemyID)
        {
            currentAmount++;
            Debug.Log($" {currentAmount} Enemy killed");
        }
        if (currentAmount >= requiredAmount) CompleteObjective();
        QuestManager.Instance.UpdateQuestUI();
    }
    public void CompleteObjective()
    {
        QuestManager.Instance.CheckObjective();
        GameManager.BattleReport -= UpdateProgress;
    }
}
