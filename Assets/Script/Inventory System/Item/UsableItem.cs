using UnityEngine;

public abstract class UsableItem : Item
{
    public Player player;
    public int priority;
    public float duration;

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}
