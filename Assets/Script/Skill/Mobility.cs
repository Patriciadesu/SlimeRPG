using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Mobility : Skill
{
    public ActivateType activateType;

    public override IEnumerator OnUse()
    {
        Debug.Log("Using Mobility skill");
        yield break;
    }
}
[CreateAssetMenu(fileName = "Dash", menuName = "Skill/Mobility/Dash", order = 1)]
public class Dash : Mobility
{
    public float dashDistance = 5f; // ระยะทางที่ Dash
    public float dashSpeed = 20f;   // ความเร็วในการ Dash

    public override IEnumerator OnUse()
    {
        Debug.Log("Using Dash skill");

        Player character = Player.Instance;
        if (character == null) yield break;

        // ดึงตำแหน่งจากการคลิกที่หน้าจอ
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // คำนวณทิศทางจากตำแหน่งผู้เล่นไปยังตำแหน่งที่คลิก
        Vector3 direction = (mousePosition - character.transform.position).normalized;

        // คำนวณตำแหน่งเป้าหมาย
        Vector3 targetPosition = character.transform.position + direction * dashDistance;


        character.GetComponent<Rigidbody2D>().AddRelativeForce(direction *  dashSpeed, ForceMode2D.Impulse);

        // ใช้ Rigidbody2D เคลื่อนที่ไปยังตำแหน่งเป้าหมาย
        //character.rb2D.linearVelocity = direction * dashSpeed;

        //// รอให้การเคลื่อนที่เสร็จสิ้น
        //yield return new WaitForSeconds(0.1f);

        //// หยุดการเคลื่อนที่
        //character.rb2D.linearVelocity = Vector2.zero;

        Debug.Log("Dash skill completed");
    }
}
[CreateAssetMenu(fileName = "Teleport", menuName = "Skill/Mobility/Teleport", order = 1)]
public class Teleport : Mobility
{
    public float teleportDistance = 10f; // ระยะที่สามารถ Teleport ได้
    public LayerMask obstacleLayer;      // Layer สำหรับตรวจสอบสิ่งกีดขวาง (ถ้ามี)
    private Vector3 targetPosition;      // ตำแหน่งเป้าหมายสำหรับ Teleport

    public override IEnumerator OnUse()
    {
        Debug.Log("Using Teleport skill");

        // ดึงตัวละคร Player
        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player is missing!");
            yield break;
        }

        // ดึงตำแหน่ง Mouse (cursor) ในโลก 3D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ตั้งค่า z ให้เป็น 0 เพื่อให้เหมาะกับเกม 2D

        // คำนวณทิศทางจากตำแหน่งของผู้เล่นไปยังตำแหน่งของ cursor
        Vector3 direction = (mousePosition - character.transform.position).normalized;

        // คำนวณตำแหน่งเป้าหมายที่จะ Teleport โดยคำนึงถึงระยะทางสูงสุด
        targetPosition = character.transform.position + direction * teleportDistance;

        // ตรวจสอบสิ่งกีดขวาง (ถ้ามี)
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, direction, teleportDistance, obstacleLayer);
        if (hit.collider != null)
        {
            // หากเจอสิ่งกีดขวาง Teleport ได้จนถึงจุดที่ชน
            targetPosition = hit.point;
            Debug.Log("Obstacle detected, teleporting to nearest point.");
        }

        //// เล่น Animation Teleport (ถ้ามี)
        //if (character.animator != null)
        //{
        //    character.animator.SetTrigger("Teleport");
        //}

        // ย้ายตัวละครไปยังตำแหน่งที่คำนวณได้
        character.transform.position = targetPosition;

        Debug.Log("Teleport skill completed");
        yield return null;
    }

    // Optional: method to set target position from player input (mouse position)
    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
}

[CreateAssetMenu(fileName = "SuperSpeed", menuName = "Skill/Mobility/SuperSpeed", order = 1)]
public class SuperSpeed : Mobility
{
    public float UseTime = 2f; // Time to apply super speed in seconds
    private float originalSpeed; // To store the character's original speed

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

        originalSpeed = character.Speed; // Store the original speed
        character.Speed *= 2; // Increase speed by a factor of 2 (or any desired value)

        yield return new WaitForSeconds(UseTime);

        // Revert the speed to original value
        if (character != null)
        {
            character.Speed = originalSpeed;
        }

        yield return new WaitForSeconds(coolDown);

        isActive = true;

        // Check if the speed is not already boosted
        /*if (!isSpeedActive)
        {
            isSpeedActive = true;
            Debug.Log("Using SuperSpeed skill for " + UseTime + " seconds");
            // Increase character speed
            Character character = GetComponent<Character>();
            if (character != null)
            {
                originalSpeed = character.Speed; // Store the original speed
                character.Speed *= 2; // Increase speed by a factor of 2 (or any desired value)
            }

            // Start the timer to revert speed after UseTime seconds
            CoroutineRunner.instance.StartCoroutine(SpeedCooldown());
        }*/
    }

    /*private IEnumerator SpeedCooldown()
    {
        yield return new WaitForSeconds(UseTime);

        // Revert the speed to original value
        Character character = GetComponent<Character>();
        if (character != null)
        {
            character.Speed = originalSpeed;
        }

        Debug.Log("SuperSpeed expired, reverting speed.");
        isSpeedActive = false;
    }*/
}
[CreateAssetMenu(fileName = "SprintToEnemy", menuName = "Skill/Mobility/SprintToEnemy", order = 1)]
public class SprintToEnemy : Mobility
{
    public Enemy Enemy; // Reference to the enemy to sprint toward
    public float sprintDistance = 2f; // Distance to sprint toward the enemy
    public float damageMultiplier = 1.5f; // Damage multiplier after sprinting

    public override IEnumerator OnUse()
    {
        Player character = Player.Instance;
        if (Enemy != null && character != null)
        {
            Debug.Log("Using SprintToEnemy skill on enemy: " + Enemy.name + " with damage multiplier: " + damageMultiplier);

            // Calculate direction from the player to enemy
            Vector3 directionToEnemy = (Enemy.transform.position - character.transform.position).normalized;

            // calculate the new position to move to
            Vector3 sprintPosition = character.transform.position + directionToEnemy * sprintDistance;

            // move player to new position
            yield return SprintMovement(character, sprintPosition);

            // after sprint, increase the character's damage
            character.Damage *= damageMultiplier;
            Debug.Log("Damage increased by multiplier: " + damageMultiplier);

            // revert damage increase after a period of time
            yield return new WaitForSeconds(2f);
            character.Damage /= damageMultiplier;
            Debug.Log("Damage reverted to original.");
        }
    }

    private IEnumerator SprintMovement(Character character, Vector2 targetPosition)
    {
        float sprintSpeed = 10f;
        float distance = Vector2.Distance(character.transform.position, targetPosition);
        float journeyTime = distance / sprintSpeed;
        float startTime = Time.time;

        // move toward the target position
        while (Time.time - startTime < journeyTime)
        {
            character.transform.position = Vector2.Lerp(character.transform.position, targetPosition, (Time.time - startTime) / journeyTime);
            yield return null;
        }

        character.transform.position = targetPosition; // Ensure final position is accurate
    }
}
