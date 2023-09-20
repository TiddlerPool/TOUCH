using System.Collections;
using System.Collections.Generic;
using MyAudio;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int shooterID;
    public float speed;
    public GameObject HitPartical;
    public GameObject AbsorbPartical;
    private Rigidbody rb;
    [SerializeField]private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }

    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        rb.velocity = transform.forward * speed;
        Invoke("DestroyItself", 10f);
    }

    /*private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Hurt>(out Hurt hurt))
        {
            hurt.ApplyHurtToRoot(damage, shooterID);
        }
        else
        {
          Debug.Log(other.gameObject.name +" can no hurt!");
        }
    }
    */

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Barrier"))
        {
            other.GetComponent<Shield>().ReflectBullet(this);
        }
            if (other.TryGetComponent<Hurt>(out Hurt hurt))
        {
            hurt.ApplyHurtToRoot(damage, shooterID); 
        }
        else
        {
            Debug.Log(other.gameObject.name + " can no hurt!");
        }     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Barrier"))
        {
            ContactPoint contact = collision.GetContact(0);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(AbsorbPartical, pos, rot);
            Debug.Log("Beam absorb by shield");
            Destroy(gameObject);
        }
        else
        {
            ContactPoint contact = collision.GetContact(0);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(HitPartical, pos, rot);
            AudioManager.PlayAudio("bullet");
            Debug.Log("Beam Hit and Explode");
            Destroy(gameObject);
        }

    }

    private void DestroyItself()
       {
        Destroy(gameObject);
    }
}
