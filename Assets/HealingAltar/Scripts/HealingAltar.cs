using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAltar : MonoBehaviour
{
    public List<string> itemLines = new();

    private void Awake()
    {
        itemLines.Add($"You feel a rejuvenating power flow through you!");
        gameObject.GetComponent<DialogueTrigger>().dialogue.lines = itemLines.ToArray();
    }

    public void HealPlayer()
    {
        StartCoroutine(WaitUntilPlayerDoneTalking());
    }

    IEnumerator WaitUntilPlayerDoneTalking()
    {
        do
        {
            yield return null;
        }
        while (Player_Movement.Instance.getIsInteracting());
        Debug.Log("Healing Player at Altar");
        Player_Movement.Instance.GetComponent<Character>().fullHeal();
        SoundManager.Instance.PlaySFX("HealSFX");
    }
}
