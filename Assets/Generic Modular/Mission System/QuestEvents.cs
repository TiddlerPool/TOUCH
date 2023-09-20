using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public static QuestEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action<int, int> onQuestUpdate;

    public void QuestUpdate(int id, int value)
    {
        if(onQuestUpdate != null)
        {
            onQuestUpdate(id, value);
        }
    }

    public event Action<int> onQuestAccept;

    public void QuestAccept(int id)
    {
        if(onQuestAccept != null)
        {
            onQuestAccept(id);
        }
    }
}
