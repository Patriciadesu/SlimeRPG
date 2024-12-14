using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected int level = 1;
    public string Name;
    public string Description;
    public Sprite skillSprite;
    public float price;
    public bool have;
    public float coolDown;
    public bool isActive;

    public virtual IEnumerator OnUseForEnemy(Enemy enemy) { yield return OnUse(); }
    public abstract IEnumerator OnUse();
}
