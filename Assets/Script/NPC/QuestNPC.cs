using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestNPC : NPC
{
    //serialize for testing quest
    [SerializeField] private string[] npcMidQuestDialog;
    [SerializeField] private string[] questAvailableList;
    [SerializeField] private TMP_Text QuestName1;
    [SerializeField] private TMP_Text QuestName2;
    [SerializeField] private TMP_Text QuestName3;
    [SerializeField] private GameObject QuestUI;

    public void Start()
    {
        base.Start();
        QuestUI.SetActive(false);
    }
    private void Update()
    {
        base.Update();
    }
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
        QuestName1.text = questAvailableList[0];
        QuestName2.text = questAvailableList[1];
        QuestName3.text = questAvailableList[2];
        QuestUI.SetActive(true);
    }
    public override void DialogSystem()
    {
        if (dialogInterface.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentDialog += 1;
            }
        }
        nameText.text = npcName;

        if (currentDialog < npcDialog.Length + 1)
        {
            dialogText.text = npcDialog[currentDialog - 1];
        }
        else
        {
            ChooseQuest();
        }
    }
    public void GetQuest(int QuestIndex)
    {
        GiveQuest(QuestIndex);
        QuestUI.SetActive(false);
        dialogInterface.SetActive(false);
    }
}
