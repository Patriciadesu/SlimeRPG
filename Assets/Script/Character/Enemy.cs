using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy closestEnemy
    {
        get
        {
            var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

            float distance = Mathf.Infinity;
            Enemy closestEnemy = null;

            foreach (var enemy in enemies)
            {
                var enemyPos = enemy.transform.position;
                enemyPos.z = 0;


            }

            return closestEnemy;
        }
    }
}
