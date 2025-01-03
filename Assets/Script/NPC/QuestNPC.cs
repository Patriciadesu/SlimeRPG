using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestNPC : NPC
{
    //serialize for testing quest
    [SerializeField, Tooltip("NPC Dialog When Quest is Active")] 
    private string[] npcMidQuestDialog;
    [SerializeField] private string[] questAvailableList; // QuestID
    [SerializeField] private TMP_Text[] QuestNames;
    [SerializeField, Tooltip("Put Choose Quest GameObject here")] 
    private GameObject QuestUI;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        DialogSystem();
    }
    public void GiveQuest(int index)  
    {
        QuestManager.Instance.GetQuest(questAvailableList, index);
        UnInteract();
    }
    public void CheckPlayerQuestStatus()
    {

    }
    public void PlayMidQuestDialog()
    {

    }
    public void ChooseQuest()
    {
        int index = 0;
        foreach(TMP_Text i in QuestNames)
        {
            i.text = QuestManager.Instance.GetName(questAvailableList[index]);
            index++;
        }
        QuestUI.SetActive(true);
    }
    public override void DialogSystem()
    {
        if (dialogInterface.activeSelf)
        {
            if (Input.GetKeyDown(dialogProgressingKey))
            {
                currentDialog += 1;
            }
        }
        nameText.text = npcName;

        if (currentDialog > 0 && currentDialog <= npcDialog.Length)
        {
            dialogText.text = npcDialog[currentDialog - 1];
        }
        else
        {
            ChooseQuest();
        }
    }

    public override void Interact()
    {
        base.Interact();
        QuestUI.SetActive(false);


    }
    public override void UnInteract()
    {
        base.UnInteract();
        QuestUI.SetActive(false);
    }
}
