using System;
using UnityEngine;

public class Timer : IUpdateable
{
    public bool IsActive { get; set; } = true;

    public int Id { get; private set; }

    private float length;
    private bool isLooping;
    private float currentTime;
    private bool isStarted;
    private Delegate onExpired;

    public Timer(float _length, Delegate _onExpired, bool _startImmediately = true, bool _isLooping = false)
    {
        if (EventManager.InvokeCallback(new UpdateableCreatedEvent(this), out int id))
        {
            Id = id;
        }
        length = _length;
        onExpired = _onExpired;
        isStarted = _startImmediately;
        isLooping = _isLooping;
    }


    public void Update(float _delta)
    {
        if (isStarted)
        {
            RunTimer(_delta);
        }
    }
    public void FixedUpdate(float _fixedDelta) { }

    public void Start() => isStarted = true;
    public void Pause() => isStarted = false;
    public void Stop()
    {
        isStarted = false;
        currentTime = .0f;
    }
    
    private void RunTimer(float _delta)
    {
        if (currentTime < length)
        {
            currentTime += _delta;
        }
        else
        {
            if (!isLooping) isStarted = false;
            currentTime = .0f;
            onExpired.DynamicInvoke();
        }
    }

}
