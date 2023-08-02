using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public Material damageMaterial;     
    public float damageDuration = 1f;
    public int HurtID
    {
        get {return entityManager.GetIDByEntity(gameObject); }
        private set { }
    }

    private GlobalEntityManager entityManager;
    private Renderer _renderer;
    [SerializeField]Material[] mats_o;
    Material[] mats_n;

    private void Awake()
    {
        entityManager = FindObjectOfType<GlobalEntityManager>();
    }

    void Start()
    {
        HurtEvents.current.onHurtBegin += TakeDamage;
        _renderer = GetComponent<Renderer>();
        StartCoroutine("SetUp");
    }


    private void OnEnable()
    {
        int entityID = entityManager.RegisterEntity(gameObject);
        Debug.Log(gameObject.name + " have registered with ID: " + entityID);
    }

    private void OnDisable()
    {
        entityManager.UnregisterEntity(gameObject);
    }

    public void ApplyHurtToRoot(float damage,int damageOrigin)
    {
        IDamageable hurtInterafce;
        hurtInterafce = GetComponentInParent<IDamageable>();
        if (hurtInterafce != null)
        {
            hurtInterafce.GetHurt(damage, HurtID, damageOrigin);
        }
        else
        {
            Debug.Log("Can not find the interface");
        }
    }

    public void TakeDamage(int id)
    {
        if(id == HurtID)
        StartCoroutine(ShowDamageEffect());
    }

    private IEnumerator ShowDamageEffect()
    {
        if(mats_n != null)
        _renderer.materials = mats_n;

        yield return new WaitForSeconds(damageDuration);
        _renderer.materials = mats_o;
        yield return null;
    }

    private void OnDestroy()
    {
        HurtEvents.current.onHurtBegin -= TakeDamage;
    }

    private IEnumerator SetUp()
    {
        yield return new WaitForSeconds(0.1f);
        mats_o = _renderer.materials;
        mats_n = new Material[mats_o.Length + 1];
        yield return null;
        for (int i = 0; i < mats_n.Length; i++)
        {
            if (i == 0)
                mats_n[i] = damageMaterial;
            else
                mats_n[i] = mats_o[i - 1];
            yield return null;
        }
    }
}
