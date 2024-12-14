using UnityEngine;

public class InteractTester : MonoBehaviour
{
    public NPC currentNPC;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            currentNPC = npc;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc) && currentNPC == npc)
        {
            currentNPC.UnInteract();
            currentNPC = null;
        }
    }

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNPC.Interact();
        }
    }
}
