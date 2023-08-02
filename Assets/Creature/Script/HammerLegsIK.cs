using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerLegsIK : MonoBehaviour
{
    public Transform TestTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.T))
        {
            TestTarget.position = TestTarget.position + Vector3.forward;
        }
    }
}
