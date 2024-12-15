using System;
using System.Collections;
using UnityEngine;
[System.Serializable]
public abstract class Event : MonoBehaviour
{
    public string Name;
    public EventManager.DayInWeek activatedDay;
    public int activatedHour;
    public float duration;

    public abstract IEnumerator StartEvent();
    public abstract void EndEvent();

}
