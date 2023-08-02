using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEvents : MonoBehaviour
{
    public static ShieldEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action onReflectorActivate;
    public void ReflectorActivate() {
        if(onReflectorActivate != null) {
            onReflectorActivate();
        }
    }

    public event Action onReflectorDisable;
    public void ReflectorDisable()
    {
        if (onReflectorDisable != null)
        {
            onReflectorDisable();
        }
    }
}
