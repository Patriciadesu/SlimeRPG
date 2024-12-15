using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LightBeam", menuName = "Skill/ActiveSkill/LightBeam", order = 1)]
public class LightBeam : ActiveSkill
{
    [SerializeField] private float beamSize = 1;
    [SerializeField] private float spread = 90;
    [SerializeField] private int beamCount = 1;
    [SerializeField] private float damageMultiply = 1;
    [SerializeField] private float knockbackPower = 4;
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

        ////////////// ATTACK //////////////
        float damage = Player.Instance.AttackDamage * damageMultiply;

        var plrPos = Player.Instance.transform.position.ConvertTo<Vector2>();
        var mousePos = MouseInput.Instance.MousePos;
        var direction = (mousePos - plrPos).normalized;

        float startAngle = beamCount <= 1 ? 0 : - spread / 2f;
        float step = beamCount <= 1 ? 0 : spread / (beamCount - 1);

        for (int i = 0; i < beamCount; i++)
        {
            float angle = startAngle + (step * i);

            Vector2 newDirection = new Vector2(
                Mathf.Cos((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angle) * Mathf.Deg2Rad),
                Mathf.Sin((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angle) * Mathf.Deg2Rad)
            ).normalized;

            var effectObj = EffectManager.Instance.CreateEffect(
                EffectManager.Effect.LIGHTBEAM,
                plrPos + (newDirection * (beamSize * 9)),//plrPos + (direction * (beamSize * 9)),
                Quaternion.Euler(
                    0,
                    0,
                    Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg//Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg
                ),
                Player.Instance.transform
            );
            effectObj.localScale = Vector3.one * beamSize;
            var attackObject = effectObj.AddComponent<AttackObject>();
            attackObject.Setup(KnockbackPosition.Player, damage, knockbackPower);
        }

        float normalSpeed = Player.Instance.Speed;

        Player.Instance.Speed = 0;

        CameraShaker.Instance.TriggerShake(0.08f, stayTime, 0.55f);

        yield return new WaitForSeconds(stayTime);

        Player.Instance.Speed = normalSpeed;

        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
