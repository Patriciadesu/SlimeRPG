using UnityEngine;
using TMPro;
public class NPC : MonoBehaviour , IInteractable
{
    [Header("Input Things Here")]
    [SerializeField, Tooltip("Insert NPC name")] protected string npcName;
    [SerializeField, Tooltip("Input your NPC Dialog in here")] protected string[] npcDialog;
    [SerializeField, Tooltip("Insert Dialogue TMP_Text From Inspector")] protected TMP_Text dialogText;
    [SerializeField, Tooltip("Insert NPC Name TMP_Text From Inspector")] protected TMP_Text nameText;
    [SerializeField, Tooltip("Put Entire Dialog GameObject here")] protected GameObject dialogInterface;
    [Space(2)]
    [Header("Key Input")]
    [SerializeField, Tooltip("Uhh Key to move to next Dialogue")] protected KeyCode dialogProgressingKey = KeyCode.Mouse0;
    [Space(2)]
    [Header("Do not touch")]
    [SerializeField, Tooltip("Don't Touch Pls")] protected int currentDialog;
    protected virtual void Start()
    {
        dialogInterface.SetActive(false); //Just in Case
    }

    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // ¯\_(ツ)_/¯
        {
            Interact();
        }
        DialogSystem(); //The Command that run Dialog System
    }
    
    public virtual void DialogSystem()
    {  
        nameText.text = npcName; // Set NPC Text Name
        if (dialogInterface.activeSelf) //Check if Dialog is Active
        {
            if(Input.GetKeyDown(dialogProgressingKey)) 
            {
                currentDialog += 1;
            }
        }
        if (currentDialog > 0 && currentDialog <= npcDialog.Length) //System
        {
            dialogText.text = npcDialog[currentDialog - 1];
        }
        else
        {
            UnInteract();
        }
    }

    public virtual void Interact() // <-- Name Tell it all
    {
        currentDialog = 1;
        dialogInterface.SetActive(true);
    }

    public virtual void UnInteract() // <-- Name Tell it all
    {
        dialogInterface.SetActive(false);
    }
}
