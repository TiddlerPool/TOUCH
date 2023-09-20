using System.Collections;
using System.Collections.Generic;
using MyAudio;
using UnityEngine;

public class SteleCollapse : MonoBehaviour
{
    public Tower Tower;
    public GameObject SteleMesh;
    public GameObject CollapsePartical;
    public Material CollapseMat;
    [SerializeField] private Material collapseMat;
    [SerializeField] private MeshRenderer _renderer;
    private List<Material[]> mats_oList = new List<Material[]>();
    private List<Material[]> mats_nList = new List<Material[]>();
    [SerializeField]private List<Material> deadMats = new List<Material>();
    private Stele stele;

    private void Update()
    {
        CollapseEffectUpdate();
    }

    private void Awake()
    {
        stele = GetComponent<Stele>();
        _renderer = SteleMesh.transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void OnDisable()
    {
        SteleMesh.SetActive(false);
        stele.reachDsitance = 0f;
    }

    public void CollapseEffectUpdate()
    {
        float thread = Mathf.Lerp(-1.5f, 1.5f, stele.CurrentLifeSpan / stele.LifeSpan);

        collapseMat = _renderer.material;
        collapseMat.SetFloat("_ClipThread", thread);
            
        if(stele.CurrentLifeSpan < 0.01f)
        {
            Instantiate(CollapsePartical, transform.position, Quaternion.Euler(-90, 0, 0));
            AudioManager.PlayAudio("rock");
            stele.touchSign.SetActive(false);
            Tower.Progress++;
            Tower.GoProgress();
            enabled = false;
        }
    }
}