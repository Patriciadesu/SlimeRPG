using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "EnemySlashAttack", menuName = "Skill/EnemyAttack/EnemySlashAttack", order = 1)]
public class EnemySlashAttack : EnemyAttack
{
    [SerializeField] private Vector2 effectScale = new Vector2(2.5f, 1.5f);
    [SerializeField] private Vector2 attackCast = Vector2.one * 3;
    [SerializeField] private float attackMultiply = 1;
    [SerializeField] private float knockbackPower;

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
        float damage = enemy.AttackDamage * attackMultiply;

        var plrPos = Player.Instance.transform.position;
        var enemyPos = enemy.transform.position;
        enemyPos.z = plrPos.z;
        var direction = (plrPos - enemyPos).normalized;

        yield return new WaitForSeconds(stayTime);

        if (enemy == null)
        {
            isActive = true;
            yield break;
        }

        var animator = enemy.GetComponent<Animator>();

        animator.SetTrigger("attack");

        var effectObj = EffectManager.Instance.CreateEffect(
            EffectManager.Effect.SLASH,
            enemyPos + (direction * (effectScale.x / 1.5f)),
            Quaternion.Euler(
                0,
                direction.x > 0 ? -180 : 0,
                direction.x < 0 ?
                Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg :
                -Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg + 180
            ),
            enemy.transform
        );
        effectObj.localScale = effectScale;

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
                player.GetComponent<Rigidbody2D>().AddRelativeForce(direction * knockbackPower * 7.5f, ForceMode2D.Impulse);
            }
        }
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
