using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SprintToEnemy", menuName = "Skill/Mobility/SprintToEnemy", order = 1)]
public class SprintToEnemy : Mobility
{
    public float knockbackRadious = 5f;      // ���������ⴹ���ᷡ
    public float knockbackForce = 1;      // ���������͡��ᷡ
    public LayerMask obstacleLayer;        //�����������Ѻ��Ǩ�Ѻ��觡մ��ҧ

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        Player character = Player.Instance;
        if (character == null)
        {
            Debug.LogError("Player not found.");
            isActive = true;
            yield break;
        }

        // �Ѻ���˹�Mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = character.transform.position.z; // Align with the player's Z-axis in a 2D game

        //�ӹǳ���зҧ ��� ���˹��������
        Vector3 directionToMouse = (mousePosition - character.transform.position).normalized;
        Vector3 targetPosition = Vector3.zero;

        // ��Ǩ�ͺ��觡մ��ҧ
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

        // ��ҹknock back (����������)
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
        // ���µ���Ф���ѧ���˹���������µç
        character.transform.position = targetPosition;
    }

    private void KnockbackEnemy(Enemy enemy, Vector3 direction)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            // ��˹�������������Ѻ��á�����µç
            enemyRb.AddRelativeForce(direction.normalized * knockbackForce * 35, ForceMode2D.Impulse);
            Debug.Log("Enemy knocked back with force: " + knockbackForce);
        }
    }
}