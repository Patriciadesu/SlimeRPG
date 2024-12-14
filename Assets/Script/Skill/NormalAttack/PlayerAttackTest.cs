using System.Collections;
using UnityEngine;

public class PlayerAttackTest : NormalAttack
{
    [SerializeField] protected Vector2 attackCast = new Vector2(5, 3);

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        ////////////// ATTACK //////////////
        /*Enemy enemy = Enemy.closestEnemy;

        if (enemy != null)
        {
            enemy.TakeDamage(Player.Instance.AttackDamage);

            var cast = Physics2D.BoxCastAll()
        }*/
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
