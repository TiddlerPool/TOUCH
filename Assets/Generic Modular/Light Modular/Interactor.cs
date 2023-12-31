using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode] 
public class Interactor : MonoBehaviour
{   
    [SerializeField]
    float radius;
    
    // Update is called once per frame
    void Update()
    {
        
        radius = transform.localScale.y;
        Shader.SetGlobalVector("_Position", transform.position);
        Shader.SetGlobalFloat("_Radius", radius);
    }
}