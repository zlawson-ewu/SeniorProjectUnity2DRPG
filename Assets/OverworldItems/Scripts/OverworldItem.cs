using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldItem : MonoBehaviour
{
    public Item item;
    public List<string> itemLines = new();
    public bool IsPickedUp;

    private void Awake()
    {
        itemLines.Add($"You received {item.getIdentifier()}!");
        gameObject.GetComponent<DialogueTrigger>().dialogue.lines = itemLines.ToArray();
    }

    public void GivePlayerItemOnInteract()
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
        item.addAmount(1);
        IsPickedUp = true;
        gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = !IsPickedUp;
        gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = !IsPickedUp;
        gameObject.GetComponent<CircleCollider2D>().enabled = !IsPickedUp;
    }
}
