using UnityEngine;

public class SpawnDectector : MonoBehaviour
{
    private Spawner spawner;

    void Awake()
    {
        spawner = transform.parent.GetComponent<Spawner>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player;
        if(other.TryGetComponent<Player>(out player)){
            spawner.isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player player;
        if(other.TryGetComponent<Player>(out player)){
            spawner.isInRange = false;
        }
    }
}
