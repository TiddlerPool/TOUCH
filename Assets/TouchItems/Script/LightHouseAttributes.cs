using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseAttributes : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private bool set;

    private void Start()
    {
        player = GameObject.Find("PlayerCapsule").transform;
    }

    private void Update()
    {
        if (set) {
            StartCoroutine("RecoverHealth");
        }
    }

    public bool Set {
        get
        {
            return set;
        }
        set
        {
            set = value;

            StopCoroutine("RecoverHealth");
            StartCoroutine("RecoverHealth");
        }
            
    }


    IEnumerator RecoverHealth() {
        Debug.Log("StartCoroutine");
            player.GetComponent<DataController>().ApplyDamage(-1f);
            yield return new WaitForSeconds(1f);
        

        set = false;
        yield return null;
    }
}
