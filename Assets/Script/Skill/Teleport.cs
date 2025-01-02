using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "Skill/Mobility/Teleport", order = 1)]
public class Teleport : Mobility
{
    public float teleportDistance = 10f; // ระยะทางที่ผู้เล่นสามารถเทเลพอร์ตได้
    public LayerMask obstacleLayer;      // ชั้นสำหรับตรวจจับสิ่งกีดขวาง


    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        Debug.Log("Using Teleport skill");

        // รับอินสแตนซ์ของผู้เล่น
        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player is missing!");
            isActive = true;
            yield break;
        }

        // รับตำแหน่งMouse
        Vector3 mousePos = MouseInput.Instance.MousePos;
        mousePos.z = character.transform.position.z;
        float distance = (mousePos - character.transform.position).magnitude;

        // คำนวณระยะและเป้าหมาย
        Vector3 direction = (mousePos - character.transform.position).normalized;
        Vector3 targetPosition;
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, direction, distance, obstacleLayer);
        if (hit.collider != null)
        {
            targetPosition = hit.point; // ปรับไปยังจุดที่ใกล้ที่สุดหากถูกบล็อก
            Debug.Log("Obstacle detected, teleporting to nearest point.");
        }
        else if (distance <= teleportDistance)
        {
            targetPosition = mousePos; // teleportโดยตรงไปยังตำแหน่งmouse
        }
        else
        {
            targetPosition = character.transform.position + (direction * teleportDistance); // ระยะTeleport สูงสุด
        }

        // teleport ผู้เล่น
        character.transform.position = targetPosition;

        Debug.Log("Teleport skill completed");

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}