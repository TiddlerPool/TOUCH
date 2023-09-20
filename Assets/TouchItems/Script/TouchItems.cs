using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using StarterAssets;
using UnityChan;
using Unity.VisualScripting;
using MyAudio;

public class TouchItems : MonoBehaviour
{
    
    public Transform playerTrans;

    [Header("ConstructSettings")]
    public bool debug;
    [Range(0,5)]public float reachDsitance;
    public string audioName;
    public Light topLight;
    public Light tempLight;
    public Animator anim;
    public Transform lightTrans;
    public Transform sphereTrans;
    public GameObject bottomSphere;
    public GameObject touchHint;
    public GameObject touchSign;
    [SerializeField] private Transform cylinder;

    [Header("LifeSpanState")]
    public bool touched;
    [SerializeField] protected bool timerStart;
    [SerializeField] protected bool needRecharge;
    [SerializeField] protected bool inrange;
    [SerializeField] protected bool reachable;
    [SerializeField] private bool rechargeable;
    [SerializeField, Range(0, 10)] protected float rechargeTime;
    [SerializeField, Range(0, 10)] protected float lifespanTime;
    [SerializeField, Range(0, 10)] protected float sanRecoverSpeed;

    [Header("LifeSpanState")]
    [SerializeField] protected float timerLSP;
    [SerializeField] protected float timerRCG;

    [Header("LightSettings")]
    [Range(0, 10)] public float lightIntensity;
    [Range(0, 15)] public float lightRadius;
    public float growSpeed;
    public float fadeSpeed;
    public float damageBoost;

    //private bool isTouch = false;
    private void Awake()
    {
        GetComponent<CapsuleCollider>().radius = lightRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        Touch();
        CanReachCheck();
        SignCheck();
        RenderRange();
        Growing();
        HintSync();
        LifeSpanTimer();
        RechargeTimer();
        PlayAudio();
    }

    private bool audioPlayed = false;
    private void PlayAudio()
    {
        if (anim.GetBool("growing") && !audioPlayed)
        {
            AudioManager.PlayAudio("touch");
            AudioManager.PlayAudio(audioName);
            audioPlayed = true;
        }
        else if(!anim.GetBool("growing"))
        {
            audioPlayed = false;
        }
    }
    private void RenderRange()
    {
        if (debug)
        {
            if (playerTrans != null)
            print(Vector3.Distance(playerTrans.position,transform.position));
            cylinder.gameObject.SetActive(true);
            cylinder.transform.localScale = new Vector3(reachDsitance * 2f, 1f, reachDsitance * 2f);
        }
        else {
            cylinder.gameObject.SetActive(false);
        }

    }

    private bool CanReachCheck() {
        if (inrange) {
            if (Vector3.Distance(playerTrans.position, transform.position) <= reachDsitance)
                return reachable= true;
            else
                return reachable = false;
            
        }
        else {
            return reachable = false;
        }
    }

    private void Touch()
    {
        if (playerTrans != null)
        {
            var inputAsset = playerTrans.GetComponent<StarterAssetsInputs>();
            if (inputAsset.touch)
            {
                if (reachable)
                {
                    touched = true;
                    timerStart = true;
                    playerTrans.GetComponent<DataController>().DamageValue = damageBoost;
                    playerTrans.GetComponentInChildren<TouchController>().touchTarget.position = touchHint.transform.GetChild(0).transform.position;
                    playerTrans.GetComponentInChildren<SpringManager>().enabled = false;
                    var _weight = playerTrans.GetComponentInChildren<Rig>().weight;
                    playerTrans.GetComponentInChildren<Rig>().weight = Mathf.Lerp(_weight, 1f, Time.deltaTime * 0.4f);
                }
            }
            else
            { 
                if (reachable)
                {
                    touched = false;
                    timerStart = false;
                    playerTrans.GetComponent<DataController>().DamageValue = 0f;
                    playerTrans.GetComponentInChildren<TouchController>().touchTarget.position = playerTrans.transform.position;
                    playerTrans.GetComponentInChildren<SpringManager>().enabled = true;
                    playerTrans.GetComponentInChildren<Rig>().weight = 0f;
                }
            }
        }
    }

