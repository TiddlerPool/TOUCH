using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class SanUI : MonoBehaviour
{
    [SerializeField]private Volume postProcess;
    
    
    void Start()
    {
        postProcess = GetComponent<Volume>();
        postProcess.weight = 0;//san
    }

    // Update is called once per frame
    void Update()
    {
        postProcess.weight = Mathf.Lerp(1f, 0f, Character.san/100f);
    }

    public void San(float san)
    {
        postProcess.weight = san;
    }
}
