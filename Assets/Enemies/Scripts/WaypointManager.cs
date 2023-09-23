using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    EdgeCollider2D ec2d;
    public Vector2[] waypoints;
    public int current;

    // Start is called before the first frame update
    void Start()
    {
        //waypoints = GetComponentsInChildren<Node>();
        ec2d = GetComponent<EdgeCollider2D>(); //an edge collider should be attached to this object and set as a trigger
        waypoints = ec2d.points;               //the path to travel is the vertexes of this edge collider
        current = 0;                           //will travel from last to first node as well, so don't connect the end to the beginning
    }

    public Vector2 GetFirstWaypoint()
    {
        return transform.TransformPoint(waypoints[0]);
    }

    public Vector2 GetLastWaypoint()
    {
        return transform.TransformPoint(waypoints[waypoints.Length - 1]);
    }

    public Vector2 GetNextWaypoint()
    {
        current++;
        if (current == waypoints.Length)
        {
            current = 0;
        }
        return transform.TransformPoint(waypoints[current]);
    }

    public Vector2 GetPreviousWaypoint()
    {
        current--;
        if (current < 0)
        {
            current = waypoints.Length - 1;
        }
        return transform.TransformPoint(waypoints[current]);
    }

}
