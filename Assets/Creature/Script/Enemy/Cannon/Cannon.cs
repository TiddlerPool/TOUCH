using System.Collections;
using System.Collections.Generic;
using MyAudio;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class Cannon : Enemy
{
    public AssaultData ShootData;
    public Transform cannon;
    public Transform target;
    public GameObject bullet;
    public Rig aim;
    public float fireRate;
    public float rebornTime;

    public bool IsAim;
    public bool Attackable
    {
        get { return attackable; }
        set { attackable = value; }
    }

    public override void Start()
    {
        base.Start();
        Assaults = new AssaultData[] {ShootData};
    }

    private bool activePlayed;
    public override void TraitUpdater()
    {
        base.TraitUpdater();
        CannonRotate();
        if (CurrentTarget != null && !activePlayed)
        {
            //print("Notied");
            AudioManager.PlayAudio("active1");
            activePlayed = true;
        }
        else if(TargetList.Count == 0)
        {
            activePlayed = false;
        }
    }

    private void CannonRotate() {
        if (CurrentTarget != null && IsAim)
        {
            //Debug.Log("Locking");
            aim.weight = Mathf.Lerp(aim.weight, 1f, Time.deltaTime * 2f);
            var direction = CurrentTarget.position - transform.position;
            cannon.transform.rotation = Quaternion.LookRotation(E2TDirection());

            target.position = Vector3.Lerp(target.position, CurrentTarget.position, Time.deltaTime * 3f);
        }
        else
        {
            aim.weight = Mathf.Lerp(aim.weight, 0f, Time.deltaTime);
        }
    }

    public void FireBeam()
    {
       var beam = Instantiate(bullet, eyes.position, Quaternion.LookRotation(eyes.position - cannon.position));
        beam.GetComponent<Bullet>().Damage = AttackDamage;
        beam.GetComponent<Bullet>().shooterID = GetComponent<EntityFactions>().FactionID;
    }

    public IEnumerator Shoot()
    {
        Debug.Log("Corountine Started");
        yield return new WaitUntil(() =>anim.GetCurrentAnimatorStateInfo(1).IsName("Cannon_aim"));
        anim.SetTrigger("Attack");
        Debug.Log("Cannon Shoot");
        yield return null;
        attackIsOver = true;
    }

    public override void GetKill()
    {
        base.GetKill();
        attackable = false;
        aim.weight = 0f;
        anim.SetLayerWeight(1, 0);
        anim.SetInteger("State", 1);
        Invoke("ReBorn", rebornTime);
    }

    public void ReBorn()
    {
        isDead = false;
        
    }
}
