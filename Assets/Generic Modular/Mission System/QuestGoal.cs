using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool GoalUpdate(int value)
    {
        if (goalType == GoalType.Kill)
            currentAmount += value;
        else if (goalType == GoalType.Collect)
            currentAmount += value;
        else
            return currentAmount >= requiredAmount;

        if (currentAmount >= requiredAmount)
            return true;
        else
            return false;
    }
}

public enum GoalType
{
    Kill,
    Collect,
    Arrive
}
