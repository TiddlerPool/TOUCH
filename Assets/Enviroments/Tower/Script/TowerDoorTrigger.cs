using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoorTrigger : MonoBehaviour
{
    public Animator anim;
    public bool IsPassed;
    private void Awake()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if(IsPassed)
        {
            anim.SetBool("DoorOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("DoorOpen", false);
    }


}
 