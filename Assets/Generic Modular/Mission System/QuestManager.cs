using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestManager instance;

    public GameObject stripePrefab;
    public Transform questList;
    public Quest[] library;
    [SerializeField]
    private MissionStripe[] stripes;
    public List<MissionStripe> acceptedMissions;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void DestroyOnQuit()
    {
        var manager = FindObjectOfType<QuestManager>();
        if (manager.instance != null)
        {
            Destroy(manager.instance.gameObject);
            manager.instance = null;
        }
    }

    private void Start()
    {
        SetUpQuestPool();
        QuestEvents.current.onQuestAccept += AcceptNewMission;
    }

    private void SetUpQuestPool()
    {
        stripes = new MissionStripe[library.Length];
        for(int i = 0; i < library.Length; i++)
        {
            var stripe = Instantiate(stripePrefab, questList);
            var data = stripe.GetComponent<MissionStripe>();
            data.quest = library[i];
            data.Initiate();
            stripes[i] = data;
            stripe.SetActive(false);
        }
    }

    public void AcceptNewMission(int id)
    {
        if(id > stripes.Length -1)
        {
            Debug.Log("ID is out of quest librarie's index.");
            return;
        }

        stripes[id].AcceptMission(id);
        RefreshList();
    }

    public void RefreshList()
    {
        for(int i = 0;i < stripes.Length; i++)
        {
            if (stripes[i].quest.isActive)
            {
                stripes[i].gameObject.SetActive(true);
            }
        }
    }
}
