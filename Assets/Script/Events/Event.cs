using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Event : ScriptableObject
{
    public string eventID;
    public string Name;
    public List<EventManager.DAYINWEEK> activatedDay = new List<EventManager.DAYINWEEK>();
    [Tooltip("Time that the event start.(24H)")]public int activatedHour;
    [Tooltip("How many hours the event will last.")]public float duration;
    public abstract IEnumerator StartEvent();
    public abstract void EndEvent();

}
