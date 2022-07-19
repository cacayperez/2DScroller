using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer
{
    public float RemainingSeconds { get; private set; }
    public event System.Action onTimerEnd;
    private bool _isEnabled;
    public Timer(float duration = 0f)
    {
        RemainingSeconds = duration;
    }

    public void Tick(float deltaTime)
    {
        if (RemainingSeconds == 0f) return;
        RemainingSeconds -= deltaTime;
        CheckForTimerEnd();
    }

    private void CheckForTimerEnd()
    {
        if (RemainingSeconds > 0f) return;
        RemainingSeconds = 0f;
        onTimerEnd?.Invoke();
    }

    public void SetTime(float timeInSeconds)
    {
        RemainingSeconds = timeInSeconds;
    }

}