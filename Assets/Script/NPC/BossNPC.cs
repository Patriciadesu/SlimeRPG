using UnityEngine;

public class BossNPC : NPC
{
    [SerializeField, Tooltip("Input Dialog After Boss is Active")] private string[] BossActiveDialog;
    public bool BossEventOngoing;
    void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();

        }
        if (BossEventOngoing == true) //Check if Boss is active or not then run the right system
        {   
            PlayBossDialog();
           
            
        }
        else
        {
             DialogSystem();
        }
        
    }
    public void PlayBossDialog()
    {
        if (dialogInterface.activeSelf)
        {
            if (Input.GetKeyDown(dialogProgressingKey))
            {
                currentDialog += 1;
            }
        }
        nameText.text = npcName;

        if (currentDialog > 0 && currentDialog <= BossActiveDialog.Length)
        {
            dialogText.text = BossActiveDialog[currentDialog - 1];
        }
        else
        {
            UnInteract();
        }
    }
    
    public void CheckBossStatus()
    {

    }
}
