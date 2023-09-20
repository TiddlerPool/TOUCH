using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int id;
    public int nextQuestID = -1;
    public bool isActive;
    public bool isCompleted;

    public string title;
    public string description;

    public QuestGoal goal;

    public string UpdateDescription()
    {
        string des;
        if(goal.goalType != GoalType.Arrive)
        {
            des = string.Format("{0}/{1} " + description, goal.currentAmount, goal.requiredAmount);
        }
        else
        {
            des = description;
        }

        return des;
    }
}
