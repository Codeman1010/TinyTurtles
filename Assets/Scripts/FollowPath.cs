using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovementPath MyPath;
    public float speed = 1;
    public float maxDistanceToGoal = .1f;

    private IEnumerator<Transform> pointInPath;

	// Use this for initialization
	void Start ()
    {
        InitiatePath();
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();	
	}

    void InitiatePath()
    {
        if (MyPath == null)
        {
            Debug.LogError("Movement Path can not be null", gameObject);
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
