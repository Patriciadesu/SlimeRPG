using System.Collections;
using UnityEngine;
[System.Serializable]
public abstract class Event : MonoBehaviour
{
    public string Name;
    public string activatedDay;
    public int activatedHour;
    public int activatedMinute;
    public float duration;

    public abstract IEnumerator StartEvent();
    public abstract void EndEvent();

}
