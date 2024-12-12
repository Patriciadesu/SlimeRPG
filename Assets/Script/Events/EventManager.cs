using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public Event currentEvent;
    public List<Event> AllEvents = new List<Event>();

    public void GetEvents()
    {
        //AllEvents = EventsFromDatabase;
    }

}
