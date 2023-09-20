using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using MyAudio;
public class CannonAnimEvent : MonoBehaviour
{
    public Cannon self;

    public void StartAim()
    {
        self.IsAim = true;
    }

    public void StopAim()
    {
        self.IsAim = false;
    }

    public void Fire()
    {
        self.FireBeam();
        AudioManager.PlayAudio("shield3");
    }

    public void Reborn()
    {
        self.anim.SetLayerWeight(2, 0f);
        self.Health = 2f;
        self.aim.weight = 1f;
        self.RefreshState();
        self.Attackable = true;
    }
}
