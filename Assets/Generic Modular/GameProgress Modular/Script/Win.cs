using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !DataController.isWin)
        {
            Invoke("Pass", 0.75f);
        }
    }

    private void Pass()
    {
        DataController.isWin = true;
    }
}
