using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

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