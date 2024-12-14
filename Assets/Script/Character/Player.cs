using System;
using Unity.Collections;
using UnityEngine;

public class Player : Character
{
    [Header("Singleton")]
    public static Player Instance{get; private set;}

    [Header("Player Stats")]
    public float AttackDamage { get { return _attackDamage; } }
    public float dodgeRate{get; private set;}
    public float criticalRate{get; private set;}
    public float exp;
    public float coin;

    [Header("Collectable")]
    private float collectItemRange;

    [Header("Skill")]
    private Dash dashSkill; // Reference to the Dash skill
    private SuperSpeed superSpeedSkill; // เพิ่มตัวแปรเพื่อเก็บ SuperSpeed skill
    private SprintToEnemy SprintToEnemy;// เพิ่มตัวแปรเพื่อเก็บ SprintToEnemy
    private Teleport Teleport;// เพิ่มตัวแปรเพื่อเก็บ Teleport
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

    protected void Attack()
    {
        
    }
    private void PlayerInput(){

        // กด Q เพื่อใช้ ..... Skill
        if (Input.GetKeyDown(KeyCode.Q) && superSpeedSkill != null)
        {
            StartCoroutine(superSpeedSkill.OnUse());
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x,y);
        Move(movement);
    }

    protected void Move(Vector2 velocity)
    {
        rb2D.linearVelocity = velocity * speed * 3;
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
