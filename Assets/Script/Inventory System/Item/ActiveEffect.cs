using UnityEngine;
using System.Collections.Generic;

public class ActiveEffect
{
    public UsableItem item;
    public float remainingTime;
    public bool isActive;

    public ActiveEffect(UsableItem item)
    {
        this.item = item;
        this.remainingTime = item.duration;
        this.isActive = true;
    }
}