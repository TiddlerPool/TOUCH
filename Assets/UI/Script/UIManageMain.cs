 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class UIManageMain : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pause;
    public GameObject death;
    public GameObject tutorialUI;
    public GameObject promptUI;
    
    public AudioSource btn;
    public AudioSource btn2;
    private bool isTuror;
    void Start()
    {
        isTuror = false;
        tutorialUI.SetActive(false);
        promptUI.SetActive(true);
        StartCoroutine(WaitPrompt());
        DataController.isDead = false;
    }
    IEnumerator WaitPrompt()
    {
        yield return new WaitForSeconds(5f);
        promptUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!DataController.isDead)
        {
            btn2.Play();
            if (!pause.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
                pause.SetActive(true); 
            }
            else if (pause.activeSelf)//double click
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                pause.SetActive(false);
            }
        }

        if (isTuror && Input.GetMouseButtonDown(0))
        {
            btn.Play();
            tutorialUI.SetActive(false);
            isTuror = !isTuror;
        }
    }
    public void Remake()
    {
        btn.Play();
        SceneManager.LoadScene("Start");
        Cursor.visible = false;
    }

    public void Tutorial()
    {
        if (!isTuror)
        {
            btn.Play();
            tutorialUI.SetActive(true);
            isTuror = !isTuror;
        }
    }
    public void Resume()
    {
        btn.Play();
        pause.SetActive(false);
        Time.timeScale = 1;
        //Cursor.visible = false;
    }
    public void Close()
    {
        btn.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中
#else
        Application.Quit();//否则在打包文件中
           
#endif
    }
}

