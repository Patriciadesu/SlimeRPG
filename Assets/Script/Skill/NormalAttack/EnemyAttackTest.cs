using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackTest", menuName = "Skill/EnemyAttack/EnemyAttackTest", order = 1)]
public class EnemyAttackTest : EnemyAttack
{
    [SerializeField] private float knockbackPower;
    [SerializeField] private Vector2 attackCast = new Vector2(5, 3);

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        if (enemy == null || Player.Instance == null)
        {
            isActive = true;
            yield break;
        }

        ////////////// ATTACK //////////////
        float damage = enemy.AttackDamage;

        var plrPos = Player.Instance.transform.position;
        var enemyPos = enemy.transform.position;
        var direction = (plrPos - enemyPos).normalized;

        yield return new WaitForSeconds(stayTime);

        if (enemy == null)
        {
            isActive = true;
            yield break;
        }

        var animator = enemy.GetComponent<Animator>();

        animator.SetTrigger("attack");

        var rays = Physics2D.BoxCastAll(
            enemyPos,
            attackCast,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg,
            direction,
            attackCast.x / 2f,
            LayerMask.GetMask("Player")
        );

        foreach (var ray in rays)
        {
            if (ray.collider != null && ray.collider.TryGetComponent(out Player player))
            {
                player.TakeDamage(damage);
                player.GetComponent<Rigidbody2D>().AddRelativeForce(direction * knockbackPower);
            }
        }
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
