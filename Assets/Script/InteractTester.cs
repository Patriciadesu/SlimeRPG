using UnityEngine;

public class InteractTester : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        NPC nPC;
        if(other.TryGetComponent<NPC>(out nPC)){
            nPC.Interact();
        }
    }
}
