using UnityEngine;

public class ActiveSkill : Skill
{
    public ActivateType activateType;
    public ActiveSkill NextStep;
    public override void OnUse()
    {
        Debug.Log("Using ActiveSkill");
    }
}

public enum ActivateType
{
    Instant,
    Charged,
    Passive
}

public class LightBeamV1 : ActiveSkill
{
    public override void OnUse()
    {
        base.OnUse();
    }
}

public class LightBeamV2 : ActiveSkill
{
    public override void OnUse()
    {
        base.OnUse();
    }
}

