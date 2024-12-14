//using UnityEngine;
//using System.Collections;
//[CreateAssetMenu(fileName = "Dash", menuName = "Skill/Mobility/Dash", order = 1)]
//public class Dash : Mobility
//{
//    public float dashSpeed = 10f; // ความเร็วในการ dash
//    public float dashDistance = 5f; // ระยะทางสูงสุดที่ dash ได้
//    private Camera mainCamera;

//    private void Awake()
//    {
//        // ดึง Camera หลัก
//        mainCamera = Camera.main;
//    }

//    public override IEnumerator OnUse()
//    {
//        Player character = Player.Instance; // อ้างอิงตัวละครผู้เล่น
//        if (character == null || mainCamera == null)
//        {
//            Debug.LogError("Player or Camera is missing!");
//            yield break;
//        }

//        // ดึงตำแหน่งเมาส์ในหน้าจอ
//        Vector3 mousePosition = Input.mousePosition;

//        // แปลงตำแหน่งเมาส์จากหน้าจอ (Screen Position) เป็นตำแหน่งในโลก (World Position)
//        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

//        // คำนวณทิศทางจากตัวละครไปยังตำแหน่งเป้าหมาย
//        Vector2 characterPosition = character.transform.position;
//        Vector2 direction = (worldPosition - characterPosition).normalized;

//        // คำนวณตำแหน่งปลายทางโดยใช้ระยะทางสูงสุด
//        Vector2 targetPosition = characterPosition + direction * dashDistance;

//        // ตรวจสอบการชนวัตถุ (ถ้ามีระบบสิ่งกีดขวาง)
//        RaycastHit2D hit = Physics2D.Raycast(characterPosition, direction, dashDistance);
//        if (hit.collider != null)
//        {
//            // ถ้าชนสิ่งกีดขวาง ให้เปลี่ยนตำแหน่งปลายทางเป็นตำแหน่งที่ชน
//            targetPosition = hit.point;
//        }

//        // เริ่ม dash
//        yield return DashMovement(character, targetPosition);
//    }

//    private IEnumerator DashMovement(Character character, Vector2 targetPosition)
//    {
//        Vector2 startPosition = character.transform.position; // ตำแหน่งเริ่มต้น
//        float journeyTime = dashDistance / dashSpeed; // เวลาในการ dash
//        float elapsedTime = 0f;

//        // การเคลื่อนที่แบบราบรื่น
//        while (elapsedTime < journeyTime)
//        {
//            elapsedTime += Time.deltaTime;
//            character.transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / journeyTime);
//            yield return null;
//        }

//        // ตั้งตำแหน่งตัวละครให้อยู่ที่ปลายทาง
//        character.transform.position = targetPosition;
//    }
//}
