using UnityEngine;
using System;
using System.Collections;
using AnimationTween;

public class LazerEmitter : MonoBehaviour
{
    public bool Emitting;
    [SerializeField]private bool enableEmi;
    Vector3 LazerStart;
    Vector3 LazerTarget;
    Vector3 RayHit;
    [SerializeField]private Transform player;
    [SerializeField]private GameObject HitBox;
    [SerializeField]private LineRenderer LazerLine;
    [SerializeField]private ParticleSystem LazerPartical;

    private void Awake()
    {
        LazerStart = transform.position;
        player = GameObject.Find("PlayerCapsule").transform;
    }

    void Update()
    {
        if (Emitting && !enableEmi)
        {
            StopCoroutine("Lazer");
            StartCoroutine("Lazer");
            enableEmi = true;
        }
        else if (!Emitting && enableEmi)
        {
            StopCoroutine("Lazer");
            enableEmi = false;
            HitBox.SetActive(false);
            LazerLine.enabled = false;
            LazerPartical.Stop();

        }
        else
        {
            HitBox.SetActive(false);
            LazerLine.enabled = false;
            LazerPartical.Stop();
        }

        LazerLine.SetPosition(0, LazerStart);
        LazerLine.SetPosition(1, LazerTarget);
    }

    private void LazerEmit()
    {

            Ray ray = new Ray(transform.position, AimDir());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                RayHit = hit.point;
            }
            else
            {
                RayHit = transform.position + AimDir().normalized * 100f;
            }
            LazerStart = transform.position;
            LazerTarget = RayHit;
            LazerPartical.Play();
            LazerPartical.transform.position = LazerTarget;
            LazerPartical.transform.rotation = Quaternion.LookRotation(transform.forward, transform.position - LazerTarget);
            HitBox.SetActive(true);
            HitBox.transform.position = LazerTarget;
            HitBox.transform.rotation = Quaternion.LookRotation(transform.position - LazerTarget);
            LazerLine.enabled = true;

            
    }

    public Vector3 AimDir()
    {
        Vector3 dir;
        Vector3 v1 = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 v2 = new Vector3( player.position.x, 0f, player.position.z);
        dir = v1 - v2;
        return dir;
    }

    public IEnumerator Lazer()
    { 
        LazerTarget = LazerStart;
        LazerLine.enabled = true;

        yield return null;

        while(Emitting)
        {
            LazerEmit();
            yield return null;
        }

        enableEmi = false;
        HitBox.SetActive(false);
        LazerLine.enabled = false;
        LazerPartical.Stop();
        yield return null;
    }
}

