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
            DataController.isWin = true;
        }
    }
}