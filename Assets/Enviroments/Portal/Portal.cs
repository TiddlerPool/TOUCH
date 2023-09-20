using System;
using System.Collections;
using System.Collections.Generic;
using MyAudio;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform target_door;
    public GameObject player;
    private bool flag;
    void Start()
    {
        flag = true;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print(flag);
            if (flag)
            {
                print("传送");
                AudioManager.PlayAudio("portal");
                CharacterController controller = player.GetComponent<CharacterController>();
                controller.enabled = false;  // 在设置位置之前禁用CharacterController，以防止可能的碰撞问题
                other.transform.position = target_door.position;  // 设置玩家位置
                controller.enabled = true;   // 重新启用CharacterController
                flag = false;
            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        flag = true;
    }
}
