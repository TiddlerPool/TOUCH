using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIBossHealth : MonoBehaviour
{
    public Image healthStripe;
    public TMP_Text bossName;

    public void SetBar<T>(T data) where T : Enemy
    {
        bossName.text = data.enemyName;
        healthStripe.fillAmount = data.Health / data.MaxHealth;
    }
}
