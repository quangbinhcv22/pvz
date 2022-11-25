// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace Game.Runtime
// {
//     [Serializable]
//     public class LinearTimeline : ILinearTimeline
//     {
//         private float _previousElapsed;
//         private float _elapsedSeconds;
//
//
//         public TimeSpan Elapsed => TimeSpan.FromMilliseconds(ElapsedMilliseconds);
//         public float ElapsedMilliseconds => _elapsedSeconds * TimeDefine.MillisecondsPerSecond;
//         public float ElapsedSeconds => _elapsedSeconds;
//
//
//         public Action OnStart { get; set; }
//         public Action OnStop { get; set; }
//         public Action OnRestart { get; set; }
//         public Action OnReset { get; set; }
//
//
//         private bool _isRunning;
//
//         public bool IsRunning
//         {
//             get => _isRunning;
//             private set
//             {
//                 if (_isRunning == value) return;
//                 _isRunning = value;
//
//                 if (_isRunning) MonoLifeCycle.OnUpdate += OnUpdate;
//                 else MonoLifeCycle.OnUpdate -= OnUpdate;
//             }
//         }
//
//
//         public void Start()
//         {
//             IsRunning = true;
//             OnStart?.Invoke();
//         }
//
//         public void Stop()
//         {
//             IsRunning = false;
//             OnStop?.Invoke();
//         }
//
//         public void Restart()
//         {
//             Reset();
//             Start();
//
//             OnRestart?.Invoke();
//         }
//
//         public void Reset()
//         {
//             _elapsedSeconds = default;
//             OnReset?.Invoke();
//         }
//
//
//         private readonly List<TimelineEvent> _events = new();
//
//         public bool AddEvent(TimelineEvent @event)
//         {
//             if (_events.Contains(@event)) return false;
//
//             _events.Add(@event);
//             return true;
//         }
//
//         public bool RemoveEvent(TimelineEvent @event)
//         {
//             return _events.Remove(@event);
//         }
//
//
//         private void OnUpdate()
//         {
//             if (!IsRunning) return;
//
//             _previousElapsed = _elapsedSeconds;
//             _elapsedSeconds += Time.deltaTime;
//
//
//             try
//             {
//                 foreach (var @event in _events)
//                 {
//                     var atSeconds = @event.seconds;
//
//                     var isComplete = atSeconds > _previousElapsed && atSeconds <= _elapsedSeconds;
//                     if (isComplete) @event.callback?.Invoke();
//                 }
//             }
//             catch (InvalidOperationException)
//             {
//                 Debug.LogWarning("Invalid Operation Exception: Events was destroy when running");
//             }
//         }
//
//
//         public void Dispose()
//         {
//             IsRunning = false;
//             _events.Clear();
//         }
//     }
//
//     [Serializable]
//     public class TimelineEvent
//     {
//         public float seconds;
//         public Action callback;
//
//         public TimelineEvent()
//         {
//         }
//
//         public TimelineEvent(float seconds, Action callback)
//         {
//             this.seconds = seconds;
//             this.callback = callback;
//         }
//     }
//
//     public interface IStopwatch
//     {
//         TimeSpan Elapsed { get; }
//         float ElapsedSeconds { get; }
//         float ElapsedMilliseconds { get; }
//         bool IsRunning { get; }
//
//         void Start();
//         void Stop();
//         void Restart();
//         void Reset();
//     }
//
//     public interface IMultiEventTimeline
//     {
//         bool AddEvent(TimelineEvent @event);
//         bool RemoveEvent(TimelineEvent @event);
//     }
//
//     public interface ILinearTimeline : IStopwatch, IStopwatchCallback, IMultiEventTimeline, IDisposable
//     {
//     }
//
//     public interface IStopwatchCallback
//     {
//         public Action OnStart { get; set; }
//         public Action OnStop { get; set; }
//         public Action OnRestart { get; set; }
//         public Action OnReset { get; set; }
//     }
//
//     public static class TimeDefine
//     {
//         public const int MillisecondsPerSecond = 1000;
//     }
// }