using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private MovementPath MyPath; //Path to follow containing MovementPath script.
    [SerializeField]
    private float speed = 1; // adjusted by how far player must travel in turn.
    [SerializeField]
    private float maxDistanceToGoal = .1f; //How close to path node to get before setting new path node
    [SerializeField]
    private IEnumerator<Transform> pointInPath;

	// Use this for initialization
	void Start ()
    {
        InitiatePath();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovePlayer();	
	}

    void InitiatePath()
    {
        if (MyPath == null)
        {
            Debug.LogError("Path can not be null", gameObject);
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if (pointInPath.Current == null)
        {
            Debug.LogError("No path", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    void MovePlayer()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
        {
            pointInPath.MoveNext();
        } 
    }
}