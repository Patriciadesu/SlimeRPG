using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackTest", menuName = "Skill/NormalAttack/PlayerAttackTest", order = 1)]
public class PlayerAttackTest : NormalAttack
{
    [SerializeField] private Vector2 attackCast = new Vector2(5, 3);
    [SerializeField] private float knockbackPower;

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        if (Player.Instance == null)
        {
            isActive = true;
            yield break;
        }

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
                enemy.GetComponent<Rigidbody2D>().AddRelativeForce(direction * knockbackPower * 7.5f, ForceMode2D.Impulse);
            }
        }
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
