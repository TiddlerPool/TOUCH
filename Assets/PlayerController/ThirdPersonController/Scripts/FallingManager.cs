using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingManager : MonoBehaviour
{
    [SerializeField] private bool isfall;
    [SerializeField] private bool recorded;
    [SerializeField] private bool onAir;
    [SerializeField] private Transform backPoint;
    public Vector3 respawnPos;
    private CharacterController player;

    public Collider fallingPlane;

    private void Awake()
    {
        var cc = GameObject.Find("PlayerCapsule");
        player = cc.GetComponent<CharacterController>();
    }

    private void Start()
    {
        Invoke("AdjustBool", 1f);
    }

    private void Update()
    {
        onAir = !player.isGrounded;
        
    }

    private void LateUpdate()
    {
        RespawnPointSet();
        FallingCheck();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == fallingPlane)
        { 
            GetComponent<DataController>().ApplyDamage(1);
            isfall = true;
        }

        else
        {
            isfall = false;
        }
    }

    private void RespawnPointSet() {
        if (onAir && !recorded)
        {
            respawnPos = backPoint.position;
            recorded = true;
        }
        else if (!onAir && recorded)
        {
            recorded = false;

        }
       
    }

    private void FallingCheck() {
        if (isfall) {
            Vector3 newPoint = new Vector3(respawnPos.x, respawnPos.y + 2f, respawnPos.z);
            transform.position = newPoint;
            Invoke("AdjustBool", 0.2f);
            
        }
    }

    private void AdjustBool() {
        onAir = false;
        recorded = false;
    }
}
