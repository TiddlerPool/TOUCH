using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void GetHurt(float value, int hitpartID, int damageOrigin);
}
