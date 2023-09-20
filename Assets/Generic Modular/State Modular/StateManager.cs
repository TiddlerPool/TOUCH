using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager manager;
    public BossMusic bossMusic;
    public UIBossHealth UIBoss;
    public Enemy bossData;
    public State state;

    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
        }
    }

    private void Update()
    {
        if(bossData != null)
        {
            bossMusic.IsEncounter = true;
            UIBoss.SetBar(bossData);
            UIBoss.gameObject.SetActive(true);
        }
        else
        {
            bossMusic.IsEncounter = false;
            UIBoss.gameObject.SetActive(false);
        }
    }
}

public enum State
{
    Normal,
    Battle
}