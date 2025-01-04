using UnityEngine;

public class UsableItem : Item
{
    public Player player;
    public int priority;
    public float duration;
    public virtual void Start()
    {
        player = player.GetComponent<Player>();
    }

    
    public virtual void Used()
    {

    }

    public virtual void ApplyEffect()
    {

    }
    public virtual void RemoveEffect()
    {
        
    }
}
