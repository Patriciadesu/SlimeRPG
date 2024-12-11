using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int _level = 1;
    [SerializeField] protected float _maxHealth = 100;
    [SerializeField] protected float health;
    [SerializeField] protected float _attackDamage = 10;
    [SerializeField] protected Skill normalAttack;
    [SerializeField] protected float speed = 1;

    protected abstract void Move(Vector2 velocity);

    protected abstract void Attack();

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
