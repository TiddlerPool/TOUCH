using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomAnimEvent : MonoBehaviour
{
    public Phantom self;

    public void SlowDown() {
        self.Speed = self.Speed * 1 / 5f;
    }

    public void SpeedUp()
    {
        self.Speed = self.Speed * 5f;
    }


}
