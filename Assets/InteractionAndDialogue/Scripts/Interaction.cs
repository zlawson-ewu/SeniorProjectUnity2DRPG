using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] UnityEvent interactAction;

    private bool isInRange;

    Player_Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
        playerMovement = Player_Movement.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.getIsInteracting())
        {
            return;
        }

        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactAction.Invoke();
                playerMovement.setIsInteracting(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player in range of interactable");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player out of range of interactable");
        }
    }
}
