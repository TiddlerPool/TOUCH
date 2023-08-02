using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomSpawner : MonoBehaviour
{
    [SerializeField] private float currentPhantomCount;
    [SerializeField] private float maxPhantomCount;
    public float CurrentPhantomCount
    {
        get { return currentPhantomCount; }
        set { currentPhantomCount = value; }
    }

    public float MaxPhantomCount
    {
        get { return maxPhantomCount; }
        set { maxPhantomCount = value; }
    }


    public GameObject PhantomPrefab;

    private void Awake()
    {
        Phantom.OnPhantomDie += DecreasePhantomCount;
    }

    public void Start()
    {
        Shader.SetGlobalVector("_Position", transform.position);
        Shader.SetGlobalFloat("_Radius", 0);
    }

    void Update()
    {

    }

    public void SpawnPhantom()
    {
        if (currentPhantomCount >= maxPhantomCount)
        {
            return;
        }

        float randomAngle = Random.Range(0f, 360f);
        Vector3 spawnDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;
        Vector3 spawnPosition = transform.position + spawnDirection * 20f;
        Instantiate(PhantomPrefab, spawnPosition, Quaternion.identity);
        currentPhantomCount++;

    }

    public void DecreasePhantomCount()
    {
        currentPhantomCount--;
    }
}
