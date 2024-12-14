using UnityEngine;
using TMPro;
public class NPC : MonoBehaviour
{
    [SerializeField] protected string npcName;
    [SerializeField] protected string[] npcDialog;
    [SerializeField] protected TMP_Text dialogText;
    [SerializeField] protected TMP_Text nameText;
    [SerializeField] protected int currentDialog;
    [SerializeField] protected GameObject dialogInterface;

    protected virtual void Start()
    {
        dialogInterface.SetActive(false);
        currentDialog = 1;
    }


    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentDialog = 1;
            dialogInterface.SetActive(true);
            
        }
        DialogSystem();
    }
    
    public virtual void DialogSystem()
    {
        if (dialogInterface.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0)) 
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
            dialogInterface.SetActive(false);
        }
    }

}
