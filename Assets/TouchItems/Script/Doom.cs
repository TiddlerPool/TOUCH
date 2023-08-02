using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom : TouchItems
{
    public GameObject doomSign;

    public override void RechargeTimer()
    {
        if (needRecharge)
        {
            timerRCG -= Time.deltaTime;
            if (timerRCG <= 0.0f)
            {
                needRecharge = false;
                anim.SetTrigger("fresh");
                ResetTimer();
            }
        }
    }

    public override void Growing()
    {
        base.Growing();
        if(!needRecharge)
        {
            anim.SetBool("RunningOut", false);
        }
        else
        {
            anim.SetBool("RunningOut", true);
        }

        if(playerTrans != null)
        ActivateLazer();
    }
    public void ShowDoomSign()
    {
        doomSign.SetActive(true);
    }

    public void DestroyDoomSign() {
        doomSign.SetActive(false);
    }

    public void ActivateLazer()
    {
        if (reachable && touched && !needRecharge)
        {
            playerTrans.GetComponentInChildren<LazerEmitter>().Emitting = true;
        }
        else
        {
            playerTrans.GetComponentInChildren<LazerEmitter>().Emitting = false;
        }


    }
}
