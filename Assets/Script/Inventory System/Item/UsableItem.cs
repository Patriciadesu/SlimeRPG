using UnityEngine;

public class UsableItem : Item
{
    public Player player;
    public void Start()
    {
        player = player.GetComponent<Player>();
    }

    
    public virtual void Used()
    {

    }
}
