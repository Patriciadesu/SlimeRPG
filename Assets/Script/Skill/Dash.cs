using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Skill/Mobility/Dash", order = 1)]
public class Dash : Mobility
{
    public float dashSpeed = 1;   // ความเร็วในการ Dash

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
        mousePosition.z = 0;

        // คำนวณทิศทางจากตำแหน่งผู้เล่นไปยังตำแหน่งที่คลิก
        Vector3 direction = (mousePosition - character.transform.position).normalized;

        character.GetComponent<Rigidbody2D>().AddRelativeForce(direction * dashSpeed * 50, ForceMode2D.Impulse);

        // ใช้ Rigidbody2D เคลื่อนที่ไปยังตำแหน่งเป้าหมาย
        //character.rb2D.linearVelocity = direction * dashSpeed;

        //// รอให้การเคลื่อนที่เสร็จสิ้น
        //yield return new WaitForSeconds(0.1f);

        //// หยุดการเคลื่อนที่
        //character.rb2D.linearVelocity = Vector2.zero;
        // หลังจากคูลดาวน์เสร็จ
        // ตั้งเวลา cooldown
        Debug.Log("Dash skill completed");

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}