using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackTest", menuName = "Skill/NormalAttack/EnemyAttackTest", order = 1)]
public class EnemyAttackTest : NormalAttack
{
    [SerializeField] private float stayTime;
    [SerializeField] private Vector2 attackCast = new Vector2(5, 3);
    private Enemy enemy;

    public override IEnumerator OnUseForEnemy(Enemy enemy)
    {
        this.enemy = enemy;

        yield return OnUse();
    }

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        ////////////// ATTACK //////////////
        float damage = enemy.AttackDamage;

        var plrPos = Player.Instance.transform.position;
        var enemyPos = enemy.transform.position;
        var direction = (plrPos - enemyPos).normalized;

        yield return new WaitForSeconds(stayTime);

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
            }
        }
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
