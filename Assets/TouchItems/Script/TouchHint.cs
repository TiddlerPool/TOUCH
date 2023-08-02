using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHint : MonoBehaviour
{
    private GameObject _mainCamera;


    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(_mainCamera.transform, Vector3.up);
    }
}
