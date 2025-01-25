using System;
using UnityEngine;

public static class EventManager
{
    public static event Action PhaseCompleted;

    public static void TriggerPhaseCompleted()
    {
        PhaseCompleted?.Invoke();
    }
}