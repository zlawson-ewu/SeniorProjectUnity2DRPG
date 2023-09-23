using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpot : MonoBehaviour
{
    public GameObject targetLocation;
    public float xOffset;
    public float yOffset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Movement>() != null)
        {
            GameManager.Instance.TeleportPlayer((Vector2)targetLocation.transform.position + new Vector2(xOffset, yOffset));
        }
    }
}
