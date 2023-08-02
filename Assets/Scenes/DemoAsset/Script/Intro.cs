using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject ui1;
    public GameObject ui2;
    //private bool isNext;
    private int num;
    void Start()
    {
        //isNext = false;
        num = 1;
        //ui1.SetActive(true);
        ui2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            num++;
        }

        if (num == 1)
        {
            //isNext = true;
            ui1.SetActive(false);
            ui2.SetActive(true);
        }
        if (num == 2)
        { 
            SceneManager.LoadScene("Main");
        }
        
    }
        
    
}
