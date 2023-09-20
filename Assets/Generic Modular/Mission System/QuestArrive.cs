using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArrive : MonoBehaviour
{
    public int questID;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        QuestEvents.current.QuestUpdate(questID,0);
        gameObject.SetActive(false);
    }
}
