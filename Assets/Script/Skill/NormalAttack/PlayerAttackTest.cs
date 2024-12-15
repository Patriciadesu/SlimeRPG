using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackTest", menuName = "Skill/NormalAttack/PlayerAttackTest", order = 1)]
public class PlayerAttackTest : NormalAttack
{
    [SerializeField] private Vector2 effectScale = new Vector2(2.5f, 1.5f);
    [SerializeField] private Vector2 attackCast = Vector2.one * 3;
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

        var effectObj = EffectManager.Instance.CreateEffect(
            EffectManager.Effect.SLASH,
            plrPos + (direction * (effectScale.x / 1.5f)),
            Quaternion.Euler(
                0,
                direction.x > 0 ? -180 : 0,
                direction.x < 0 ?
                Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg :
                -Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg + 180
            ),
            Player.Instance.transform
        );
        effectObj.localScale = effectScale;

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
