using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    [SerializeField] protected int _level = 1;
    [SerializeField] protected float _maxHealth = 100;
    public float health { get; protected set; }
    [SerializeField] protected float _attackDamage = 10;
    //[SerializeField] protected NormalAttack normalAttack;
    [SerializeField] protected float speed = 1;

    protected virtual void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(float dmg)
    {
        health = Mathf.Max(health - dmg, 0);

        if (health <= 0)
            Die();
    }

    public virtual void Heal(float h)
    {
        health = Mathf.Max(health + h, _maxHealth);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
