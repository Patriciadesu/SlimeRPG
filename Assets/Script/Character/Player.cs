using Unity.Collections;
using UnityEngine;

public class Player : Character
{
    [Header("Singleton")]
    public static Player Instance{get; private set;}
    
    [Header("Player Stats")]
    public float dodgeRate{get; private set;}
    public float criticalRate{get; private set;}
    public float exp;
    public float coin;

    [Header("Collectable")]
    private float collectItemRange;

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

    protected override void Attack()
    {
        
    }
    private void PlayerInput(){
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x,y) * speed;
        Move(movement);
    }

    protected override void Move(Vector2 velocity)
    {
        rb2D.linearVelocity = velocity;
    }

    protected override void Die()
    {
        // Do something with die mechanics
        base.Die();
    }
}
