using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperSpeed", menuName = "Skill/Mobility/SuperSpeed", order = 1)]
public class SuperSpeed : Mobility
{
    public float UseTime = 2f; // ����������ҹ SuperSpeed
    public float SpeedMultiplier = 3f; // �������Ƿ������ö��Ѻ�� (Multiplier)
    private float originalSpeed; // �������Ǵ������ͧ����Ф�

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

        // �纤������������������������ǵ����ҷ���˹�
        originalSpeed = character.Speed;
        character.Speed *= SpeedMultiplier;
        Debug.Log($"Speed increased to {character.Speed}.");

        yield return new WaitForSeconds(UseTime);

        // �׹��Ҥ����������
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