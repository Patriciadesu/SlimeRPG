using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    [SerializeField] protected int _level = 1;
    public float MaxHealth { get => _maxHealth + (_maxHealth * (_level - 1) * 0.2f); }
    [SerializeField] private float _maxHealth = 100;
    public float health { get; protected set; }
    public float AttackDamage { get => _attackDamage + (_attackDamage * (_level - 1) * 0.2f); }
    [SerializeField] private float _attackDamage = 10;
    [SerializeField] protected NormalAttack normalAttack;
    [SerializeField] protected float speed = 1;
    [SerializeField] public float range = 5f;

    protected virtual void Awake() {
        rb2D = GetComponent<Rigidbody2D>();

        _level = Mathf.Max(_level, 1);
        health = MaxHealth;
    }
    public float Speed
    {
        get { return speed; }
        set { speed = Mathf.Max(value, 0); } // �������� speed ���¡��� 0
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
        float newHealth = Mathf.Min(health + h, MaxHealth); // ไม่ให้เกิน MaxHealth
        float healedAmount = newHealth - health; // คำนวณจำนวนที่เพิ่มขึ้นจริง
        health = newHealth;

        Debug.Log($"{gameObject.name} healed for {healedAmount}. Current Health: {health}/{MaxHealth}");
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void Move(Vector2 velocity)
    {

        rb2D.linearVelocity = Vector2.Lerp(rb2D.linearVelocity, velocity * speed * (this is Player ? 3 : 2.5f), Time.fixedDeltaTime * 5);

        float currentX = transform.rotation.eulerAngles.x;
        float currentZ = transform.rotation.eulerAngles.z;

        if ((velocity * speed).x > 0)
            transform.rotation = Quaternion.Euler(currentX, 0, currentZ);
        else if ((velocity * speed).x < 0)
            transform.rotation = Quaternion.Euler(currentX, 180, currentZ);
    }

    protected abstract void Attack();

    public virtual void ApplyDebuff(DamageOverTimeDebuff debuff)
    {
        StartCoroutine(debuff.ApplyDebuff(this));
    }
    private void OnDrawGizmosSelected()
    {
        if (Player.Instance != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Player.Instance.transform.position, range);
        }
    }

}
