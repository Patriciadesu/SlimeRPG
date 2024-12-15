using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private List<Enemy> enemyTakedDamages = new List<Enemy>();
    private float damage { get; set; }
    private float knockbackPower { get; set; }
    private float tickTime { get; set; }
    private bool started = false;
    private KnockbackPosition knockbackPosition;
    private AttackType attackType;

    private enum AttackType
    {
        OneHit,
        MultiHit
    }

    public void Setup(KnockbackPosition knockbackPosition, float damage, float knockbackPower)
    {
        if (started) return;

        this.knockbackPosition = knockbackPosition;
        this.damage = damage;
        this.knockbackPower = knockbackPower;
        attackType = AttackType.OneHit;
        started = true;
    }

    public void Setup(KnockbackPosition knockbackPosition, float damage, float knockbackPower, float tickTime)
    {
        if (started) return;

        this.knockbackPosition = knockbackPosition;
        this.damage = damage;
        this.knockbackPower = knockbackPower;
        this.tickTime = tickTime;
        attackType = AttackType.MultiHit;
        started = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!started) return;

        if (Player.Instance == null) return;

        Vector3 startPos = Vector3.zero;

        switch (knockbackPosition)
        {
            case KnockbackPosition.Player:
                startPos = Player.Instance.transform.position;
                break;
            case KnockbackPosition.Effect:
                startPos = transform.position;
                break;
        }

        if (collision.TryGetComponent(out Enemy enemy))
        {
            StartCoroutine(TakeDamageDelay(enemy, startPos));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!started) return;

        if (Player.Instance == null) return;

        Vector3 startPos = Vector3.zero;

        switch (knockbackPosition)
        {
            case KnockbackPosition.Player:
                startPos = Player.Instance.transform.position;
                break;
            case KnockbackPosition.Effect:
                startPos = transform.position;
                break;
        }

        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            StartCoroutine(TakeDamageDelay(enemy, startPos));
        }
    }

    private IEnumerator TakeDamageDelay(Enemy enemy, Vector3 startPos)
    {
        if (!enemyTakedDamages.Contains(enemy))
        {
            enemyTakedDamages.Add(enemy);

            if (startPos != Vector3.zero)
            {
                var enemyPos = enemy.transform.position;
                enemyPos.z = startPos.z;

                var direction = (enemyPos - startPos).normalized;

                enemy.GetComponent<Rigidbody2D>().AddRelativeForce(direction * knockbackPower, ForceMode2D.Impulse);
            }

            enemy.TakeDamage(damage);

            if (attackType == AttackType.MultiHit)
            {
                yield return new WaitForSeconds(tickTime);

                enemyTakedDamages.Remove(enemy);
            }
        }
    }
}

public enum KnockbackPosition
{
    Player,
    Effect
}