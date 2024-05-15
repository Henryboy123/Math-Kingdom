using System;
using UnityEngine.Events;

public class EventsManager : MonoBehaviourSingleton<EventsManager>
{
    public UnityEvent OnStartDrag = new UnityEvent();
    public UnityEvent OnEndDrag = new UnityEvent();
    public UnityEvent OnGameStart = new UnityEvent();
    public UnityEvent OnTransparentBoxHit = new UnityEvent();
    public UnityEvent OnWin = new UnityEvent();
}

