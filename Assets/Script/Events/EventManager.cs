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
        /*
        List<sBoostEvent> sBoostEvents = getBoostEventsFromDatabase();
        List<sBossEvent> sBossEvents = getBossEventsFromDatabase();

        AllEvents.Clear();
        foreach (sBoostEvent _event in sBoostEvents)
        {
            ProgressionBoostEvent boostEvent = new ProgressionBoostEvent(_event);
            AllEvents.Add(boostEvent);
        }
        foreach (sBossEvent _event in sBossEvents)
        {
            BossEvent bossEvent = new BossEvent(_event);
            AllEvents.Add(bossEvent);
        }
        Debug.Log($"All event loaded from database. Total event : {AllEvents.Count}");
        */
    }
    public void CheckTodayEvents()
    {
        foreach (var _event in AllEvents)
        {
            foreach (var day in _event.activatedDay)
            {
                if (day.ToString().ToLower() == DateTime.Now.DayOfWeek.ToString().ToLower() || day == DayInWeek.Everyday)
                {
                    TodayEvents.Add(_event);
                }
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
            if (hour < -TodayEvents[i].duration) TodayEvents[i] = null;
            else waitTime[i] = hour;
        }
        if (waitTime.Length <= 0)
        {
            Debug.Log("No more events today");
            CheckTodayEvents();
            yield return null;
        }
        Event _event = TodayEvents[Array.IndexOf(waitTime, waitTime.Min())];
        if (waitTime.Min() <= 0)
        {
            Debug.Log("An event is currently happening");
        }
        else
        {
            Debug.Log($"Next event will start in approximately {waitTime.Min()} hours");
            yield return new WaitUntil(() => _event.activatedHour == DateTime.Now.Hour);
        }
        AddEvent(_event);
    }
    public void AddEvent(Event _event)
    {
        StartCoroutine(_event.StartEvent());
        Debug.Log($"{_event.Name} Started");
        StartCoroutine(CheckEventSpawnTime());
    }
}
