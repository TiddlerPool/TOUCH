using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : TouchItems
{
    
    private void LateUpdate()
    {
        if(playerTrans != null)
        ActivateShield();
    }

    public void ActivateShield() {
        if (reachable && touched && !needRecharge) {
            ShieldEvents.current.ReflectorActivate();
        }
        else {
            ShieldEvents.current.ReflectorDisable();
        }

      
    }
}
