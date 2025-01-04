using UnityEngine;

public class SpeedItem : UsableItem
{
    public override void Start()
    {
        base.Start();
    }

    public float speedBonus;

    public override void ApplyEffect()
    {
        player.Speed += speedBonus;
    }

    public override void RemoveEffect()
    {
        player.Speed -= speedBonus;
    }

}
