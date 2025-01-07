using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Skill/Mobility/Dash", order = 1)]
public class Dash : Mobility
{
    public float dashDistance = 5f; // ระยะทาง Dash
    public float dashDuration = 0.2f; // ระยะเวลาที่ใช้ในการ Dash

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        Debug.Log("Using Dash skill");

        Player character = Player.Instance;
        if (character == null)
        {
            isActive = true;
            yield break;
        }

        // ดึงตำแหน่งจากการคลิกที่หน้าจอ
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = character.transform.position.z;

        // คำนวณทิศทาง Dash
        Vector3 direction = (mousePosition - character.transform.position).normalized;

        // คำนวณตำแหน่งเป้าหมาย
        Vector3 startPosition = character.transform.position;
        Vector3 targetPosition = startPosition + direction * dashDistance;

        // Dash ด้วย Lerp
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            character.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ตั้งตำแหน่งสุดท้ายให้เท่ากับเป้าหมาย
        character.transform.position = targetPosition;

        Debug.Log("Dash skill completed");


        yield return new WaitForSeconds(coolDown); // รอ Cooldown

        isActive = true;
    }
}