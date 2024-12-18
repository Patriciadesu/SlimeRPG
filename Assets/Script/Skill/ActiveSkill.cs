using UnityEngine;
using System.Collections;

public abstract class ActiveSkill : Skill
{
    public Rarity rarity;
    public ActivateType activateType;
    public ActiveSkill NextStep;

    public enum Rarity
    {
        Common,
        Epic,
        Mythic,
        Legendary
    }
}

public enum ActivateType
{
    Instant,
    Charged,
    Passive
}
public class HealSkill : ActiveSkill
{
    public float healAmount = 50f; 

    public override IEnumerator OnUse()
    {
        Debug.Log($"Using HealSkill. Healing for {healAmount} HP");

        if (Player.Instance != null)
        {
            Player.Instance.Heal(healAmount);
        }

        yield break;
    }
}
