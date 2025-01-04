using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMoveStep", menuName = "Skill/EnemyAttack/EnemyMoveStep", order = 1)]
public class EnemyMoveStep : EnemyAttack
{
    [SerializeField] private float teleportDistance = 3;

    public override IEnumerator OnUse()
    {
        if (!isActive) yield break;

        isActive = false;

        if (enemy == null || Player.Instance == null)
        {
            isActive = true;
            yield break;
        }

        ////////////// ATTACK //////////////
        var plrPos = Player.Instance.transform.position;
        var randomCircle = Random.insideUnitCircle.normalized * teleportDistance;

        enemy.transform.position = plrPos + randomCircle.ConvertTo<Vector3>();
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
