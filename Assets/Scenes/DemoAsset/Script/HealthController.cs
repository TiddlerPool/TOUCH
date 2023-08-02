/* HealthController.cs */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    //public AudioSource audioSouce;
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    
    [SerializeField]
    private GameObject healthPanel;
    
    [SerializeField]
    private RectTransform healthBar;

    private float healthBarStartWidth;

    //public MeshRenderer meshRenderer;
    public GameObject enemy;
    public GameObject ash;
    private bool isDead;

    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        ash.SetActive(false);
        currentHealth = maxHealth;
        healthBarStartWidth = healthBar.sizeDelta.x;
        UpdateUI();
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            //audioSouce.Play();
            //meshRenderer.enabled = false;
            ash.SetActive(true);
            enemy.SetActive(false);
            healthPanel.SetActive(false);

           // StartCoroutine(RespawnAfterTime());
        }

        UpdateUI();
    }
    /*
    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTime);
        ResetHealth();
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        meshRenderer.enabled = true;
        healthPanel.SetActive(true);
        UpdateUI();
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
    }
    private void Update()
    {
        
    }
    private void UpdateUI()
    {
        float percentOutOf = (currentHealth / maxHealth) * 100;
        float newWidth = (percentOutOf / 100) * healthBarStartWidth;

        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
        //healthText.text = currentHealth + "/" + maxHealth;
    }
}
