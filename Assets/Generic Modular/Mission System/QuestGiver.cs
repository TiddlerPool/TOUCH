using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public int questID;
    public bool onetimeTrigger;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        if(onetimeTrigger)      
            gameObject.SetActive(false);

        QuestEvents.current.QuestAccept(questID);
    }
}
