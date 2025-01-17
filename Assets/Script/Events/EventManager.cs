using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public List<Event> AllEvents = new List<Event>();
    public List<Event> TodayEvents = new List<Event>();

    public enum DAYINWEEK
    {
        SUNDAY,
        MONDAY,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY,
        SATURDAY,
        EVERYDAY
    }
    private void Awake()
    {
        CheckTodayEvents();
        AllEvents.Clear();
        DatabaseManager.Instance.GetDataObejct<sBoostEvent[]>(API.getAllGrindingEvent, OnGetBoostEventData);
        DatabaseManager.Instance.GetDataObejct<sBossEvent[]>(API.getAllBossEvent, OnGetBossEventData);
    }
    public void OnGetBoostEventData(sBoostEvent[] boostEvents)
    {      
        List<sBoostEvent> sBoostEvents = boostEvents.ToList();
        foreach (sBoostEvent _event in sBoostEvents)
        {
            ProgressionBoostEvent boostEvent = new ProgressionBoostEvent(_event);
            AllEvents.Add(boostEvent);
        }
        Debug.Log($"Boost events loaded from database. Total event : {AllEvents.Count}");
    }
    public void OnGetBossEventData(sBossEvent[] bossEvents) 
    {
        List<sBossEvent> sBossEvents = bossEvents.ToList();
        foreach (sBossEvent _event in sBossEvents)
        {
            BossEvent bossEvent = new BossEvent(_event);
            AllEvents.Add(bossEvent);
        }
        Debug.Log($"All events loaded from database. Total event : {AllEvents.Count}");
    }
    public void CheckTodayEvents()
    {
        foreach (var _event in AllEvents)
        {
            foreach (var day in _event.activatedDay)
            {
                if (day.ToString().ToLower() == DateTime.Now.DayOfWeek.ToString().ToLower() || day == DAYINWEEK.EVERYDAY)
                {
                    TodayEvents.Add(_event);
                }
            }
        }
        if(TodayEvents.Count>0)StartCoroutine(CheckEventSpawnTime());
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
