using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public abstract class Event : MonoBehaviour
{
    public string eventID;
    public string Name;
    public List<EventManager.DayInWeek> activatedDay = new List<EventManager.DayInWeek>();
    public int activatedHour;
    public float duration;
    public abstract IEnumerator StartEvent();
    public abstract void EndEvent();

}
