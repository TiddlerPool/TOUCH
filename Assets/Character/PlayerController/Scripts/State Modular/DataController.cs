using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class DataController : MonoBehaviour , IDamageable
{
    public MonoBehaviour script;
    //private GlobalEntityManager entityManager;
    
    [SerializeField]
    private GameObject hurt;
    
    [SerializeField]
    private GameObject healthPanel;
    
    [SerializeField]
    private GameObject sanBar;
    
    public Image fuel;
    
    [SerializeField]
    private RectTransform healthBar;

    private RectMask2D healthBarMask;
    private RectMask2D sanBarMask;

    private float healthBarStartWidth;

    [SerializeField]
    private float damageValue = 0f;
    public float DamageValue
    {
        get { return damageValue; }
        set { damageValue = value; }
    }

    public float hurtRate;
    private float nextHurt;

    private bool isHurt;
    public bool IsHurt {
        get { return isHurt; }
        set { isHurt = value; }
    }

    public static bool isDead;
    public static bool isWin;
    public GameObject DeathUI;
    public GameObject WinUI;
    
    public AudioSource win;
    public AudioSource dead;
    void Start()
    {
        isDead = false;
        isWin = false;
        Character.health = Character.maxHealth;
        healthBarStartWidth = healthBar.sizeDelta.x;
        healthBarMask = healthBar.GetComponent<RectMask2D>();
        sanBarMask = sanBar.GetComponent<RectMask2D>();
        script.enabled = true;
        DeathUI.SetActive(false);
        WinUI.SetActive(false);
        UpdateUI();
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ApplyDamage(2);
            ApplySan(10);
            ApplyFuel(10);
        }
        
        if (isDead)
        {
            Death();
            return;
        }

        if (isWin) {
            script.enabled = false;
            isDead = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            WinUI.SetActive(true);
        }

        if(Time.time > nextHurt)
        {
            isHurt = false;
            nextHurt = Time.time + hurtRate;
        }

        FuelCheck();
    }


    public void ApplyHeal(float heal)
    {
        if (isDead) return;

        Character.health += heal;

        if (Character.health <= 0)
        {
            Character.health = 0;
            isDead = true;
            //audioSouce.Play();
            healthPanel.SetActive(false);
        }
        UpdateUI();
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;

        Instantiate(hurt, transform.position, Quaternion.identity);
        Character.health -= damage;

        if (Character.health <= 0)
        {
            Character.health = 0;
            isDead = true;
            //audioSouce.Play();
            //meshRenderer.enabled = false;
            healthPanel.SetActive(false);
        }
        UpdateUI();
    }
    public void ApplySan(float attack)
    {
        if (isDead) return;

        Character.san -= attack;

        if (Character.san <= 0)//san值为0
        {

        }
        UpdateUI();
    }
    public void ApplyFuel(float attack)
    {
        if (isDead) return;

        Character.fuel -= attack;

        if (Character.fuel <= 0)//san值为0
        {

        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        float percentOutOf = (Character.health / Character.maxHealth) * 100;
        float percentOutOf2 = 1-Character.san/100;
        float percentOutOf3 = (Character.fuel / Character.maxFuel);
        //print(Character.health);
        //print(Character.san);
        //print(Character.fuel);
        float newWidth = (1- percentOutOf / 100) * healthBarStartWidth * 0.73f;
        
        healthBarMask.padding = new Vector4(0,0,newWidth,0);
        
        sanBarMask.padding = new Vector4(0, 70 * percentOutOf2, 0, 0);


        fuel.material.SetFloat("_Clip", percentOutOf3);
        //print(fuel.fillAmount);
        //healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
        //healthText.text = currentHealth + "/" + maxHealth;
    }

    public void Death()
    {
        if (isDead == true)
        {
            dead.Play();
            DeathUI.SetActive(true);
            script.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void GetHurt(float DamageValue, int id, int origin) {
        if(!isHurt)
        {
            if(GetComponent<EntityFactions>().FactionID != origin)
            {
                ApplyDamage(DamageValue);
                HurtEvents.current.HurtBegin(id);
                isHurt = true;
            }
        }
        
    }

    public void FuelCheck()
    {
        if (Character.fuel > 0f)
        {
            GetComponent<ThirdPersonController>().FuelNormal = true;
        }
        else
        {
            GetComponent<ThirdPersonController>().FuelNormal = false;
        }
    }

}
