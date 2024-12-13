using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected int level = 1;
    protected float damage;
    public string name;
    public string description;
    public float price;
    public bool have;
    public float coolDown;
    public bool canUse;

    public abstract void OnUse();
}
