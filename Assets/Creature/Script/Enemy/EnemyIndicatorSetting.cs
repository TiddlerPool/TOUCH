using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
[ExecuteInEditMode]
public class EnemyIndicatorSetting : MonoBehaviour
{
    public GameObject VisionIndi;
    public GameObject SenseIndi;
    //public GameObject PathPoint;
    public Material IndicatorMat;
    //public LineRenderer line;
    public Enemy enemy;
    //public bool refresh;

    //[SerializeField]private Transform[] patrolPoints;  

    
    private void Awake()
    {
        
    }

    private void Start()
    {
        //refresh = true;
    }

    private void Update()
    {
        Vector3 visionR = new Vector3(enemy.VisionRange, 0.5f, enemy.VisionRange);
        VisionIndi.SetActive(true);
        VisionIndi.transform.localScale = visionR;

        Vector3 senseR = new Vector3(enemy.MinSensingRange, 0.5f, enemy.MinSensingRange);
        SenseIndi.SetActive(true);
        SenseIndi.transform.localScale = senseR;
        //for(int i = 0; i < patrolPoints.Count; i++) {

        //}
        //RenderLine();
        //InitSphere();
        //CreatePatrolPathPoint();
    }

    //public void InitSphere() {
    /*    if (PathPoint == null)
        {
            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.GetComponent<MeshRenderer>().material = IndicatorMat;
            point.transform.parent = transform.GetChild(1);
            point.transform.localPosition = Vector3.zero;
            PathPoint = Instantiate(point, transform.GetChild(1));
            PathPoint.SetActive(false);
        }
    }

    public void CreatePatrolPathPoint()
    {
        if (refresh)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (patrolPoints[i] == null)
                {
                    patrolPoints[i] = Instantiate(PathPoint, transform.GetChild(1)).transform;
                    enemy.PatrolSteps.Add(patrolPoints[i]);
                }     
            }
            refresh = false;
        }
    }

    public void RenderLine() {
        Vector3[] linePoints = new Vector3[patrolPoints.Length];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            linePoints[i] = patrolPoints[i].position;
        }
        line.SetPositions(linePoints);
    }
    */

}
