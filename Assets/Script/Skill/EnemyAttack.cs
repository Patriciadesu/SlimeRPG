using System.Collections;
using UnityEngine;

public abstract class EnemyAttack : NormalAttack
{
    protected Enemy enemy;
    [SerializeField] protected float stayTime;

    public virtual IEnumerator OnUse(Enemy enemy)
    {
        this.enemy = enemy;

        yield return OnUse();
    }
}
