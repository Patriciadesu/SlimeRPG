using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [Header("Singleton")]
    public static Player Instance{get; private set;}

    [Header("Player Stats")]
    public int Level { get { return _level; } }
    public float MaxExp { get { return ((float)Math.Pow(1.1f, _level + 1)) * 100; } }
    public float dodgeRate
    {
        get
        {
            return Math.Min(_level * 0.4f, 100);
        }
    }
    public float criticalRate
    {
        get
        {
            return Math.Min(_level * 0.25f, 100);
        }
    }
    public float Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;

            while (_exp >= MaxExp)
            {
                _exp -= MaxExp;
                _level++;
            }
        }
    }
    private float _exp;
    public float coin;

    [Header("Collectable")]
    private float collectItemRange;

    [Header("Skill")]
    public Dash dashSkill; // Reference to the Dash skill
    public SuperSpeed superSpeedSkill; // เพิ่มตัวแปรเพื่อเก็บ SuperSpeed skill
    // [Header("Skill")]
    // public Skill activeSkill1;
    // public Skill activeSkill2;
    // public Skill mobilitySkill;

    // [Header("Inventory")]
    // public Inventory inventory;

    protected override void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
        DontDestroyOnLoad(this);
        base.Awake();
    }
    void FixedUpdate()
    {
        PlayerInput();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    
    protected void Attack()
    {
        if (normalAttack != null)
            StartCoroutine(SkillManager.Instance.UseSkill(normalAttack));
    }
    private void PlayerInput(){

        if (Input.GetKeyDown(KeyCode.Q) && superSpeedSkill != null)
        {
            superSpeedSkill.OnUse(); // เรียกใช้ SuperSpeed skill
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x,y);
        Move(movement);
    }

    protected void Move(Vector2 velocity)
    {
        rb2D.linearVelocity = velocity * speed * 3;

        float currentX = transform.rotation.eulerAngles.x;
        float currentZ = transform.rotation.eulerAngles.z;

        if (velocity.x > 0)
            transform.rotation = Quaternion.Euler(currentX, 0, currentZ);
        else if (velocity.x < 0)
            transform.rotation = Quaternion.Euler(currentX, 180, currentZ);
    }

    protected override void Die()
    {
        // Do something with die mechanics
        base.Die();
    }

    internal void Move_2(Vector2 vector2)
    {
        throw new NotImplementedException();
    }
}
