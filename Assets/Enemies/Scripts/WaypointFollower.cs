using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public WaypointManager waypointManager; //this follower must have an attached waypoint manager game object (with edge collider 2D) to manage its path.
    public float speed = 1.0f;
    public float nodeCloseness = 0.01f; //how close to a node is 'close enough', set above zero to avoid math gremlins
    public float delayTime = 1.0f; //how long to wait at a node
    public float chaseCloseness = 1.0f; //how close to the player is 'close enough', should be smaller than hitbox if trying to collide with player

    [Range(1, 4)]
    public int behaviorType; //1 is continuous travel, 2 is travel with stops at each node, 3 is stop, and 4 is to chase the player (via EnemyController)

    public int travelDistance = 1; //number of nodes it will travel before stopping, can use this to create winding paths with only some stops
    public bool reverseDirection;
    public bool isAtNode;
    public bool reverseDirectionAtEnd; //false is a looping path, true is a back-and-forth path
    public Vector3 target;

    public Vector2 direction;
    Rigidbody2D rb2d;
    TravelBehavior travelBehavior;
    int nodesToTravel;
    float currentRemainingDelay;
    float currentRemainingChaseDelay;


    // Start is called before the first frame update
    void Start()
    {
        waypointManager = waypointManager.GetComponent<WaypointManager>();
        rb2d = GetComponent<Rigidbody2D>();
        target = transform.position;
        nodesToTravel = travelDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player_Movement.Instance.getIsInteracting())
        {
            rb2d.velocity = Vector3.zero;
            return;
        }
        switch (behaviorType)
        {
            case 1:
                travelBehavior = TravelNodes;
                break;
            case 2:
                travelBehavior = TravelNodesWithStop;
                break;
            case 3:
                travelBehavior = StopTravel;
                break;
            case 4:
                travelBehavior = ChasePlayer;
                break;
            default:
                travelBehavior = StopTravel;
                break;
        }
        if (target != null)
        {
            travelBehavior();
        }
    }

    bool CloseEnough(Vector2 node1, Vector2 node2, float tolerance)
    {
        if ((Mathf.Abs(node1.x - node2.x) >= tolerance
            || (Mathf.Abs(node1.y - node2.y) >= tolerance)))
        {
            return false;
        }
        return true;
    }

    bool TraverseNodes() //returns true when at a node, and false when en route to one
    {
        // while not at the target
        if (!CloseEnough(target, transform.position, nodeCloseness))
        {
            direction = (target - transform.position).normalized;
            rb2d.velocity = direction * speed;
            isAtNode = false;
            return false;
        }
        // while at the target
        rb2d.velocity = Vector2.zero;
        if (!reverseDirection)
        {
            if (reverseDirectionAtEnd && CloseEnough((Vector2)transform.position, waypointManager.GetLastWaypoint(), nodeCloseness))
            {
                target = waypointManager.GetPreviousWaypoint();
                isAtNode = true;
                reverseDirection = true;
                return true;
            }
            target = waypointManager.GetNextWaypoint();
            isAtNode = true;
            return true;
        }
        if (reverseDirectionAtEnd && CloseEnough((Vector2)transform.position, waypointManager.GetFirstWaypoint(), nodeCloseness))
        {
            target = waypointManager.GetNextWaypoint();
            isAtNode = true;
            reverseDirection = false;
            return true;
        }
        target = waypointManager.GetPreviousWaypoint();
        isAtNode = true;
        return true;
    }

    bool WaitForDelay()
    {
        if (currentRemainingDelay >= 0.0f && isAtNode)
        {
            currentRemainingDelay -= Time.deltaTime;
            return true;
        }
        currentRemainingDelay = delayTime;
        return false;

    }

    public bool isWaiting()
    {
        if (currentRemainingDelay < delayTime)
        {
            return true;
        }
        return false;
    }

    public delegate void TravelBehavior();

    void TravelNodes()
    {
        if (WaitForDelay())
        {
            return;
        }
        TraverseNodes();
    }

    void TravelNodesWithStop()
    {
        if (WaitForDelay())
        {
            return;
        }
        if (nodesToTravel == 0)
        {
            nodesToTravel = travelDistance;
            behaviorType = 3;
            return;
        }
        if (TraverseNodes())
            nodesToTravel--;
    }

    void StopTravel()
    {
        rb2d.velocity = Vector2.zero;
    }

    void ChasePlayer()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;

        if (!CloseEnough(playerPos, transform.position, chaseCloseness))
        {
            direction = (playerPos - transform.position).normalized;
            rb2d.velocity = direction * speed;
            isAtNode = false;
            return;
        }
        
    }
}