    protected void RecoverSan(float speed)
    {
        if (playerTrans != null)
        {
            playerTrans.GetComponent<DataController>().ApplySan( -speed * 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
//            isTouch = true;
            playerTrans = other.transform;
            inrange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrans = other.transform;
            inrange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
//            isTouch = false;
            playerTrans = null;
            inrange = false;
        }
    }

    public void SignCheck() {
        Animator tsAnim = touchSign.GetComponent<Animator>();
        tsAnim.SetBool("gettouch", touched);
        if (reachable && !needRecharge) {
            
            tsAnim.SetBool("show", true);
            
        }
        else {
            tsAnim.SetBool("show", false);
        }    
    }

    public virtual void Growing() 
    {
        var radius = Shader.GetGlobalFloat("_Radius");
        if (reachable && touched && !needRecharge) 
        {
            RecoverSan(sanRecoverSpeed);

            Shader.SetGlobalVector("_Position", sphereTrans.position);

            if (tempLight == null)
            {
                Light _tempLight = Instantiate(topLight, lightTrans);
                tempLight = _tempLight;
                tempLight.range = lightRadius;
            }

            if(tempLight != null)
            {
                var r = radius;
                r = Mathf.Lerp(r, lightRadius, Time.deltaTime * growSpeed);
                Vector3 scale = new Vector3(r, r, r);

                tempLight.intensity = Mathf.Lerp(tempLight.intensity, lightIntensity, Time.deltaTime * growSpeed);
                bottomSphere.transform.localScale = scale;

                Shader.SetGlobalFloat("_Radius", r);
                anim.SetBool("growing", true);
                
                
            }

            bottomSphere.GetComponent<Collider>().enabled = true;
        }
        else
        {
            if (tempLight != null)
            {
                var r = radius;
                
                r = Mathf.Lerp(r, 0, Time.deltaTime * fadeSpeed);
                Vector3 scale = new Vector3(r, r, r);

                Shader.SetGlobalFloat("_Radius", r);
                tempLight.intensity = Mathf.Lerp(tempLight.intensity, 0.0f, Time.deltaTime * fadeSpeed);
                bottomSphere.transform.localScale = scale;
                anim.SetBool("growing", false);
            }

            if (tempLight != null && tempLight.intensity <= 0.1f) {
                Destroy(tempLight.gameObject);
                bottomSphere.GetComponent<Collider>().enabled = false;
                //Destroy(tempSphere.gameObject);
            }
        }
    }

    public virtual void EffectLifeSpan() {
        if (rechargeable) {

        }
    }

    private void LifeSpanTimer() {
        if (timerStart) {
            timerLSP = Mathf.Clamp( timerLSP -= Time.deltaTime, 0, lifespanTime);
            if(timerLSP <= 0.0f) {
                needRecharge = true;
                timerStart = false;
            }
        }
        else if(!needRecharge && !timerStart) {
            timerLSP = Mathf.Clamp(timerLSP += Time.deltaTime * 0.6f, 0, lifespanTime);
        }
    }

    public virtual void RechargeTimer()
    {
        if (needRecharge)
        {
            timerRCG -= Time.deltaTime;
            if (timerRCG <= 0.0f)
            {
                needRecharge = false;
                ResetTimer();
            }
        }
    }

    protected void ResetTimer() {
        timerLSP = lifespanTime;
        timerRCG = rechargeTime;
    }

    public void HintSync() {
        if (playerTrans != null)
        {
            Vector3 v3 = playerTrans.position - transform.position; 
            touchHint.transform.rotation = Quaternion.LookRotation(v3);
        }
    }

}
