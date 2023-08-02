using System;
using System.Collections;
using AnimationTween;
using UnityEngine;

public class Phantom : Enemy
{

    public GameObject Indicator;
    public TrailRenderer dashTail;
    public Transform dashTarget;
    public float dashDistance;
    [SerializeField] private bool inLight;
    [SerializeField] private bool startDestroy;
    [SerializeField] private bool enable;

    [SerializeField] private AssaultData MeleeData;
    [SerializeField] private AssaultData ChargeData;

    public delegate void PhantomDieEvent();

    public static event PhantomDieEvent OnPhantomDie;

    public bool Enable
    {
        get
        {
        return enable;
        }
        set
        {
        enable = value;
        }
    }

    public override void Start()
    {
        base.Start();
        Assaults = new AssaultData[] { MeleeData, ChargeData };
        Array.Sort(Assaults, new IntValueComparer());
    }

    public override void TraitUpdater()
    {
        base.TraitUpdater();
        DieInLight();
    }

    public IEnumerator Melee(AssaultData data)
    {
        if (CurrentTarget != null)
        {
            if (Vector3.Dot(transform.forward, CurrentTarget.position - transform.position) < 0.9f)
            {
                float rotateDuration = 0.5f;
                float timeRotate = 0f;
                while (timeRotate < rotateDuration)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                        Quaternion.LookRotation(E2TDirection()),
                        Tween.EaseInOut(timeRotate / rotateDuration));
                    yield return null;
                    timeRotate += Time.deltaTime;
                }
                yield return null;
            }
        }
        moveable = false;
        yield return null;

        anim.SetTrigger("Attack");

        data.weight -= 1;
        moveable = true;
        attackIsOver = true;
        nextAttack = Time.time + Assaults[0].colddown;
        Array.Sort(Assaults, new IntValueComparer());
        yield return null;
    }

    public IEnumerator Charge(AssaultData data)
    {
        if (CurrentTarget != null)
        {
            if (Vector3.Dot(transform.forward, CurrentTarget.position - transform.position) < 0.9f)
            {
                float rotateDuration = 0.5f;
                float timeRotate = 0f;
                while (timeRotate < rotateDuration)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                        Quaternion.LookRotation(E2TDirection()),
                        Tween.EaseInOut(timeRotate / rotateDuration));
                    yield return null;
                    timeRotate += Time.deltaTime;
                }
                yield return null;
            }
        }
        moveable = false;
        yield return null;

        anim.SetTrigger("Skill");
        anim.SetBool("DashStart", true);
        yield return new WaitForSeconds(5/6f);

        if (CurrentTarget != null)
        {
            Vector3 normalPoint = CurrentTarget.position + E2TDirection().normalized;

            if (E2TDistance() < dashDistance)
            {
                dashTarget.position = normalPoint;
                Debug.Log("Over");
            }
            else
            {
                dashTarget.position = transform.position + E2TDirection().normalized * dashDistance;
                Debug.Log("Max");
            }
        }
        Vector3 point;
        point = dashTarget.position;
        yield return null;


        float chargeDuration = 3/4f;
        float timeCharge = 0f;
        while(timeCharge<chargeDuration)
        {
            transform.position = Vector3.Lerp(transform.position, point, Tween.EaseInOut(timeCharge / chargeDuration));
            yield return null;
            timeCharge += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("DashStart", false);
        data.weight -= 1;
        moveable = true;
        attackIsOver = true;
        nextAttack = Time.time + Assaults[0].colddown;
        Array.Sort(Assaults, new IntValueComparer());
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightSphere"))
        {
            inLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("LightSphere"))
        {
            inLight = false;
        }
    }

    public void DieInLight()
    {
        if (inLight && !startDestroy)
        {
            StopCoroutine("CountinueHurting");
            StartCoroutine("CountinueHurting");
        }
        else if(!inLight && startDestroy)
        {
            StopCoroutine("CountinueHurting");
            startDestroy = false;
            attackable = true;
            Speed = Speed * 10f;
        }
    }

    public override void GetKill()
    {
        base.GetKill();
        Invoke("GoDie", 0.5f);
    }

    public void GoDie()
    {
        if (TryGetComponent<DeathEffect>(out DeathEffect FX))
            FX.ActivateDeadMats();
        OnPhantomDie?.Invoke();
    }

    IEnumerator CountinueHurting() {

        float speed = 1f;
        Speed = Speed / 10f; 
        startDestroy = true;
        attackable = false;
        yield return null;

        while(inLight)
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 100f);
            Hurt hurtPart = GetComponentInChildren<Hurt>();
            hurtPart.ApplyHurtToRoot(1f,0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
