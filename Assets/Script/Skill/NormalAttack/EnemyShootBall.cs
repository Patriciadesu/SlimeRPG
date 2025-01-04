using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShootBall", menuName = "Skill/EnemyAttack/EnemyShootBall", order = 1)]
public class EnemyShootBall : EnemyAttack
{
    [SerializeField] private float ballSpeed = 1;

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
        CreateBall(enemy.transform.position, direction);
        ////////////////////////////////////

        yield return new WaitForSeconds(coolDown);

        isActive = true;
    }

    protected GameObject CreateBall(Vector3 pos, Vector2 vector)
    {
        var ball = Instantiate(Resources.Load<Transform>("Effect/Ball"));

        ball.position = pos;

        var ballObj = ball.AddComponent<BallObject>();

        ballObj.Setup(enemy.AttackDamage, vector * ballSpeed * 5);

        return ball.gameObject;
    }

    private class BallObject : MonoBehaviour
    {
        private float damage;

        public void Setup(float damage, Vector2 vector)
        {
            this.damage = damage;

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = vector;

            Destroy(gameObject, 10);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
