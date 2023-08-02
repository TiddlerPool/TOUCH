using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManage : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
        Cursor.visible= true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AboutDisplay()
    {
        
    }
    
    public void StartGame()
    {
        //audioSource.clip = audioClip1;
        audioSource.Play();
        StartCoroutine(WaitAndPrint());
    }
    public void About()
    {
        //audioSource.clip = audioClip1;
        audioSource.Play();
        SceneManager.LoadScene("About");
    }
    public void Close()
    {
        audioSource.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
           
#endif
    }
    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(2f);
        print("开始");
        SceneManager.LoadScene("Load");
    }
}
