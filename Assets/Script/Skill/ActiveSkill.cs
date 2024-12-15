using UnityEngine;
using System.Collections;

public class ActiveSkill : Skill
{
    public ActivateType activateType;
    public ActiveSkill NextStep;
    public override IEnumerator OnUse()
    {
        Debug.Log("Using ActiveSkill");
        yield break;
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
        yield return base.OnUse();

        Debug.Log($"Using HealSkill. Healing for {healAmount} HP");

        if (Player.Instance != null)
        {
            Player.Instance.Heal(healAmount);
        }

        yield break;
    }
}
public class LightBeamV1 : ActiveSkill
{
    public override IEnumerator OnUse()
    {
        yield return base.OnUse();
    }
}

public class LightBeamV2 : ActiveSkill
{
    public override IEnumerator OnUse()
    {
        yield return base.OnUse();
    }
}

