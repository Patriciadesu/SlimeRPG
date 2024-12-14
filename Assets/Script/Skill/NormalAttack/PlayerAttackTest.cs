using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackTest", menuName = "Skill/NormalAttack/PlayerAttackTest", order = 1)]
public class PlayerAttackTest : NormalAttack
{
    [SerializeField] private Vector2 attackCast = new Vector2(5, 3);

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        ////////////// ATTACK //////////////
        float damage = Player.Instance.AttackDamage;

        var plrPos = Player.Instance.transform.position.ConvertTo<Vector2>();
        var mousePos = MouseInput.Instance.MousePos;
        var direction = (mousePos - plrPos).normalized;

        var rays = Physics2D.BoxCastAll(
            plrPos,
            attackCast,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg,
            direction,
            attackCast.x / 2f,
            LayerMask.GetMask("Enemy")
        );

        foreach (var ray in rays)
        {
            if (ray.collider != null && ray.collider.TryGetComponent(out Enemy enemy))
            {
                Debug.Log("TakeDamage: " + enemy);
                enemy.TakeDamage(damage);
            }
        }
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
