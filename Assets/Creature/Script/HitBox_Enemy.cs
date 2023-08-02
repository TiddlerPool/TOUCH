using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Enemy : MonoBehaviour
{
    public Enemy self;
    public bool Continuing;

    [SerializeField]private float damageValue;

    private void Update()
    {
        damageValue = self.AttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Hurt>(out Hurt hurt))
        {
            hurt.ApplyHurtToRoot(damageValue, self.GetComponent<EntityFactions>().FactionID); 
        }
        else
        {
            Debug.Log("Target can no hurt!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Continuing)
        {
            if (other.TryGetComponent<Hurt>(out Hurt hurt))
            {
                hurt.ApplyHurtToRoot(damageValue, self.GetComponent<EntityFactions>().FactionID);
            }
            else
            {
                Debug.Log("Target can no hurt!");
            }
        }
    }
}
