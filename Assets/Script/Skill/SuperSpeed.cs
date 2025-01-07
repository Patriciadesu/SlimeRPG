using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperSpeed", menuName = "Skill/Mobility/SuperSpeed", order = 1)]
public class SuperSpeed : Mobility
{
    public float UseTime = 2f; // ระยะเวลาใช้งาน SuperSpeed
    public float SpeedMultiplier = 3f; // ความเร็วที่สามารถปรับได้ (Multiplier)
    private float originalSpeed; // ความเร็วดั้งเดิมของตัวละคร

    public override IEnumerator OnUse()
    {
        if (!isActive)
        {
            Debug.Log("SuperSpeed is not active.");
            yield break;
        }

        isActive = false;
        Debug.Log("SuperSpeed skill activated!");

        Player character = Player.Instance;

        if (character == null)
        {
            Debug.LogWarning("Player instance is null. SuperSpeed cannot be used.");
            isActive = true;
            yield break;
        }

        // เก็บความเร็วเดิมและเพิ่มความเร็วตามค่าที่กำหนด
        originalSpeed = character.Speed;
        character.Speed *= SpeedMultiplier;
        Debug.Log($"Speed increased to {character.Speed}.");

        yield return new WaitForSeconds(UseTime);

        // คืนค่าความเร็วเดิม
        if (character != null)
        {
            character.Speed = originalSpeed;
            Debug.Log($"Speed reverted to {character.Speed}.");
        }

        yield return new WaitForSeconds(coolDown);

        Debug.Log("SuperSpeed skill cooldown completed.");
        isActive = true;
    }
}