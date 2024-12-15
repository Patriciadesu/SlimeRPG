using UnityEngine;

public class BossNPC : NPC
{
    [SerializeField] private string[] BossActiveDialog;
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
        if (BossEventOngoing == true)
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
