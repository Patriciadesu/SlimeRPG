using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    public string objectiveID;
    public string name;
    public string enemyID;
    public int requiredAmount;
    public int currentAmount;

    public QuestObjective(string _name,string _enemyID, int _requiredAmount)
    {
        this.name = _name;
        this.enemyID = _enemyID;
        this.requiredAmount = _requiredAmount;
        this.currentAmount = 0;
    }

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
