using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MissionStripe : MonoBehaviour
{
    public int id;
    public GameObject checkMark;
    public TMP_Text title;
    public TMP_Text description;

    public Quest quest;

    public MissionStripe(Quest quest)
    {
        this.quest = quest;
        id = quest.id;
        title.text = quest.title;
        description.text = quest.UpdateDescription();
    }

    private void Awake()
    {
        QuestEvents.current.onQuestUpdate += UpdateMission;
        //Initiate();
    }

    public void Initiate()
    {
        id = quest.id;
        title.text = quest.title;
        description.text = quest.UpdateDescription();
        checkMark = GetComponentInChildren<MissionCheckMark>().gameObject;
        checkMark.SetActive(false);
    }

    public void AcceptMission(int id)
    {
        if (id != this.id) {
            Debug.Log("Unmatched Quest ID.");
            return;
        }
        else
        {
            if (quest.isActive || quest.isCompleted)
            {
                Debug.Log("ID:" + id +" Is Already Accepted or Completed.");
                return;
            }
        }

        quest.isActive = true;
    }

    public void CompleteMission(int id)
    {
        if(id != this.id) { return; }

        quest.isCompleted = true;
        quest.isActive = false;
        checkMark.SetActive(true);
        Invoke("HideMission", 1.5f);
        Reward();
    }

    private void NextQuest()
    {
        if(quest.nextQuestID <= -1) { return; }

        FindObjectOfType<QuestManager>().AcceptNewMission(quest.nextQuestID);
    }

    private void Reward()
    {
        // Reward Something...
    }

    public void UpdateMission(int id, int value)
    {
        if (id != this.id) { return; }
        if (quest.goal.GoalUpdate(value))
            CompleteMission(id);
        description.text = quest.UpdateDescription();
    }

    public void HideMission()
    {
        NextQuest();
        gameObject.SetActive(false);
    }
}
