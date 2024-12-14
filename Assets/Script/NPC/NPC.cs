using UnityEngine;
using TMPro;
public class NPC : MonoBehaviour , IInteractable
{
    [SerializeField, Tooltip("Your NPC Name")] protected string npcName;
    [SerializeField, Tooltip("Put Your NPC Dialogue in here, Create more index for more dialogue.")] protected string[] npcDialog;
    [SerializeField, Tooltip("Put Your Dialogue TMP_Text here")] protected TMP_Text dialogText;
    [SerializeField, Tooltip("Put Your NPC Name TMP_Text Here")] protected TMP_Text nameText;
    [SerializeField, Tooltip("Don't Touch Pls")] protected int currentDialog;
    [SerializeField, Tooltip("Put Your Entire Dialog Canvas in here!")] protected GameObject dialogInterface;
    [Header("Key Input")]
    [SerializeField, Tooltip("Uhh Key to move to next Dialogue")] protected KeyCode dialogProgressingKey = KeyCode.Mouse0;

    protected virtual void Start()
    {
        dialogInterface.SetActive(false);
        
    }

    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            
        }
        DialogSystem();
    }
    
    public virtual void DialogSystem()
    {
        if (dialogInterface.activeSelf)
        {
            if(Input.GetKeyDown(dialogProgressingKey)) 
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
            UnInteract();
        }
    }

    public virtual void Interact()
    {
        currentDialog = 1;
        dialogInterface.SetActive(true);
    }

    public virtual void UnInteract()
    {
        dialogInterface.SetActive(false);
    }
}
