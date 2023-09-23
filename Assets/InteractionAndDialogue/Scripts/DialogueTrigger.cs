using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;

    DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager.Instance;
    }

    public void Interact()
    {
        dialogueManager.StartDialogue(dialogue);
        if (GetComponent<QuestGiver>() != null)
        {
            GetComponent<QuestGiver>().QuestGiverRewardPlayer();
        }
        if (GetComponent<OverworldItem>() != null)
        {
            GetComponent<OverworldItem>().GivePlayerItemOnInteract();
        }
        if (GetComponent<HealingAltar>() != null)
        {
            GetComponent<HealingAltar>().HealPlayer();
        }
        dialogue.playerHasRead = true;
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
    }
}
