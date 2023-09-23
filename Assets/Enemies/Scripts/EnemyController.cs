using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int originalBehavior;
    public float waitDelayAfterHit;

    WaypointFollower waypoints;
    Vector3 target;
    bool doneWaiting;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GetComponent<WaypointFollower>();
        doneWaiting = true;
    }
    
    void FixedUpdate()
    {
        if (waypoints == null)
        {
            return;
        }

        if (waypoints.behaviorType == 4) // Chase the player if set to do it
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                target = player.transform.position - transform.position;
            }
        }
        else //target is the next waypoint
        {
            target = waypoints.target - transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other) //player enters chase range, uses the trigger collider
    {
        Player_Movement player = other.gameObject.GetComponent<Player_Movement>();
        if (player != null && doneWaiting)
        {
            waypoints.behaviorType = 4;
        }
    }

    void OnTriggerStay2D(Collider2D other) //player still within chase range, uses the trigger collider
    {
        Player_Movement player = other.gameObject.GetComponent<Player_Movement>();
        if (player != null && doneWaiting)
        {
            waypoints.behaviorType = 4;
        }
    }

    void OnTriggerExit2D(Collider2D other) //go back to original behavior if player gone, uses the trigger collider
    {
        Player_Movement player = other.gameObject.GetComponent<Player_Movement>();
        if (player != null && doneWaiting)
        {
            waypoints.behaviorType = originalBehavior;
        }
    }

    void OnCollisionEnter2D(Collision2D other) //physical contact
    {
        Player_Movement player = other.gameObject.GetComponent<Player_Movement>();

        if (player != null)
        {
            StartCoroutine(WaitSeconds(waitDelayAfterHit)); //wait after hit to limit pushing and allow time for scene transition
            Debug.Log("Trigger Combat Here");
            SoundManager.Instance.PlaySFX("EncounterSFX");
            GameManager.Instance.StartCombat(gameObject);
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    IEnumerator WaitSeconds(float waitTime) //helper method to wait for seconds at a node
    {
        doneWaiting = false;
        waypoints.behaviorType = 3;
        yield return new WaitForSeconds(waitTime);
        waypoints.behaviorType = originalBehavior;
        doneWaiting = true;
    }
}
