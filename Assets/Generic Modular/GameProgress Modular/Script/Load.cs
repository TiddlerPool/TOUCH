using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Load : MonoBehaviour
{
    //public Text loadingText;
    public TextMeshProUGUI loadingText;
    public Image progressBar;
    private float curProgressValue;
    private float precent;
    void Start()
    {
        //precent = 1;
        curProgressValue = 0;
        print("加载");
    }
    void FixedUpdate()
    {
        int progressValue = 100;

        if (curProgressValue < progressValue)
        {
            curProgressValue++;
        }

        loadingText.text = curProgressValue + "%";//实时更新进度百分比的文本显示
        //precent = (1-curProgressValue/100);
        progressBar.color = new Color(0, 0, 0, curProgressValue / 100*curProgressValue / 100*curProgressValue / 100 * curProgressValue / 100);
        //progressBar.fillAmount = curProgressValue / 100f;//实时更新滑动进度图片的fillAmount值  

        if (curProgressValue == 100)
        {
            loadingText.text = "OK";//文本显示完成OK
            SceneManager.LoadScene("Introduction");
        }
    }

    
}
