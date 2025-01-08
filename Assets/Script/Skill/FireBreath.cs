using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "FireBreath", menuName = "Skill/ActiveSkill/FireBreath", order = 1)]
public class FireBreath : ActiveSkill
{
    [SerializeField] private GameObject fireBreathEffectPrefab; // Prefab อันเดียว

    [SerializeField] private float fireBreathSize = 1;
    [SerializeField] private float damageMultiply = 0.15f;
    [SerializeField] private float knockbackPower = 4;
    [SerializeField] private float tickTime = 0.05f;
    [SerializeField] private float stayTime = 1;

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        if (Player.Instance == null)
        {
            isActive = true;
            yield break;
        }

        float normalSpeed = Player.Instance.Speed;

        try
        {
            ////////////// ATTACK //////////////
            float damage = Player.Instance.AttackDamage * damageMultiply;

            var plrPos = Player.Instance.transform.position.ConvertTo<Vector2>();
            var mousePos = MouseInput.Instance.MousePos;
            var direction = (mousePos - plrPos).normalized;

            var effectObj = Instantiate(
                fireBreathEffectPrefab,
                plrPos + (direction * (fireBreathSize * 0.15f)),
                Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg),
                Player.Instance.transform
            );

            effectObj.transform.localScale = Vector3.one * fireBreathSize;

            var attackObject = effectObj.AddComponent<AttackObject>();
            attackObject.Setup(KnockbackPosition.Player, damage, knockbackPower, tickTime);

            Player.Instance.Speed = 0;

            CameraShaker.Instance.TriggerShake(0.1f, stayTime, 0.3f);

            yield return new WaitForSeconds(stayTime);

            Destroy(attackObject.gameObject);
        }
        finally
        {
            // Ensure the player's speed is reset
            Player.Instance.Speed = normalSpeed;
        }

        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
