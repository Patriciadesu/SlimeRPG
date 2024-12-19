using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SprintToEnemy", menuName = "Skill/Mobility/SprintToEnemy", order = 1)]
public class SprintToEnemy : Mobility
{
    public float knockbackRadious = 5f;      // Force applied to knockback the enemy
    public float knockbackForce = 1;      // Force applied to knockback the enemy
    public LayerMask obstacleLayer;        // Layer to detect obstacles

    //public float skillCooldown = 5f;       // Cooldown duration in seconds
    //private bool isCooldown = false;       // Is the skill on cooldown?

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;
        //if (isCooldown)
        //{
        //    Debug.Log("Skill is on cooldown.");
        //    yield break;
        //}

        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player not found.");
            isActive = true;
            yield break;
        }

        // Get the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = character.transform.position.z; // Align with the player's Z-axis in a 2D game

        // Calculate direction and target position
        Vector3 directionToMouse = (mousePosition - character.transform.position).normalized;
        Vector3 targetPosition = Vector3.zero;

        // Check for obstacles along the path
        float distanceToMouse = Vector3.Distance(character.transform.position, mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(character.transform.position, directionToMouse, distanceToMouse, obstacleLayer);
        if (hit.collider != null)
        {
            targetPosition = hit.point; // Adjust target position to the obstacle point
            Debug.Log("Obstacle detected. Sprinting to closest possible point.");
        }

        if (targetPosition == Vector3.zero)
        {
            isActive = true;
            yield break;
        }

        // Perform the sprint movement
        SprintMovement(character, targetPosition);

        // Apply knockback to the enemy (if any nearby)
        var rays = Physics2D.CircleCastAll(targetPosition, knockbackRadious, Vector2.zero, knockbackRadious, obstacleLayer);
        foreach (var ray in rays)
        {
            if (ray.transform.TryGetComponent(out Enemy enemy))
            {
                KnockbackEnemy(enemy, ray.transform.position - targetPosition);
            }
        }

        yield return new WaitForSeconds(coolDown);

        isActive = true;
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
            enemyRb.AddRelativeForce(direction.normalized * knockbackForce * 35, ForceMode2D.Impulse);
            Debug.Log("Enemy knocked back with force: " + knockbackForce);
        }
    }
}