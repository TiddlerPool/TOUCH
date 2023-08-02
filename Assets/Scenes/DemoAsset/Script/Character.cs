using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float maxHealth = 10f;
    public static float maxSan= 100f;
    public static float maxFuel= 100f;
    //private float currentHealth;
    public static float health;
    public static float san;
    public static float fuel;

    private void Awake()
    {
        health = maxHealth;
        san = maxSan;
        fuel = maxFuel/2;
    }

    void Start()
    {
        //health = maxHealth;
        //san = maxSan;
        //fuel = maxFuel/2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        health = Mathf.Clamp(health, 0, 10f);
        san = Mathf.Clamp(san, 0, 100f);
        fuel = Mathf.Clamp(fuel, 0, 100f);
    }
    
    
    
}
