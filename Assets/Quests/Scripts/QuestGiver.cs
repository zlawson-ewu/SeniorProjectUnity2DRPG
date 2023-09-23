using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Dialogue DialogueOnComplete;
    public Dialogue DialogueAfterReward;
    public List<QuestTarget> targets;
    public bool questCompleted;
    public bool playerRewarded;
    public int objectivesCompleted;
    public int experienceReward;
    public int goldReward;

    private void Start()
    {
        questCompleted = false;
        objectivesCompleted = 0;
    }
    public void CompleteObjective(QuestTarget target)
    {
        if (targets.Contains(target))
        {
            objectivesCompleted++;
        }
        CompleteQuestIfObjectivesDone();
    }

    public void CompleteQuestIfObjectivesDone()
    {
        if (objectivesCompleted == targets.Count)
        {
            questCompleted = true;
            QuestComplete();
        }
    }

    public void QuestComplete()
    { 
        DialogueOnComplete.lines[DialogueOnComplete.lines.Length - 1] = $"<You recieved {goldReward} gold.>";
        gameObject.GetComponent<DialogueTrigger>().SetDialogue(DialogueOnComplete);
    }

    public void QuestGiverRewardPlayer()
    {
        if (questCompleted && !playerRewarded && !GetComponent<DialogueTrigger>().dialogue.playerHasRead)
        {
            StartCoroutine(WaitUntilPlayerDoneTalking());
        }
    }

    IEnumerator WaitUntilPlayerDoneTalking()
    {
        do
        {
            yield return null;
        }
        while (Player_Movement.Instance.getIsInteracting());
        Character playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        FindObjectOfType<Gold>().addAmount(goldReward);
        playerCharacter.experiencePoints += experienceReward;
        playerCharacter.exp.addValue(experienceReward);
        if (LevelUpMenu.Instance.hasLeveled())
        {
            LevelUpMenu.Instance.levelUp();
        }
        GetComponent<DialogueTrigger>().dialogue = DialogueAfterReward;
        playerRewarded = true;
    }
}
