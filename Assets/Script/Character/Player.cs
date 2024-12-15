using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

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
    public Rigidbody2D rb2D;

    [Header("Interact")]
    public NPC currentNPC;

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
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNPC.Interact();
        }
    }

    protected void Attack()
    {
        if (normalAttack != null)
            StartCoroutine(SkillManager.Instance.UseSkill(normalAttack));
    }

    private void PlayerInput()
    {

        //// ใช้สกิลที่เลือกไว้สำหรับปุ่ม E
        //if (Input.GetKeyDown(KeyCode.E) && activeSkill1 != null)
        //{
        //    Debug.Log("Using Skill 1: " + activeSkill1.name);
        //    StartCoroutine(SkillManager.Instance.UseSkill(activeSkill1));
        //}

        //// ใช้สกิลที่เลือกไว้สำหรับปุ่ม F
        //if (Input.GetKeyDown(KeyCode.F) && activeSkill2 != null)
        //{
        //    Debug.Log("Using Skill 2: " + activeSkill2.name);
        //    StartCoroutine(SkillManager.Instance.UseSkill(activeSkill2));
        //}
        // กด Q เพื่อใช้ ..... Skill
        if (Input.GetKeyDown(KeyCode.Q) && superSpeedSkill != null)
        {
            StartCoroutine(SkillManager.Instance.UseSkill(superSpeedSkill));
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x, y);

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

    [Header("Skill Selection")]
    public Skill activeSkill1; // สำหรับปุ่ม E
    public Skill activeSkill2; // สำหรับปุ่ม F

    public void SetSkill(int skillSlot, Skill newSkill)
    {
        if (skillSlot == 1)
        {
            activeSkill1 = newSkill;
            Debug.Log("Skill 1 (E) set to: " + newSkill.name);
        }
        else if (skillSlot == 2)
        {
            activeSkill2 = newSkill;
            Debug.Log("Skill 2 (F) set to: " + newSkill.name);
        }
    }

    //void Start()
    //{
    //    // ตัวอย่างการเลือกสกิลใน Start (สามารถแก้ไขเป็น UI input ได้)
    //    SetSkill(1, ScriptableObject.CreateInstance<Dash>());       // ตั้ง Dash เป็นสกิลปุ่ม E
    //    SetSkill(2, ScriptableObject.CreateInstance<Teleport>());   // ตั้ง Teleport เป็นสกิลปุ่ม F
    //}

    #region onEnter/Exit
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            currentNPC = npc;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc) && currentNPC == npc)
        {
            currentNPC.UnInteract();
            currentNPC = null;
        }
    }
    #endregion
}