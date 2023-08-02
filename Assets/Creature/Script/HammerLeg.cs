using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationTween;
using static HammerLeg;

public class HammerLeg : MonoBehaviour
{
    [System.Serializable]
    public class LegIK
    {
        public bool AllowMove;
        public bool Moving;
        public int ID;
        public int ReverseID;
        public float StepDistance;
        public Transform FootIK;
        public Transform NaviPos;
        public Vector3 IKPos;
    }

    public bool Malfunction;
    public float StepRate;

    public float HipOffset;
    public Transform hip;
    public List<LegIK> Legs;

    

    public Transform FootIK_f_r;
    public Transform FootIK_f_l;
    public Transform FootIK_b_r;
    public Transform FootIK_b_l;

    public Transform NaviPos_f_r;
    public Transform NaviPos_f_l;
    public Transform NaviPos_b_r;
    public Transform NaviPos_b_l;

    public Vector3 IKpos;

    [SerializeField] private LegIK front_Left;
    [SerializeField] private LegIK front_Right;
    [SerializeField] private LegIK back_Left;
    [SerializeField] private LegIK back_Right;

    private void Awake()
    {
        SetUp();
    }

    void Start()
    {
    }

    
    void Update()
    {
        StepPosUpdate(front_Left);
        StepPosUpdate(front_Right);
        StepPosUpdate(back_Left);
        StepPosUpdate(back_Right);
        //MaintainHip();
    }

    private void SetUp()
    {
        front_Left = AssignLeg(front_Left);
        Legs.Add(front_Left);
        front_Right = AssignLeg(front_Right);
        Legs.Add(front_Right);
        back_Left = AssignLeg(back_Left);
        Legs.Add(back_Left);
        back_Right = AssignLeg(back_Right);
        Legs.Add(back_Right);
    }

    private LegIK AssignLeg(LegIK leg)
    {
        leg.IKPos = leg.FootIK.position;
        return leg;
    }

    Vector3 CalculateAverageVector(Vector3[] vectors)
    {
        Vector3 sumVector = Vector3.zero;

        foreach (Vector3 vector in vectors)
        {
            sumVector += vector;
        }

        Vector3 averageVector = sumVector / vectors.Length;

        return averageVector;
    }

    public void MaintainHip()
    {
        Vector3[] fourPoints = new Vector3[Legs.Count];
        for(int i = 0; i < Legs.Count; i++)
        {
            fourPoints[i] = Legs[i].FootIK.position;
        }

        var avg = CalculateAverageVector(fourPoints);

        Vector3 hipPos = new Vector3(avg.x, avg.y + HipOffset, avg.z);
        print(hipPos);
        hip.position = hipPos;
    }

    private void StepPosUpdate(LegIK leg)
    {
        if (!Malfunction)
        {
            if (DistanceCheck(leg) && !leg.Moving)
            {
                if (leg.AllowMove)
                {
                    if (leg.ID <= 1)
                    {
                        Legs[leg.ID + 2].AllowMove = false;
                    }
                    leg.AllowMove = false;
                    leg.Moving = true;
                    StartCoroutine("MoveLegs", leg);
                }
            }
            else if (!leg.Moving)
            {
                leg.FootIK.position = leg.IKPos;
            }
        }
        else
        {
            DownWardRayCheck(leg);
            leg.FootIK.position = leg.IKPos;
        }
    }

    private bool DistanceCheck(LegIK leg)
    {
        bool boolen;
        Vector2 point1 = new Vector2(leg.NaviPos.position.x, leg.NaviPos.position.z);
        Vector2 point2 = new Vector2(leg.FootIK.position.x, leg.FootIK.position.z);
        float dis = Vector2.Distance(point1, point2);
        if (dis > leg.StepDistance)
            boolen = true;
        else
            boolen = false;
        return boolen;
    }

    public IEnumerator MoveLegs(LegIK leg)
    {
        float elasped_time = 0;
        leg.IKPos = leg.NaviPos.position;
        Vector3 pos = leg.FootIK.position;
         
        

        yield return null;

        while (elasped_time <= StepRate)
        {
            DownWardRayCheck(leg);
            pos.x = Mathf.Lerp(leg.FootIK.position.x, leg.IKPos.x, Tween.EaseInOut(elasped_time / StepRate));
            pos.z = Mathf.Lerp(leg.FootIK.position.z, leg.IKPos.z, Tween.EaseInOut(elasped_time / StepRate));
            if(Tween.EaseIn(elasped_time / StepRate) < 0.5f)
            pos.y = Mathf.Lerp(leg.FootIK.position.y, leg.IKPos.y + 1.0f, Tween.EaseIn(elasped_time / StepRate));
            else
                pos.y = Mathf.Lerp(leg.FootIK.position.y, leg.IKPos.y, Tween.EaseOut(elasped_time / StepRate));

            leg.FootIK.position = pos;
            yield return null;
            elasped_time += Time.deltaTime;
            yield return null;
        }

        if (leg.ID <= 1)
        {
            Legs[leg.ID + 2].AllowMove = true;
        }
        Legs[leg.ReverseID].AllowMove = true;
        leg.Moving = false;
        leg.IKPos = pos;
        yield return null;
    }

    public void DownWardRayCheck(LegIK leg)
    {
        Ray ray = new Ray(leg.NaviPos.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 6f,9))
        {
            leg.IKPos = hit.point;
        }
    }
}
