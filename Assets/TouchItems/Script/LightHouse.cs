using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : TouchItems
{
    //private bool startHeal;
    private ParticleSystem RecoverPartical;
    [SerializeField]private bool startHealed;


    private void LateUpdate()
    {
        TouchToRecover();

        
    }

    public void TouchToRecover() {
        if (playerTrans != null)
        {
            RecoverPartical = playerTrans.Find("Healing").GetComponent<ParticleSystem>();
        }
        else
        {
            //Debug.Log("Can not find healing partical!");
        }

        if (touched && !startHealed) {
            StopCoroutine("RecoverHealth");
            StartCoroutine("RecoverHealth");
            StopCoroutine("RecoverFuel");
            StartCoroutine("RecoverFuel");
            startHealed = true;
            
        }
        else if (!touched && startHealed)
        {
            StopCoroutine("RecoverHealth");
            StopCoroutine("RecoverFuel");
            Debug.Log("RecoverStopped!");
            startHealed = false;
            RecoverPartical.Stop();
        }
    }


    IEnumerator RecoverHealth()
    {
        Debug.Log("StartHealing");
        while (Character.health < 10f)
        {
            if (RecoverPartical.isStopped)
                RecoverPartical.Play();
            playerTrans.GetComponent<DataController>().ApplyHeal(1f);
            yield return new WaitForSeconds(1f);
        }
        if(Character.fuel >= 100f)
            RecoverPartical.Stop();
        Debug.Log("Health Full Recovered!");
        yield return null;
    }

    IEnumerator RecoverFuel()
    {
        Debug.Log("StartRefuelling");
        while (Character.fuel < 100f)
        {
            if(RecoverPartical.isStopped)
                RecoverPartical.Play();
            playerTrans.GetComponent<DataController>().ApplyFuel(-0.1f);
            yield return null;
        }
        if (Character.health >= 10f)
            RecoverPartical.Stop();
        Debug.Log("Fuel Full Recovered!");
        yield return null;
    }
}
