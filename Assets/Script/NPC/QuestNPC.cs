using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    //serialize for testing quest
    [SerializeField] private string[] npcMidQuestDialog;
    [SerializeField] private string[] questAvailableList;

    public void GiveQuest(int index)
    {
        QuestManager.Instance.GetQuest(questAvailableList, index);
    }
    public void CheckPlayerQuestStatus()
    {

    }
    public void PlayMidQuestDialog()
    {

    }
    public void ChooseQuest()
    {

    }
}
