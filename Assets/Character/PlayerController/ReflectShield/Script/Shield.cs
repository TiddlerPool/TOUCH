using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;
    public GameObject boundPartical;
    public Transform refPoint;
    public float timeSlowDuration;


    public void ReflectBullet(Bullet bullet)
    {
        Debug.Log("Reflect!");
        Vector3 aimDir = player.GetComponent<TouchController>().aimPos.normalized;
        GameObject ReBullet = Instantiate(bullet.gameObject, refPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        ReBullet.GetComponent<Bullet>().Damage = bullet.Damage * 2f;
        ReBullet.GetComponent<Bullet>().shooterID = 1;
        //Instantiate(boundPartical, refPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }


    IEnumerator RecoverTime()
    {
        yield return new WaitForSecondsRealtime(timeSlowDuration);
        TimeSlowEvents.current.SlowEnd();
        yield return null;
    }

}
