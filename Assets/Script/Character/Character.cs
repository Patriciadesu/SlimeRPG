using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    [SerializeField] protected int _level = 1;
    public float MaxHealth { get { return _maxHealth + (_maxHealth * _level * 0.2f); } }
    [SerializeField] private float _maxHealth = 100;
    public float health { get; protected set; }
    public float AttackDamage { get { return _attackDamage + (_attackDamage * _level * 0.2f); } }
    [SerializeField] private float _attackDamage = 10;
    [SerializeField] protected NormalAttack normalAttack;
    [SerializeField] protected float speed = 1;

    protected virtual void Awake() {
        rb2D = GetComponent<Rigidbody2D>();

        health = MaxHealth;
    }
    public float Speed
    {
        get { return speed; }
        set { speed = Mathf.Max(value, 0); } // จะไม่ให้ speed น้อยกว่า 0
    }
    public virtual void TakeDamage(float dmg)
    {
        health = Mathf.Max(health - dmg, 0);

        if (health <= 0)
            Die();

        Debug.Log($"{gameObject.name} Health: {health}");
    }

    public virtual void Heal(float h)
    {
        health = Mathf.Max(health + h, MaxHealth);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
