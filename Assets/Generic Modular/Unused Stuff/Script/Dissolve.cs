using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material material;
    public float dissovleup;
    void Start()
    {
        dissovleup = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_ClipThread",dissovleup);
       
    }
    
}
