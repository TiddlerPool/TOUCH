using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isEncounter = false;
    public bool IsEncounter
    {
        get { return isEncounter; }
        set { isEncounter = value; }
    }

    void Start()
    {
        audioSource.volume = 0;
    }

    void Update()
    {
        if (isEncounter)
        {
            if(audioSource.volume<0.8) 
                audioSource.volume += Time.deltaTime;   
        }
        else
        {
            if(audioSource.volume>0)
                audioSource.volume -= Time.deltaTime;
        }
    }
}
