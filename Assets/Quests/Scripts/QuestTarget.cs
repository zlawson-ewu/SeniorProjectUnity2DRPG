using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    public QuestGiver questGiver;
    public bool IsComplete;
    void Start()
    {
        IsComplete = false;
    }

    public void QuestTargetCompleted()
    {
        IsComplete = true;
        questGiver.CompleteObjective(this);
    }
}
