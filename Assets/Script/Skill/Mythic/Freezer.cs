using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    public float aliveTime; // Lifetime of the freezer
    private float timeRunner; // Timer to track alive time
    private bool startFreeze; // Whether the freezer is active
    public float range; // Scale multiplier for the freeze zone

    private List<Enemy> enemies = new List<Enemy>(); // List to track enemies in the freeze zone
    private Dictionary<Enemy, float> originalSpeeds = new Dictionary<Enemy, float>(); // Store original speeds of enemies

    void Update()
    {
        // If freezing is active, count down the timer
        if (startFreeze)
        {
            timeRunner += Time.deltaTime;

            if (timeRunner >= aliveTime)
            {
                DestroyFreezeZone();
            }
        }
    }

    public void SetScale()
    {
        // Set the size of the freeze zone based on the range
        Vector3 scale = this.transform.localScale;
        scale.x *= range;
        scale.y *= range;
        this.transform.localScale = scale;

        startFreeze = true; // Activate the freeze effect
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider has an Enemy component
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (!originalSpeeds.ContainsKey(enemy))
            {
                // Save the enemy's original speed
                originalSpeeds[enemy] = enemy.Speed;

                // Set the enemy's speed to 0
                enemy.Speed = 0;
            }

            // Add the enemy to the list of affected enemies
            enemies.Add(enemy);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider has an Enemy component
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            // Restore the enemy's original speed
            if (originalSpeeds.ContainsKey(enemy))
            {
                enemy.Speed = originalSpeeds[enemy];
                originalSpeeds.Remove(enemy);
            }

            // Remove the enemy from the list of affected enemies
            enemies.Remove(enemy);
        }
    }

    private void DestroyFreezeZone()
    {
        // Restore speed for all enemies still in the freeze zone
        foreach (var enemy in enemies)
        {
            if (originalSpeeds.ContainsKey(enemy))
            {
                enemy.Speed = originalSpeeds[enemy];
            }
        }

        // Clear the lists
        enemies.Clear();
        originalSpeeds.Clear();

        // Destroy the freeze zone object
        Destroy(gameObject);
    }
}