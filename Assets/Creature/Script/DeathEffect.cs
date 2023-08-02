using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public GameObject DeathPartical;
    public Material DeadMat;
    private Material deadMat;
    [SerializeField]private MeshRenderer[] renderers;
    private List<Material[]> mats_oList = new List<Material[]>();
    private List<Material[]> mats_nList = new List<Material[]>();
    private List<Material> deadMats = new List<Material>();

    private void Start()
    {
        StartCoroutine("DeathEffectSetUp");
        deadMat = Material.Instantiate(DeadMat);
    }

    public void ActivateDeadMats()
    {
        for(int i = 0; i < renderers.Length;i++)
        {
            renderers[i].materials = mats_nList[i];
        }

        StartCoroutine("DeathEffectApply");
    }

    IEnumerator DeathEffectApply()
    {
        foreach(Material mat in deadMats)
        {
            while (mat.GetFloat("_ClipThread") > -2f)
            {
                mat.SetFloat("_ClipThread", mat.GetFloat("_ClipThread") - 1/200f);
                //print(mat.GetFloat("_ClipThread"));
                yield return null;
            }
            
        }

        yield return new WaitForSeconds(0.4f);

        Instantiate(DeathPartical, transform.position, Quaternion.Euler(-90, 0 ,0));
        yield return null;

        Destroy(gameObject);
        yield return null;
        
    }

    IEnumerator DeathEffectSetUp()
    {
        int i = 0;
        while (mats_oList.Count < renderers.Length)
        {
            
            mats_oList.Add(renderers[i].materials);
            i++;
            yield return null;
        }

        int c = 0;
        while (mats_nList.Count < mats_oList.Count)
        {
            var mats_o = renderers[c].materials;
            var mats_n = new Material[mats_o.Length + 1];

            for (int s = 0; s < mats_n.Length; s++)
            {
                if (s == 0)
                {
                    mats_n[s] = deadMat;
                    deadMats.Add(mats_n[s]);
                }
                else
                {
                    mats_n[s] = mats_o[s - 1];
                }
            }

            mats_nList.Add(mats_n);
            c++;
            yield return null;  
        }

        
        yield return null;

    }
}