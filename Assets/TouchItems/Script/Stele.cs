using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stele : TouchItems, IDamageable
{
    public float HurtRate;
    private float nextHurt;

    public float CurrentLifeSpan
    {
        get { return timerLSP; }
        set { timerLSP = value; }
    }

    public float LifeSpan
    {
        get { return lifespanTime; }
        set { lifespanTime = value; }
    }

    public void GetHurt(float damage, int id, int origin)
    {
        if (timerLSP > 0f)
        {
            if(Time.time> nextHurt)
            {
                if (GetComponent<EntityFactions>().FactionID != origin)
                {
                    timerLSP -= damage;
                    HurtEvents.current.HurtBegin(id);
                    nextHurt = Time.time + HurtRate;
                }
            }
            
        }
    }
}
