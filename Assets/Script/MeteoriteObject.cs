using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteoriteObject : MonoBehaviour
{
    private List<Enemy> enemyTakedDamages = new List<Enemy>();
    private float damage { get; set; }
    private float knockbackPower { get; set; }
    private bool started = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;
    }

    public void Setup(float damage, float knockbackPower)
    {
        if (started) return;

        this.damage = damage;
        this.knockbackPower = knockbackPower;
        started = true;

        StartCoroutine(MeteoriteFall());
    }

    private IEnumerator MeteoriteFall()
    {
        float startTime = 0;

        var normalPos = transform.position;
        var startPos = normalPos + new Vector3(0, 20, 0);

        transform.position = startPos;

        spriteRenderer.enabled = true;

        animator.SetTrigger("fall");

        while (startTime < 1)
        {
            transform.position = Vector3.Lerp(startPos, normalPos, startTime);
            yield return null;
            startTime += Time.deltaTime;
        }

        transform.position = normalPos;

        animator.SetTrigger("explode");
        
        CameraShaker.Instance.TriggerShake(0.065f, 2, 0.8f);
    }

    private void DestroySelf() => Destroy(gameObject);

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!started) return;

        if (Player.Instance == null) return;

        Vector3 startPos = transform.position;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            TakeDamage(enemy, startPos);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!started) return;

        if (Player.Instance == null) return;

        Vector3 startPos = transform.position;

        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            TakeDamage(enemy, startPos);
        }
    }

    private void TakeDamage(Enemy enemy, Vector3 startPos)
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
        }
    }
}
