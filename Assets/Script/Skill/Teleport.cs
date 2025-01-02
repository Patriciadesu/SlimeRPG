using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "Skill/Mobility/Teleport", order = 1)]
public class Teleport : Mobility
{
    public float teleportDistance = 10f; // ���зҧ������������ö��ž�����
    public LayerMask obstacleLayer;      // �������Ѻ��Ǩ�Ѻ��觡մ��ҧ


    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        Debug.Log("Using Teleport skill");

        // �Ѻ�Թ�ᵹ��ͧ������
        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player is missing!");
            isActive = true;
            yield break;
        }

        // �Ѻ���˹�Mouse
        Vector3 mousePos = MouseInput.Instance.MousePos;
        mousePos.z = character.transform.position.z;
        float distance = (mousePos - character.transform.position).magnitude;

        // �ӹǳ��������������
        Vector3 direction = (mousePos - character.transform.position).normalized;
        Vector3 targetPosition;
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, direction, distance, obstacleLayer);
        if (hit.collider != null)
        {
            targetPosition = hit.point; // ��Ѻ��ѧ�ش���������ش�ҡ�١���͡
            Debug.Log("Obstacle detected, teleporting to nearest point.");
        }
        else if (distance <= teleportDistance)
        {
            targetPosition = mousePos; // teleport�µç��ѧ���˹�mouse
        }
        else
        {
            targetPosition = character.transform.position + (direction * teleportDistance); // ����Teleport �٧�ش
        }

        // teleport ������
        character.transform.position = targetPosition;

        Debug.Log("Teleport skill completed");

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}