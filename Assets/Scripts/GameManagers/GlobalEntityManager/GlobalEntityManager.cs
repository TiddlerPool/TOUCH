using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEntityManager : MonoBehaviour
{
    [SerializeField]private List<EntityData> entityRecords = new List<EntityData>();

    public int RegisterEntity(GameObject entityObject)
    {
        int id = entityRecords.Count;
        entityRecords.Add(new EntityData(id, entityObject));
        return id;
    }

    public void UnregisterEntity(GameObject entityObject)
    {
        int index = entityRecords.FindIndex(record => record.EntityObject == entityObject);
        if (index != -1)
        {
            entityRecords.RemoveAt(index);
        }
    }

    public int GetIDByEntity(GameObject obj)
    {
        EntityData record = entityRecords.Find(record => record.EntityObject == obj);
        return (record != null) ? record.ID : -1;
    }
}
