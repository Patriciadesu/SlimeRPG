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

    //[Header("Skill")]
    //private Dash dashSkill; // Reference to the Dash skill
    //private SuperSpeed superSpeedSkill; // เพิ่มตัวแปรเพื่อเก็บ SuperSpeed skill
    //private SprintToEnemy SprintToEnemy;// เพิ่มตัวแปรเพื่อเก็บ SprintToEnemy
    //private Teleport Teleport;// เพิ่มตัวแปรเพื่อเก็บ Teleport
    [Header("Skill")]
    [SerializeField] private ActiveSkill activeSkill1;
    [SerializeField] private ActiveSkill activeSkill2;
    [SerializeField] private Mobility mobilitySkill;

    // [Header("Inventory")]
    // public Inventory inventory;
    //public Rigidbody2D rb2D;
    //private Skill activeQSkill; // สกิลที่จะใช้เมื่อกดปุ่ม Q
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseMobility();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill1();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseSkill2();
        }
        PlayerInput();
    }
    
    protected void Attack()
    {
        if (normalAttack != null)
            StartCoroutine(SkillManager.Instance.UseSkill(normalAttack));
    }

    private void UseMobility()
    {
        if (mobilitySkill != null)
            StartCoroutine(SkillManager.Instance.UseSkill(mobilitySkill));
    }

    private void UseSkill1()
    {
        if (activeSkill1 != null)
            StartCoroutine(SkillManager.Instance.UseSkill(activeSkill1));
    }
    private void UseSkill2()
    {
        if (activeSkill2 != null)
            StartCoroutine(SkillManager.Instance.UseSkill(activeSkill2));
    }
    private void PlayerInput()
    {
        // กด Q เพื่อใช้ ..... Skill
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Debug.Log("Q key pressed");

        //    // ใช้ Dash ที่ถูกต้องจาก ScriptableObject
        //    Dash dashSkill = ScriptableObject.CreateInstance<Dash>();
        //    StartCoroutine(SkillManager.Instance.UseSkill(dashSkill));
        //}

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Debug.Log("Q key pressed");

        //    // เรียกใช้ Teleport Skill
        //    if (Teleport == null)
        //    {
        //        Teleport = ScriptableObject.CreateInstance<Teleport>();
        //    }
        //    StartCoroutine(SkillManager.Instance.UseSkill(Teleport));
        //}

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    // ตรวจสอบว่า activeQSkill ไม่เป็น null ก่อนการใช้
        //    if (activeQSkill != null)
        //    {
        //        Debug.Log("Using skill assigned to Q: " + activeQSkill.name);
        //        StartCoroutine(SkillManager.Instance.UseSkill(activeQSkill));
        //    }
        //    else
        //    {
        //        Debug.LogWarning("No skill assigned to Q!");
        //    }
        //}
        //// กด E หรือ Q เพื่อใช้skill heal
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    StartCoroutine(SkillManager.Instance.UseSkill(new HealSkill()));
        //}
        //else if (Input.GetKeyDown(KeyCode.F))
        //{
        //    StartCoroutine(SkillManager.Instance.UseSkill(new HealSkill()));
        //}
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x, y);

        Move(movement);
        //if (Input.GetKeyDown(KeyCode.Q) && activeQSkill != null)
        //{
        //    Debug.Log("Using skill assigned to Q: " + activeQSkill.name);
        //    StartCoroutine(SkillManager.Instance.UseSkill(activeQSkill));
        //}
        //else if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Debug.LogWarning("No skill assigned to Q!");
        //}
    }

    protected void Move(Vector2 velocity)
    {

        rb2D.linearVelocity = Vector2.Lerp(rb2D.linearVelocity, velocity * speed * 3, Time.fixedDeltaTime * 5);

        float currentX = transform.rotation.eulerAngles.x;
        float currentZ = transform.rotation.eulerAngles.z;

        if ((velocity * speed).x > 0)
            transform.rotation = Quaternion.Euler(currentX, 0, currentZ);
        else if ((velocity * speed).x < 0)
            transform.rotation = Quaternion.Euler(currentX, 180, currentZ);
    }

    protected override void Die()
    {
        // Do something with die mechanics
        base.Die();
    }

    public override void Heal(float amount)
    {
        //health = Mathf.Min(MaxHealth, health + amount);
        base.Heal(amount);
        Debug.Log($"Player healed by {amount}. Current health: {health}");
    }

    //[Header("Skill Selection")]
    //public Skill activeSkill1; // สำหรับปุ่ม E
    //public Skill activeSkill2; // สำหรับปุ่ม F

    //public void SetSkill(int skillSlot, Skill newSkill)
    //{
    //    if (skillSlot == 1)
    //    {
    //        activeSkill1 = newSkill;
    //        Debug.Log("Skill 1 (E) set to: " + newSkill.name);
    //    }
    //    else if (skillSlot == 2)
    //    {
    //        activeSkill2 = newSkill;
    //        Debug.Log("Skill 2 (F) set to: " + newSkill.name);
    //    }
    //}
    //public void AddSkill1()
    //{
    //    if (activeQSkill == null) // ตรวจสอบว่าไม่ได้กำหนดสกิลไว้แล้ว
    //    {
    //        activeQSkill = ScriptableObject.CreateInstance<Dash>();
    //        Debug.Log("AddSkill1: Dash skill assigned to Q");
    //    }
    //    else
    //    {
    //        Debug.Log("Q skill already assigned: " + activeQSkill.name);
    //    }
    //}

    //public void AddSkill2()
    //{
    //    if (activeQSkill == null) // ตรวจสอบว่าไม่ได้กำหนดสกิลไว้แล้ว
    //    {
    //        activeQSkill = ScriptableObject.CreateInstance<Teleport>();
    //        Debug.Log("AddSkill2: Teleport skill assigned to Q");
    //    }
    //    else
    //    {
    //        Debug.Log("Q skill already assigned: " + activeQSkill.name);
    //    }
    //}
    //public void OnClick_AddSkill1()
    //{
    //    if (activeQSkill == null)
    //    {
    //        AddSkill1(); // เรียกใช้ฟังก์ชัน AddSkill1() เพื่อกำหนด Dash ให้กับ Q
    //        Debug.Log("Dash skill assigned to Q.");
    //    }
    //    else
    //    {
    //        Debug.Log("Q skill already assigned: " + activeQSkill.name);
    //    }
    //}

    //public void OnClick_AddSkill2()
    //{
    //    if (activeQSkill == null)
    //    {
    //        AddSkill2(); // เรียกใช้ฟังก์ชัน AddSkill2() เพื่อกำหนด Teleport ให้กับ Q
    //        Debug.Log("Teleport skill assigned to Q.");
    //    }
    //    else
    //    {
    //        Debug.Log("Q skill already assigned: " + activeQSkill.name);
    //    }
    //}
    public void AddSkill(Skill skill ,SkillSlotType skillSlotType)
    {
        switch (skillSlotType)
        {
            case SkillSlotType.NormalAttack:
                if(skill is NormalAttack _normalAttack)
                {
                    normalAttack = _normalAttack;
                }
                break;
            case SkillSlotType.ActiveSkill1:
                if(skill is ActiveSkill _ActiveSkill_1)
                {
                    activeSkill1 = _ActiveSkill_1;
                }
                break;
            case SkillSlotType.ActiveSkill2:
                if (skill is ActiveSkill _ActiveSkill_2)
                {
                    activeSkill2 = _ActiveSkill_2;
                }
                break;
            case SkillSlotType.Mobility:
                if (skill is Mobility _mobility)
                {
                    mobilitySkill = _mobility;
                }
                break;
        }
    }

    public enum SkillSlotType
    {
        NormalAttack,
        ActiveSkill1,
        ActiveSkill2,
        Mobility
    }
}