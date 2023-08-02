using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        ShieldEvents.current.onReflectorActivate += ShieldUp;
        ShieldEvents.current.onReflectorDisable += ShieldOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShieldUp() {
        shield.SetActive(true);
    }

    public void ShieldOff()
    {
        shield.SetActive(false);
        TimeSlowEvents.current.SlowEnd();
    }
}
