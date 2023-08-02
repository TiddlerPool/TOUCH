using UnityEngine;


    [System.Serializable]
    public class EntityData
    {
        public int ID { get; private set; }
        public GameObject EntityObject { get; private set; }

        public EntityData(int id, GameObject entityObject)
        {
            ID = id;
            EntityObject = entityObject;
        }
    }
