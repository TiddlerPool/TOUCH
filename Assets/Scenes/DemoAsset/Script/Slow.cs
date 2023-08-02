using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slow : MonoBehaviour
{
    // Start is called before the first frame update
    //public AudioSource[] audios;
    public GameObject CloseUpCamera;
    private float slowTime;
    public float SlowTime
    {
        get
        {
            return slowTime;
        }
        set
        {
            slowTime = value;
        }

    }
    
    void Start()
    {
        Time.timeScale = 1.0f;
        TimeSlowEvents.current.onSlowBegin += TimeSlow;
        TimeSlowEvents.current.onSlowEnd += Restore;
        CloseUpCamera = GameObject.Find("PlayerCloseUpCamera");
        CloseUpCamera.SetActive(false);
    }

    private void Update()
    {

    }

    public void TimeSlow()
    {
        if (Time.timeScale != 0.1f)
        {
            StopCoroutine("SlowEnd");
            StopCoroutine("SlowStart");
            StartCoroutine("SlowStart");
            //print("减速");
            CloseUpCamera.SetActive(true);
            //for (int i = 0; i < audios.Length; i++) audios[0].pitch = 0.5f;
        }
    }

    IEnumerator SlowStart()
    {
        while(Time.timeScale > 0.1f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.1f, Time.fixedDeltaTime * 100f);
            yield return null;
        }

    }


    void Restore()
    {
        StopCoroutine("SlowEnd");
        StopCoroutine("SlowStart");
        StartCoroutine("SlowEnd");
        //print("恢复");
        //for (int i = 0; i < audios.Length; i++) audios[0].pitch = 1.0f;
        CloseUpCamera.SetActive(false);
    }

    IEnumerator SlowEnd()
    {
        while (Time.timeScale < 1f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.fixedDeltaTime * 100f);
            yield return null;
        }

    }
}
