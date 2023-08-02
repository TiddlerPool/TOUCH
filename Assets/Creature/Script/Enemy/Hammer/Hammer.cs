using System;
using System.Collections;
using AnimationTween;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;

public class Hammer : Enemy
{
    public bool enableUpdate;
    public bool resetNeck;

    public GameObject BackStele;
    public GameObject Bullet;
    public GameObject TrampleHitBox;
    public ParticleSystem TramplePartical;

    public Vector3 LazerStart;
    public Vector3 LazerTarget;
    public LineRenderer LazerLine;
    public ParticleSystem LazerPartical;

    public Transform NeckIK;
    public Transform AimIK;
    public ChainIKConstraint NeckConstraint;

    [SerializeField] private AssaultData MeleeData;
    [SerializeField] private AssaultData ShootData;
    [SerializeField] private AssaultData LazerData;

    public override void Start()
    {
        base.Start();
        Assaults = new AssaultData[] {MeleeData,ShootData, LazerData };
        Array.Sort(Assaults, new IntValueComparer());
        Health = MaxHealth;
        LazerStart = eyes.position;
        LazerTarget = LazerStart;
    }

    public override void TraitUpdater()
    {
        base.TraitUpdater();
        if(CurrentTarget == null)
        {
            NeckIK.position = Vector3.Lerp(NeckIK.position, transform.position + transform.forward * 5f, Time.deltaTime * 10f);
            AimIK.position = Vector3.Lerp(AimIK.position, transform.position + transform.forward * 5f, Time.deltaTime * 10f);
        }
        else
        {
            NeckIKLocking();
        }

        LazerStart = eyes.position;
        LazerLine.SetPosition(0, LazerStart);
        LazerLine.SetPosition(1, LazerTarget);

    }

    public void NeckIKLocking()
    {
        NeckIK.position = Vector3.Lerp(NeckIK.position, CurrentTarget.position, Time.deltaTime * 2f) ;
        AimIK.position = Vector3.Lerp(AimIK.position, CurrentTarget.position, Time.deltaTime * 5f);
    }

    public IEnumerator Melee(AssaultData data)
    {
        if (Vector3.Dot(transform.forward, CurrentTarget.position - transform.position) < 0.9f)
        {
            float rotateDuration = 0.5f;
            float timeRotate = 0f;
            while (timeRotate < rotateDuration)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(E2TDirection()),
                    Tween.EaseInOut(timeRotate/rotateDuration));
                yield return null;
                timeRotate += Time.deltaTime;
            }
        }
        yield return null;
        GetComponent<HammerLeg>().enabled = false;
        moveable = false;

        float legDuration = 0.5f;
        float timeLeg = 0f;
        HammerLeg leg = GetComponent<HammerLeg>();
        leg.DownWardRayCheck(leg.Legs[1]);
        Vector3 legOriPos = leg.Legs[1].IKPos;
        Vector3 legMaxPos = leg.Legs[1].IKPos + new Vector3(0, 1.5f, 0);
        

        while (timeLeg < legDuration)
        {

            leg.FootIK_f_r.position = Vector3.Lerp(legOriPos, legMaxPos, Tween.Spike(timeLeg / legDuration, 1f, 2f, 0.8f));
            yield return null;

            timeLeg += Time.deltaTime;
            yield return null;
        }

        Instantiate(TramplePartical, leg.FootIK_f_r.position, Quaternion.identity);
        GameObject trampleBox = Instantiate(TrampleHitBox, leg.FootIK_f_r.position, Quaternion.identity);
        trampleBox.GetComponent<HitBox_Enemy>().self = this;
        yield return null;

        GetComponent<HammerLeg>().enabled = true;
        data.weight -= 1;
        moveable = true;
        attackIsOver = true;
        nextAttack = Time.time + Assaults[0].colddown;
        Array.Sort(Assaults, new IntValueComparer());
        yield return null;
    }

    public IEnumerator Shoot(AssaultData data)
    {
        if(CurrentTarget != null)
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
        while(NeckConstraint.weight < 0.3f)
        {
            NeckConstraint.weight = Mathf.Lerp(NeckConstraint.weight, 0.3f, Time.deltaTime);
            if ((0.3f - NeckConstraint.weight) < 0.01f)
                NeckConstraint.weight = 0.3f;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        Vector3 aimTargetPos = AimIK.position;
        if (CurrentTarget != null)
        {
            aimTargetPos = CurrentTarget.position;
        }
        for (int i = 0; i < 3; i++)
        {
            Vector3 aimPos = new Vector3(aimTargetPos.x, aimTargetPos.y + 1f, aimTargetPos.z);
            GameObject beam = Instantiate(Bullet, eyes.position, Quaternion.LookRotation(aimPos - eyes.position));
            beam.GetComponent<Bullet>().Damage = data.damage;
            beam.GetComponent<Bullet>().shooterID = GetComponent<EntityFactions>().FactionID;
            print("Shoot!");
            yield return new WaitForSeconds(0.2f);
        }

        while (NeckConstraint.weight > 0.09f)
        {
            NeckConstraint.weight = Mathf.Lerp(NeckConstraint.weight, 0.09f, Time.deltaTime);
            if ((NeckConstraint.weight - 0.09f) < 0.01f)
                NeckConstraint.weight = 0.09f;
            yield return null;
        }

        data.weight -= 2;
        moveable = true;
        attackIsOver = true;
        nextAttack = Time.time + Assaults[0].colddown;
        Array.Sort(Assaults, new IntValueComparer());
        yield return null;
    }

    public IEnumerator Lazer(AssaultData data)
    {
        if(CurrentTarget != null)
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
            }
            yield return null;
        }
        

        moveable = false;
        yield return null;
        yield return new WaitForSeconds(3f);

        Vector3 IniPos = eyes.position;
        Ray ray = new Ray(eyes.position, AimIK.position - eyes.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            IniPos = hit.point;
        }
        LazerTarget = LazerStart;
        LazerLine.enabled = true;

        yield return null;

        float lazerDuration = 0.2f;
        float timelazer = 0f;
        while (timelazer < lazerDuration)
        {
            LazerTarget = Vector3.Lerp(LazerTarget, IniPos, Tween.EaseInOut(timelazer/lazerDuration));
            yield return null;
            timelazer += Time.deltaTime;
        }

        float time = 0f;
        float duration = 3.5f;
        LazerPartical.Play();
        while(time < duration)
        {
            Vector3 targetPos = LazerTarget;
            if(CurrentTarget != null)
            {
                targetPos = CurrentTarget.position;
            }
            LazerTarget = Vector3.Lerp(LazerTarget, targetPos, Time.deltaTime *5f);
            LazerPartical.transform.position = LazerTarget;
            data.Hitbox.SetActive(true);
            data.Hitbox.transform.position = LazerTarget;
            yield return null;

            time += Time.deltaTime;
            yield return null;
        }

        LazerLine.enabled = false;
        data.weight -= 4;
        moveable = true;
        attackIsOver = true;
        LazerPartical.Stop();
        data.Hitbox.SetActive(false);
        nextAttack = Time.time + Assaults[0].colddown;
        Array.Sort(Assaults, new IntValueComparer());
        yield return null;
    }

    public override void GetKill()
    {
        base.GetKill();
        BackStele.transform.parent = null;
        BackStele.GetComponent<Rigidbody>().useGravity = true;
        BackStele.GetComponent<Rigidbody>().isKinematic = false;
        StopAllCoroutines();
        Invoke("GoDie", 1f);
    }

    public void GoDie()
    {
        if (TryGetComponent<DeathEffect>(out DeathEffect FX))
            FX.ActivateDeadMats();
    }
}
