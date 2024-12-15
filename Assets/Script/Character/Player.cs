using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Player : Character
{
    [Header("Singleton")]
    public static Player Instance { get; private set; }

    [Header("Player Stats")]
    public int Level { get => _level; }
    public float MaxExp { get => ((float)Math.Pow(1.1f, _level - 1)) * 100; }
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

            while (_exp >= MaxExp && _level < 100)
            {
                _exp -= MaxExp;
                _level++;
            }

            if (_level >= 100)
            {
                _level = 100;
                _exp = Mathf.Min(_exp, MaxExp);
            }
        }
    }
    private float _exp;
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
<<<<<<< HEAD
=======
    [Header("Player Component")]
    public Rigidbody2D rb2D; // ตัวแปรสำหรับอ้างอิง Rigidbody2D
    public Animator animator; // Animator สำหรับควบคุมแอนิเมชัน
>>>>>>> 65513587be38da08b3b6e23c01e54929e5f52415
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            transform.SetParent(null);
            Instance = this;
        }
        DontDestroyOnLoad(this);
        base.Awake();
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>(); // เชื่อมโยงกับ Rigidbody2D ถ้าไม่ถูกกำหนด
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // เชื่อมโยง Animator ถ้าไม่ถูกกำหนด
        }
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

    private void PlayerInput()
    {
        // กด Q เพื่อใช้ ..... Skill
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q key pressed");

            // ใช้ Dash ที่ถูกต้องจาก ScriptableObject
            Dash dashSkill = ScriptableObject.CreateInstance<Dash>();
            StartCoroutine(SkillManager.Instance.UseSkill(dashSkill));
        }
        // กด E หรือ Q เพื่อใช้skill heal
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SkillManager.Instance.UseSkill(new HealSkill()));
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SkillManager.Instance.UseSkill(new HealSkill()));
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x,y);

        Move(movement);
    }

    protected void Move(Vector2 velocity)
    {
        
        rb2D.linearVelocity = Vector2.Lerp(rb2D.linearVelocity, velocity * speed * 3, Time.fixedDeltaTime * 5);

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

    public void Heal(float amount)
    {
        health = Mathf.Min(MaxHealth, health + amount);
        Debug.Log($"Player healed by {amount}. Current health: {health}");
    }
}
