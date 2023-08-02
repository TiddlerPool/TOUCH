using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEvents : MonoBehaviour
{
    public static HurtEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action<int> onHurtBegin;
    public void HurtBegin(int id)
    {
        if (onHurtBegin != null)
        {
            onHurtBegin(id);
        }
    }

    public event Action<int> onHurtEnd;
    public void HurtEnd(int id)
    {
        if (onHurtEnd != null)
        {
            onHurtEnd(id);
        }
    }
}