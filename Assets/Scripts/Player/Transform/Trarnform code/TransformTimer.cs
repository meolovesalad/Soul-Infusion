using UnityEngine;
using System;
using System.Collections;

public class TransformTimer : MonoBehaviour
{
    public event Action OnTimerEnd;

    private Coroutine _timerRoutine;

    private float _remainingTime;

    public void StartTimer(float duration)
    {
        _remainingTime = duration;

        _timerRoutine = StartCoroutine(TimerRoutine());
    }

    public void AddTime(float amount)
    {
        _remainingTime += amount;
    }

    public void StopTimer()
    {
        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
        }
    }

    private IEnumerator TimerRoutine()
    {
        while (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;

            yield return null;
        }

        OnTimerEnd?.Invoke();
    }
}
