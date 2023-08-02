using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    //public static bool isDead;

    private Animator animator;
    public GameObject ui;
    public static int health;

    public GameObject healthUI1;
    public GameObject healthUI2;
    public GameObject healthUI3;

    public AudioSource audioSouce;
    void Start()
    {
        //health = 3;
        //isDead = false;

        healthUI1.SetActive(true);
        healthUI2.SetActive(true);
        healthUI3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            audioSouce.Play();
            animator = other.GetComponent<Animator>();
            health--;
            /*if(health ==2)
            {
                healthUI3.SetActive(false);
                animator.SetTrigger("Injury");
            }
            if (health == 1)
            {
                healthUI2.SetActive(false);
                animator.SetTrigger("Injury");
            }*/
            if (health ==0)
            {
                print("die");
                healthUI1.SetActive(false);

                //isDead = true;
                
                animator.SetTrigger("Die");
                ui.SetActive(true);
                Cursor.visible = true;
            }
        }   
    }
}