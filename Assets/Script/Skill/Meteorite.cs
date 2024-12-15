using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Meteorite", menuName = "Skill/ActiveSkill/Meteorite", order = 1)]
public class Meteorite : ActiveSkill
{
    [SerializeField] private float meteoriteSize = 1;
    [SerializeField] private float damageMultiply = 2;
    [SerializeField] private float knockbackPower = 50;
    [SerializeField] private float stayTime = 0.75f;

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

        var mousePos = MouseInput.Instance.MousePos;

        var effectObj = EffectManager.Instance.CreateEffect(
            EffectManager.Effect.METEORITE,
            mousePos,
            Quaternion.identity
        );
        effectObj.localScale = Vector3.one * meteoriteSize;
        var attackObject = effectObj.AddComponent<MeteoriteObject>();
        attackObject.Setup(damage, knockbackPower);

        float normalSpeed = Player.Instance.Speed;

        Player.Instance.Speed = 0;

        yield return new WaitForSeconds(stayTime);

        Player.Instance.Speed = normalSpeed;

        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
