using System;
using System.Collections;
using UnityEngine;

public class ProgressionBoostEvent : Event
{
    public float expMultiplier;
    public float coinMultiplier;
    public float itemDropRateMultiplier;
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
