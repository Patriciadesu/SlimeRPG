using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : Event
{
    public string bossID;
    public BossNPC bossAsNPC;
    public Spawner bossSpawner;
    private List<string> originalEnemyIDs;
    private int originalMaxNearbyEnemy;
    public BossEvent(sBossEvent data)
    {
        this.eventID = data._id;
        this.Name = data.name;
        this.activatedHour = data.activatedHour;
        this.duration = data.duration;

        foreach (EventManager.DayInWeek day in Enum.GetValues(typeof(EventManager.DayInWeek)))
        {
            foreach (string d in data.activatedDays)
            {
                if (d.ToLower() == day.ToString().ToLower())
                {
                    activatedDay.Add(day);
                }
            }
        }

    }
    public override IEnumerator StartEvent()
    {
        /*
        originalEnemyIDs = bossSpawner.enemyIDs;
        originalMaxNearbyEnemy = bossSpawner.maxNearbyEnemy;

        bossSpawner.enemyIDs.Clear();
        bossSpawner.enemyIDs.Add(bossID);
        bossSpawner.maxNearbyEnemy = 1;
        bossAsNPC.BossEventOngoing = true;
        */

        yield return new WaitUntil(() => activatedHour + duration == DateTime.Now.Hour);
        EndEvent();
    }
    public override void EndEvent()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null) Destroy(boss);

        bossSpawner.enemyIDs = originalEnemyIDs;
        bossSpawner.maxNearbyEnemy = originalMaxNearbyEnemy;
        bossAsNPC.BossEventOngoing = false;
    }

}
