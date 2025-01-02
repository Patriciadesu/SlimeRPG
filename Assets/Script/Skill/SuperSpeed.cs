using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperSpeed", menuName = "Skill/Mobility/SuperSpeed", order = 1)]
public class SuperSpeed : Mobility
{
    public float UseTime = 2f; // �֧��������������٧�ش�������Թҷ�
    private float originalSpeed; // �����纤������Ǵ������ͧ����Ф�

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

        originalSpeed = character.Speed; // �纤�������������
        character.Speed *= 2; //�������������� 2 ��� (���ͤ��㴡������ͧ���)

        yield return new WaitForSeconds(UseTime);

        // ����¹�������ǡ�Ѻ�繤�����
        if (character != null)
        {
            character.Speed = originalSpeed;
        }

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}