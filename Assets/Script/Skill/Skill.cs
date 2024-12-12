using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected int level = 1;
    protected float damage;

    public abstract void OnUse();
}
