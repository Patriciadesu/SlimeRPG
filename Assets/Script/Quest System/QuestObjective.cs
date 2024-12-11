[System.Serializable]
public struct QuestObjective
{
    public string name;
    public int enemyID;
    public int requiredAmount;
    public int currentAmount;

    public void OnQuestStart()
    {
        currentAmount = 0;
    }
    public void OnProgressing()
    {

    }
    public void OnCompletion()
    {
        if (currentAmount >= requiredAmount)
        {

        }
    }
}
