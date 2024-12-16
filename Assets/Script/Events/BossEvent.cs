using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BossEvent")]
[Serializable]
public class BossEvent : Event
{
    //public BossNPC bossAsNPC;
    private Spawner bossSpawner;
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
        Spawner[] allSpawner = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
        foreach (Spawner spawner in allSpawner)
        {
            if (spawner.gameObject.CompareTag("Boss"))
            {
                bossSpawner = spawner;
                Debug.Log("Spawner Found");
            }
        }
        bossSpawner.ForceSpawn();
        //bossAsNPC.BossEventOngoing = true;

        yield return new WaitUntil(() => activatedHour + duration == DateTime.Now.Hour);
        EndEvent();
    }
    public override void EndEvent()
    {
        bossSpawner.ForceDie();
        //bossAsNPC.BossEventOngoing = false;
    }

}
