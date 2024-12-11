using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public static event OnEnemyDeath BattleReport;

    public delegate void OnEnemyDeath(Enemy enemy);

    public static void onEnemyKilled(Enemy enemy)
    {
        GameManager.BattleReport?.Invoke(enemy);
    }
}
