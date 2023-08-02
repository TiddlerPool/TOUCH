using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Transform body;                  //身体
    public LayerMask terrainLayer;          //检测图层
    Vector3 newposition, oldposition, currentposition; //位置

    public float footSpacingForward,footSpacingRight,footSpacingUp; //落点偏移
    public float stepstance;                //步长
    public float high = 0.1f;               //高度
    public float speed = 2;                 //速度
    float lerp = 1;

    public target leg1;
    //leg2;                //约束

    private void Start()
    {
        newposition = this.transform.position;
        currentposition = this.transform.position;
    }

    void Update()
    {
        this.transform.position = currentposition;
        
        Ray ray = new Ray(body.position + (body.forward * footSpacingForward)+(body.right * footSpacingRight)+(body.up * footSpacingUp), -body.up);
        Debug.DrawRay(body.position + (body.forward * footSpacingForward)+(body.right*footSpacingRight)+(body.up * footSpacingUp), -body.up * 10, Color.red);
        
        
        if (Physics.Raycast(ray,out RaycastHit info, 10, terrainLayer.value))
        {
            //print(Vector3.Distance(newposition, info.point));

           //if (Vector3.Distance(newposition, info.point) > stepstance && leg1.lerp >= 1 && leg2.lerp >= 1)
           if (Vector3.Distance(newposition, info.point) > stepstance && leg1.lerp >= 1)
           {
                lerp = 0;
                newposition = info.point;
           }
        }

        if (lerp < 1)
        {
            Vector3 footposition = Vector3.Lerp(oldposition, newposition, lerp);
            footposition.y += Mathf.Sin(lerp * Mathf.PI) * high;
            currentposition = footposition;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldposition = newposition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(newposition, 0.2f);
    }
}
