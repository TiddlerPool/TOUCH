using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Return : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReturnStart()
    {
        //audioSource.clip = audioClip1;
        audioSource.Play();
        SceneManager.LoadScene("Start");
    }
}
