using UnityEngine;

public class HealItem : UsableItem
{
    void Start()
    {
        base.Start();
    }
    public override void Used()
    {
        player.Heal(20);
    }
}
