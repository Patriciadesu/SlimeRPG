using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "Skill/Mobility/Teleport", order = 1)]
public class Teleport : Mobility
{
    public float teleportDistance = 10f; // Distance the player can teleport
    public LayerMask obstacleLayer;      // Layer for detecting obstacles


    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        Debug.Log("Using Teleport skill");

        // Get the player instance
        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player is missing!");
            isActive = true;
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

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}