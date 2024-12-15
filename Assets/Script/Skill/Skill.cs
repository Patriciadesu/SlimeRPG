using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected int level = 1;
    public Sprite SkillSprite;
    public string Name = string.Empty;
    public string Description = string.Empty;
    public string skillID;
    public bool Have = false;
    [SerializeField] private float _price = 0f;
    public float Price { get => _price; }
    public float coolDown;
    public bool isActive;

    public abstract IEnumerator OnUse();
}
