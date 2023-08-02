using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<GameObject> lights;

    public GameObject TowerCamera;
    public Material PassMat;
    public TowerDoorTrigger Door;
    public int Progress
    {
        get { return progress; }
        set { progress = value; }
    }

    private int progress = 0;

    void Update()
    {
        if (progress == 3)
        {
            Door.IsPassed = true;
        }
    }

    public void GoProgress()
    {
        StartCoroutine("ProgressCamera", progress);
    }

    public IEnumerator ProgressCamera(int progress)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
        TowerCamera.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);

        if (progress >= 1)
        {
            lights[progress - 1].GetComponent<MeshRenderer>().material = PassMat;
        }
        yield return new WaitForSecondsRealtime(1f);
        TowerCamera.SetActive(false);
        Time.timeScale = 1f;
    }
}
