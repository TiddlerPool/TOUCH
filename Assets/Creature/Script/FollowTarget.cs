/*
 * 敌人巡逻脚本
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform player;
    
    public GameObject RandomTarget;    // 要生成的物体
    //private Vector3 randomPosition;
    
    //private float generationInterval = 5f;   // 生成间隔时间
    private float t1,t2;
    void Start()
    { 
        t1 = 0;
       RandomTarget = Instantiate(RandomTarget, new Vector3(Random.Range(-23.6f, -6.8f),272.34f, Random.Range(-127.8f, -93.7f)), Quaternion.identity);
    }
    
    void Update()
    {
        t2 = Time.fixedTime;
        if (t2 - t1 >= 5)
        {
            if (RandomTarget != null) 
            {
                Destroy(RandomTarget);
            }
            Vector3 randomPosition = new Vector3(Random.Range(-23.6f, -6.8f),272.34f, Random.Range(-127.8f, -93.7f));
            RandomTarget = Instantiate(RandomTarget, randomPosition, Quaternion.identity);
            t1 = t2;
            
        }

        if (DetectPlayer.isPlayer)//目标改为玩家
        {
            nav.speed = 2;
            this.nav.SetDestination(this.player.position);
        }
        else
        {
            nav.speed = 1;
            this.nav.SetDestination(RandomTarget.transform.position);
        }
        
    }
    
}
