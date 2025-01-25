using System;
using UnityEngine;

public static class EventManager
{
    public static event Action TimerCompleted;

    public static void TriggerTimerCompleted()
    {
        TimerCompleted?.Invoke();
    }
}