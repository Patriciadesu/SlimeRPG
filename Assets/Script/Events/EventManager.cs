using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public List<Event> AllEvents = new List<Event>();
    public List<Event> TodayEvents = new List<Event>();

    public enum DayInWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Everyday
    }
    private void Awake()
    {
        CheckTodayEvents();
    }
    public void GetEvents()
    {
        //AllEvents = EventsFromDatabase;
    }
    public void CheckTodayEvents()
    {
        foreach (var _event in AllEvents)
        {
            if(_event.activatedDay.ToString().ToLower() == DateTime.Now.DayOfWeek.ToString().ToLower() || _event.activatedDay == DayInWeek.Everyday)
            {
                TodayEvents.Add(_event);
            }
        }
        StartCoroutine(CheckEventSpawnTime());
    }
    public IEnumerator CheckEventSpawnTime()
    {
        int[] waitTime = new int[TodayEvents.Count];
        for (int i = 0; i < TodayEvents.Count; i++)
        {
            int hour = TodayEvents[i].activatedHour - DateTime.Now.Hour;
            if (hour < 0) TodayEvents[i] = null;
            else waitTime[i] = hour;
        }
        if (waitTime.Length <= 0)
        {
            Debug.Log("No more events today");
            CheckTodayEvents();
            yield return null;
        }
        Event _event = TodayEvents[Array.IndexOf(waitTime, waitTime.Min())];
        yield return new WaitUntil(() => _event.activatedHour == DateTime.Now.Hour);
        AddEvent(_event);
    }
    public void AddEvent(Event _event)
    {
        StartCoroutine(_event.StartEvent());
        StartCoroutine(CheckEventSpawnTime());
    }
}
