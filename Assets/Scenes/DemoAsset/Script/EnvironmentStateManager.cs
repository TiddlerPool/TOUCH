using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentStateManager : MonoBehaviour
{
    public float SpeedOfSanDecrease;
    public float SanThreshold;

    private DataController controller;
    private PhantomSpawner spawner;
    [SerializeField]private bool sanDecrease;

    public bool SanDecrease {
        get {
            return sanDecrease;
        }
        set {
            sanDecrease = value;
        }
    }

    private void Awake()
    {
        sanDecrease = true;
        controller = GetComponent<DataController>();
        spawner = GetComponent<PhantomSpawner>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SanNaturalDecrease();
        SanLowLevelPenalty();
    }

    private void SanNaturalDecrease() {
        if (sanDecrease) {
            controller.ApplySan(SpeedOfSanDecrease * 0.1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightSphere")) {
            sanDecrease = false;
        }
        
    }

    private void SanLowLevelPenalty()
    {
        if (Character.san < SanThreshold)
        {

            int numMonstersToSpawn = Mathf.CeilToInt(spawner.MaxPhantomCount * (SanThreshold - Character.san) / SanThreshold);

            // Spawn the monsters.
            for (int i = 0; i < numMonstersToSpawn; i++)
            {
                spawner.SpawnPhantom();
            }
        }
    }
}
