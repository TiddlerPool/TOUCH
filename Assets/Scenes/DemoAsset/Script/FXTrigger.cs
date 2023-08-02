using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXTrigger : MonoBehaviour
{
    public GameObject bomb;
    //public GameObject particle2;
    void Start()
    {
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Bomb()
    {
        bomb.SetActive(true);
    }
    
    
}
