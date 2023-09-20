using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBossHealthEvent : MonoBehaviour
{
    public static UIBossHealthEvent current;
    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
    }

    public event Action<Enemy> onEncounterBoss;

    public void EncounterBoss<T>(T data) where T: Enemy
    {
        if(onEncounterBoss != null)
        {
            onEncounterBoss(data);
        }
    }

    public event Action onDiscounterBoss;

    public void DiscounterBoss()
    {
        if (onDiscounterBoss != null)
        {
            onDiscounterBoss();
        }
    }
}
