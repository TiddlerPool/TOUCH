using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBossManager : MonoBehaviour
{
    public UIBossHealth UIBoss;
    public BossMusic bossMusic;
    private void Awake()
    {
        UIBossHealthEvent.current.onEncounterBoss += Encounter;
        UIBossHealthEvent.current.onDiscounterBoss += Discounter;
    }

    public void Encounter(Enemy data)
    {
        UIBoss.gameObject.SetActive(true);
        UIBoss.SetBar(data);
    }

    public void Discounter()
    {
        UIBoss.gameObject.SetActive(false);
    }
}
