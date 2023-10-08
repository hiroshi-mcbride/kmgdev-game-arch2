using System;
using UnityEngine;

public class Timer : IUpdateable, IDestroyable
{
    public bool IsActive { get; set; } = true;


    private float length;
    private bool isLooping;
    private float currentTime;
    private bool isStarted;
    private Delegate onExpired;

    public Timer(float _length, Delegate _onExpired, bool _startImmediately = true, bool _isLooping = false)
    {
        length = _length;
        onExpired = _onExpired;
        isStarted = _startImmediately;
        isLooping = _isLooping;
        
        EventManager.Invoke(new UpdateableCreatedEvent(this));
    }

    public void Update()
    {
        if (isStarted)
        {
            RunTimer();
        }
    }
    
    public void FixedUpdate() { }
    
    public void Destroy()
    {
        EventManager.Invoke(new UpdateableDestroyedEvent(this));
    }

    public void Start() => isStarted = true;
    public void Pause() => isStarted = false;
    public float Stop()
    {
        float timeLeft = currentTime;
        isStarted = false;
        currentTime = .0f;
        return timeLeft;
    }
    
    private void RunTimer()
    {
        if (currentTime < length)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            if (!isLooping) isStarted = false;
            currentTime = .0f;
            onExpired.DynamicInvoke();
        }
    }

}
