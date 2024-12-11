[System.Serializable]
public struct QuestObjective
{
    public string name;
    public int enemyID;
    public int requiredAmount;
    public int currentAmount;

    public void OnQuestStart()
    {
        GameManager.BattleReport += UpdateProgress;
        currentAmount = 0;
    }
    public void UpdateProgress(Enemy enemy)
    {
        //if (enemy.id == enemyID) currentAmount++;
        if (currentAmount >= requiredAmount) CompleteObjective();
    }
    public void CompleteObjective()
    {
        QuestManager.Instance.CheckObjective();
    }
}
