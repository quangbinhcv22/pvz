using System;
using UnityEngine;

[Serializable]
public class OneEventTimeline
{
    private float _previousElapsed;
    private float _elapsedSeconds;


    public TimeSpan Elapsed => TimeSpan.FromMilliseconds(ElapsedMilliseconds);
    public float ElapsedMilliseconds => _elapsedSeconds * TimeDefine.MillisecondsPerSecond;
    public float ElapsedSeconds => _elapsedSeconds;


    public Action OnStart { get; set; }
    public Action OnStop { get; set; }
    public Action OnRestart { get; set; }
    public Action OnReset { get; set; }


    private bool _isRunning;

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            if (_isRunning == value) return;
            _isRunning = value;

            if (_isRunning) MonoLifeCycle.OnUpdate += OnUpdate;
            else MonoLifeCycle.OnUpdate -= OnUpdate;
        }
    }


    public void Start()
    {
        IsRunning = true;
        OnStart?.Invoke();
    }

    public void Stop()
    {
        IsRunning = false;
        OnStop?.Invoke();
    }

    public void Restart()
    {
        Reset();
        Start();

        OnRestart?.Invoke();
    }

    public void Reset()
    {
        _elapsedSeconds = default;
        OnReset?.Invoke();
    }


    private TimelineEvent @event = new();

    public OneEventTimeline SetEvent(TimelineEvent @event)
    {
        this.@event = @event;
        return this;
    }
    
    public OneEventTimeline SetSeconds(float seconds)
    {
        @event.seconds = seconds;
        return this;
    }
    
        
    public OneEventTimeline SetCallback(Action callback)
    {
        @event.callback = callback;
        return this;
    }
    
    


    private void OnUpdate()
    {
        if (!IsRunning) return;

        _previousElapsed = _elapsedSeconds;
        _elapsedSeconds += Time.deltaTime;


        try
        {
            var atSeconds = @event.seconds;

            var isComplete = atSeconds > _previousElapsed && atSeconds <= _elapsedSeconds;
            if (isComplete) @event.callback?.Invoke();
        }
        catch (InvalidOperationException)
        {
            Debug.LogWarning("Invalid Operation Exception: Events was destroy when running");
        }
    }


    public void Dispose()
    {
        IsRunning = false;
        @event = null;
    }
}

[Serializable]
public class TimelineEvent
{
    public float seconds;
    public Action callback;
}

public static class TimeDefine
{
    public const int MillisecondsPerSecond = 1000;
}