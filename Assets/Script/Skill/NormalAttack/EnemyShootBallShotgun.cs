using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShootBallShotgun", menuName = "Skill/EnemyAttack/EnemyShootBallShotgun", order = 1)]
public class EnemyShootBallShotgun : EnemyShootBall
{
    [SerializeField] private int bulletCount = 5;
    [SerializeField] private float bulletRadius = 30;

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
        var direction = (Player.Instance.transform.position - enemy.transform.position).normalized;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = ((i / bulletCount) * bulletRadius) - (bulletRadius / 2f);
            CreateBall(enemy.transform.position, Quaternion.Euler(0, 0, angle) * direction);
        }
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }
}
