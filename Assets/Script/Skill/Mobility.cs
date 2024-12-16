using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Mobility : Skill
{
    public ActivateType activateType;
    public float cooldownDuration = 3f; // Default cooldown duration
    private bool isCooldown = false;

    public override IEnumerator OnUse()
    {
        if (isCooldown)
        {
            Debug.Log($"{this.name} is on cooldown.");
            yield break;
        }

        Debug.Log($"Using {this.name} skill.");
        isCooldown = true;

        // Execute the skill logic here
        yield return SkillEffect();

        // Start cooldown
        yield return StartCooldown();
    }

    protected virtual IEnumerator SkillEffect()
    {
        Debug.Log($"Default skill effect for {this.name}");
        yield break;
    }

    private IEnumerator StartCooldown()
    {
        Debug.Log($"{this.name} cooldown started for {cooldownDuration} seconds.");
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        Debug.Log($"{this.name} cooldown finished.");
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
        // หลังจากคูลดาวน์เสร็จ
        // ตั้งเวลา cooldown
        Debug.Log("Dash skill completed");
    }
}
[CreateAssetMenu(fileName = "Teleport", menuName = "Skill/Mobility/Teleport", order = 1)]
public class Teleport : Mobility
{
    public float teleportDistance = 10f; // Distance the player can teleport
    public LayerMask obstacleLayer;      // Layer for detecting obstacles


    public override IEnumerator OnUse()
    {

        Debug.Log("Using Teleport skill");

        // Get the player instance
        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player is missing!");
            yield break;
        }

        // Get mouse position
        Vector3 mousePos = MouseInput.Instance.MousePos;
        mousePos.z = character.transform.position.z;
        float distance = (mousePos - character.transform.position).magnitude;

        // Calculate teleport direction and target
        Vector3 direction = (mousePos - character.transform.position).normalized;
        Vector3 targetPosition;
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, direction, distance, obstacleLayer);
        if (hit.collider != null)
        {
            targetPosition = hit.point; // Adjust to the nearest point if blocked
            Debug.Log("Obstacle detected, teleporting to nearest point.");
        }
        else if (distance <= teleportDistance)
        {
            targetPosition = mousePos; // Teleport directly to mouse position
        }
        else
        {
            targetPosition = character.transform.position + (direction * teleportDistance); // Max distance teleport
        }

        // Teleport the player
        character.transform.position = targetPosition;

        Debug.Log("Teleport skill completed");
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
    public float sprintSpeed = 20f;         // Speed of the sprint
    public float knockbackForce = 5f;      // Force applied to knockback the enemy
    public LayerMask obstacleLayer;        // Layer to detect obstacles

    //public float skillCooldown = 5f;       // Cooldown duration in seconds
    //private bool isCooldown = false;       // Is the skill on cooldown?

    public override IEnumerator OnUse()
    {
        //if (isCooldown)
        //{
        //    Debug.Log("Skill is on cooldown.");
        //    yield break;
        //}

        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player not found.");
            yield break;
        }

        // Get the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = character.transform.position.z; // Align with the player's Z-axis in a 2D game

        // Calculate direction and target position
        Vector3 directionToMouse = (mousePosition - character.transform.position).normalized;
        Vector3 targetPosition = mousePosition;

        // Check for obstacles along the path
        float distanceToMouse = Vector3.Distance(character.transform.position, mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, directionToMouse, distanceToMouse, obstacleLayer);
        if (hit.collider != null)
        {
            targetPosition = hit.point; // Adjust target position to the obstacle point
            Debug.Log("Obstacle detected. Sprinting to closest possible point.");
        }

        // Perform the sprint movement
        SprintMovement(character, targetPosition);

        // Apply knockback to the enemy (if any nearby)
        Enemy closestEnemy = FindClosestEnemy(character, targetPosition);
        if (closestEnemy != null)
        {
            KnockbackEnemy(closestEnemy, directionToMouse);
        }

    }


    private void SprintMovement(Player character, Vector3 targetPosition)
    {
        // Move the character directly toward the target position
        character.transform.position = targetPosition;
    }

    private void KnockbackEnemy(Enemy enemy, Vector3 direction)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            // Directly set velocity for knockback
            enemyRb.linearVelocity = direction * knockbackForce;
            Debug.Log("Enemy knocked back with force: " + knockbackForce);
        }
    }

    private Enemy FindClosestEnemy(Player character, Vector3 position)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length == 0) return null;

        Enemy closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
