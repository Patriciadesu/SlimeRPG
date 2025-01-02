using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperSpeed", menuName = "Skill/Mobility/SuperSpeed", order = 1)]
public class SuperSpeed : Mobility
{
    public float UseTime = 2f; // ถึงเวลาใช้ความเร็วสูงสุดในไม่กี่วินาที
    private float originalSpeed; // เพื่อเก็บความเร็วดั้งเดิมของตัวละคร

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        Player character = Player.Instance;

        if (character == null)
        {
            isActive = true;
            yield break;
        }

        originalSpeed = character.Speed; // เก็บความเร็วเดิมไว้
        character.Speed *= 2; //เพิ่มความเร็วเป็น 2 เท่า (หรือค่าใดก็ได้ที่ต้องการ)

        yield return new WaitForSeconds(UseTime);

        // เปลี่ยนความเร็วกลับเป็นค่าเดิม
        if (character != null)
        {
            character.Speed = originalSpeed;
        }

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}