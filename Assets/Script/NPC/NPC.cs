using UnityEngine;
using TMPro;
public class NPC : MonoBehaviour , IInteractable
{
    [SerializeField] protected string npcName;
    [SerializeField] protected string[] npcDialog;
    [SerializeField] protected TMP_Text dialogText;
    [SerializeField, Tooltip("this is a something we don't talk about")] protected TMP_Text nameText;
    [SerializeField] protected int currentDialog;
    [SerializeField] protected GameObject dialogInterface;
    [Header("Key Input")]
    [SerializeField] protected KeyCode dialogProgressingKey = KeyCode.Mouse0;

    protected virtual void Start()
    {
        dialogInterface.SetActive(false);
        currentDialog = 1;
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
        
        if (currentDialog < npcDialog.Length + 1)
        {
            dialogText.text = npcDialog[currentDialog - 1];
        }
        else
        {
            dialogInterface.SetActive(false);
        }
    }

    public virtual void Interact()
    {
        currentDialog = 1;
        dialogInterface.SetActive(true);
    }

    public void UnInteract()
    {
        throw new System.NotImplementedException();
    }
}
