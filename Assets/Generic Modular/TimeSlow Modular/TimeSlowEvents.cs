using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowEvents : MonoBehaviour
{
    public static TimeSlowEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action onSlowBegin;
    public void SlowBegin()
    {
        if (onSlowBegin != null)
        {
            onSlowBegin();
        }
    }

    public event Action onSlowEnd;
    public void SlowEnd()
    {
        if (onSlowEnd != null)
        {
            onSlowEnd();
        }
    }
}
