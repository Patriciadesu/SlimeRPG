using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "HealItem")]
public class HealItem : UsableItem
{

    public float healAmount;

    public override void ApplyEffect()
    {
        Player.Instance.Heal(healAmount);
    }

    public override void RemoveEffect()
    {
        // Healing is instant, so no need to remove anything
    }
}
