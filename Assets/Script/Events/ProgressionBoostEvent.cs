using System;
using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "ProgressionBoostEvent")]
[Serializable]
public class ProgressionBoostEvent : Event
{
    [Space(10f)]
    public float expMultiplier;
    public float coinMultiplier;
    public float itemDropRateMultiplier;

    public ProgressionBoostEvent(sBoostEvent data)
    {
        this.eventID = data._id;
        this.Name = data.name;
        this.activatedHour = data.activatedHour;
        this.duration = data.duration;

        foreach (EventManager.DAYINWEEK day in Enum.GetValues(typeof(EventManager.DAYINWEEK)))
        {
            foreach (string d in data.activatedDays)
            {
                if (d.ToLower() == day.ToString().ToLower())
                {
                    activatedDay.Add(day);
                }
            }
        }
        expMultiplier = data.expMultiplier;
        coinMultiplier = data.coinMultiplier;
        itemDropRateMultiplier = data.itemDropRateMultiplier;
    }

    public override IEnumerator StartEvent()
    {
        RewardManager.Instance.expBoostRate = expMultiplier;
        RewardManager.Instance.coinBoostRate = coinMultiplier;
        RewardManager.Instance.dropBoostRate = itemDropRateMultiplier;
        yield return new WaitUntil(() => activatedHour + duration == DateTime.Now.Hour);
        EndEvent();
    }
    public override void EndEvent()
    {
        RewardManager.Instance.expBoostRate = 1f;
        RewardManager.Instance.coinBoostRate = 1f;
        RewardManager.Instance.dropBoostRate = 1f;
    }

    

    
}
