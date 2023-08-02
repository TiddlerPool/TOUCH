using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactions : MonoBehaviour
{
    public enum EntityFaction
    {
        Creations,
        Allies,
        Enemies,
    }


    public int FactionID;
    public string Faction;
    public EntityFaction faction;

    private void Awake()
    {
        switch ((int)faction)
        {
            case 0:
                Faction = "Creations";
                FactionID = (int)faction;
                break;
            case 1:
                Faction = "Allies";
                FactionID = (int)faction;
                break;
            case 2:
                Faction = "Enemies";
                FactionID = (int)faction;
                break;
        }

    }
}
