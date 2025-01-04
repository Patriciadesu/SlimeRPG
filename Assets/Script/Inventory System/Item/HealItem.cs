using Unity.VisualScripting;
using UnityEngine;

public class HealItem : UsableItem
{
    public override void Start()
    {
        base.Start();
    }

    public float healAmount;

    public override void ApplyEffect()
    {
        player.Heal(healAmount);
    }

    public override void RemoveEffect()
    {
        // Healing is instant, so no need to remove anything
    }
}
