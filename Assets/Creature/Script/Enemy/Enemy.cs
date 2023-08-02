using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class Enemy : MonoBehaviour, IDamageable
{
    EnemyBaseState currentState;
    //GlobalEntityManager entityManager;

    [Header("Enemy State")]
    public int animState;
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public AttackState AttackState = new AttackState();

    [Header("Settings")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] protected bool isDead;
    [SerializeField] protected bool territorial;
    [SerializeField] protected bool moveable;
    [SerializeField] protected bool patrolable;
    [SerializeField] protected bool rotateable;
    [SerializeField] protected bool attackable;


    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public bool Territorial
    {
        get { return territorial; }
        set { territorial = value; }
    }

    public bool Patrolable
    {
        get { return patrolable; }
        set { patrolable = value; }
    }

    [Header("Vision")]
    public float VisionRange;
    public float MinSensingRange;
    [SerializeField] public List<Transform> TargetList;
    [SerializeField] protected Transform eyes;
    [SerializeField] public Transform CurrentTarget;

    [Header("Moving")]
    public float Speed;
    public float TerritoryRange;
    public Transform Territory;
    public int stepIndex;
    private bool moveForward;
    [SerializeField] protected float patrolBreakTime;
    public List<Transform> PatrolSteps;

    [Header("Attack")]
    public float AttackDamage;
    public float AttackRange;
    public float AttackRate;
    public bool attackIsOver;
    public AssaultData[] Assaults;
    protected float nextAttack;

    [Header("Injury")]
    protected float nextHurt;
    [SerializeField] protected float hurtRate;
   
    protected Rigidbody rb;
    public Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public virtual void Start()
    {
        TransitionToState(PatrolState);

    }

    public void Update()
    {
        anim.SetBool("IsDead", isDead);
        if (isDead)
        {
            anim.SetLayerWeight(2, 1f);
            return;
        }
            
        anim.SetInteger("State", animState);
        TargetLost();
        DamageManager();
        EnemyVision();
        TraitUpdater();
        AttackRange = Assaults[0].range;
        AttackDamage = Assaults[0].damage;
        currentState.OnUpdate(this);
    }

    private void OnEnable()
    {
        //int entityID = entityManager.RegisterEntity(gameObject);
        //Debug.Log("Registered with ID: " + entityID);
    }

    private void OnDisable()
    {
        //entityManager.UnregisterEntity(gameObject);
    }

    public void EnemyMove()
    {
        if(moveable && CurrentTarget != null)
        {
            transform.SetPositionAndRotation
                (
                Vector3.MoveTowards(transform.position, CurrentTarget.position, Time.deltaTime * Speed),
                Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(E2TDirection()), Time.deltaTime)
                );
        }
    }

    protected void EnemyVision() {

        //Eye Vision Longer Searching System
        var collisions = Physics.OverlapSphere(eyes.position, VisionRange);
        if (collisions.Length >0f)
        {
            foreach(var collision in collisions)
            {
                if (collision.TryGetComponent<EntityFactions>(out EntityFactions faction))
                {
                    var dir = Vector3.Dot(eyes.forward, collision.transform.position - transform.position);
                    var dis = Vector3.Distance(collision.transform.position, transform.position);
                    if (faction.FactionID == 1 && dir >= 1f / 6f)
                    {
                        if (!TargetList.Contains(collision.transform))
                        {
                            TargetList.Add(collision.transform);
                        }
                    }
                    else if (faction.FactionID == 1 && dis <= MinSensingRange)
                    {
                        if (!TargetList.Contains(collision.transform))
                        {
                            TargetList.Add(collision.transform);
                        }
                    }
                }  
            }         
        }
    }

    public void TargetLost()
    {
        if(TargetList != null)
        {
            for (int i = 0; i < TargetList.Count; i++)
            {
                TargetList[i].TryGetComponent<EntityFactions>(out EntityFactions faction);
                var dis = Vector3.Distance(TargetList[i].transform.position, transform.position);
                if (dis > VisionRange || faction.FactionID != 1)
                {
                    if (CurrentTarget == TargetList[i])
                        CurrentTarget = null;
                    TargetList.Remove(TargetList[i]);
                }
                
            }
        }
    }

    public virtual void AttackAction()
    {
        if (attackable)
        {
            if (Time.time > nextAttack && attackIsOver)
            {
                attackIsOver = false;
                StopCoroutine(Assaults[0].name);
                StartCoroutine(Assaults[0].name, Assaults[0]);
                Debug.Log(gameObject.name + "Perform Attack!!!");
            }           
        }
    }

    public void SwitchPatrolTargetPoints()
    {
        if (moveable)
        {
            if (patrolable)
            {
                if (Vector3.Distance(transform.position, PatrolSteps[stepIndex].position) <= 1f)
                {
                    StopCoroutine("PatrolBreakBehaviour");
                    StartCoroutine("PatrolBreakBehaviour");
                    if (moveForward)
                    {
                        stepIndex++;
                        if (stepIndex >= PatrolSteps.Count)
                        {
                            stepIndex = PatrolSteps.Count - 2;
                            moveForward = false;
                        }
                    }
                    else
                    {
                        stepIndex--;
                        if (stepIndex < 0)
                        {
                            stepIndex = 1;
                            moveForward = true;
                        }
                    }


                }
            }
            else
            {
                CurrentTarget = null;
            }
        }
    }

    public virtual void TraitUpdater()
    {
        
    }

    private void DamageManager() {
        if(health <= 0) {
            
            GetKill();
        }
    }

    public void GetHurt(float damage, int id, int origin)
    {
        if (!isDead)
        {
            if (Time.time > nextHurt)
            {
                if(GetComponent<EntityFactions>().FactionID != origin)
                {
                    health -= damage;
                    HurtEvents.current.HurtBegin(id);
                    nextHurt = Time.time + hurtRate;
                    
                }
            }
        }
    }

    public void DamageCaculate(float damage) {
        health = health - damage;
    }

    public virtual void GetKill()
    {
        Debug.Log(gameObject.name + " killed");
        isDead = true;
    }

    

    public void TransitionToState(EnemyBaseState state) {
        currentState = state;
        currentState.EnterState(this);
        Debug.Log(gameObject.name + " Current State is" + state);
    }

    public void RefreshState()
    {
        currentState = PatrolState;
        currentState.EnterState(this);
    }

    IEnumerator PatrolBreakBehaviour()
    {
        float breaktime = 0f;
        CurrentTarget = null;
        BreakBehaviour();
        while (breaktime < patrolBreakTime)
        {
            var Dir = (PatrolSteps[stepIndex].position - transform.position).normalized;
            transform.rotation = Quaternion.RotateTowards
                (
                transform.rotation,
                Quaternion.LookRotation(Dir, Vector3.up),
                Time.deltaTime * 50f
                );
            breaktime += Time.deltaTime;
            yield return null;
        }

        CurrentTarget = PatrolSteps[stepIndex];
        yield return null;

    }

    public virtual void BreakBehaviour()
    {
        Debug.Log(gameObject.name + ":" + "Have a break~");
    }

    public Vector3 E2TDirection()
    {
        if (CurrentTarget != null)
        {
            Vector3 Dir = CurrentTarget.position - transform.position;
            Vector3 E2TDir = new Vector3(Dir.x, 0f, Dir.z);
            return E2TDir;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public float E2TDistance()
    {
        if (CurrentTarget != null)
        {
            float E2TDis = Vector3.Distance(CurrentTarget.position, transform.position);
            return E2TDis;
        }
        else
        {
            return 0f;
        }
    }
}
