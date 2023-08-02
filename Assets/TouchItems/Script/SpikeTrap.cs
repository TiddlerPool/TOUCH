using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Hurt>(out Hurt hurt))
        {
            hurt.ApplyHurtToRoot(1, 0);
        }
    }
}
