using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField]
    private int movementDirection = 1; // 1 is foward and -1 is reverse.
    [SerializeField]
    private int movingTo = 0; //Points to next node in PathSequence.
    [SerializeField]
    private Transform[] PathSequence; //Array of nodes to travel between.

    //Unity Method: draws lines between nodes to visualize path while testing: Remove at final. 
    public void OnDrawGizmos()
    {
        if (PathSequence == null || PathSequence.Length < 2)
        {
            return;
        }
        for (var i = 1; i < PathSequence.Length; i++)
        {
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);
        }
    }

    // Gets next point in PathSequence.
    public IEnumerator<Transform> GetNextPathPoint()
    {
        // stops in pathsequence is null or missing.  Should never happen.
        if (PathSequence == null || PathSequence.Length <= 1)
        {
            yield break;
        }
        while (true) //infinite loop is bad
        {
            yield return PathSequence[movingTo];

            // if statements currnetly turn player around to continue on same path.
            // will be used later to see which side of path player is starting.
            if (movingTo <= 0)
            {
                movementDirection = 1;
            }
            else if (movingTo >= PathSequence.Length - 1)
            {
                movementDirection = -1;
            }

            movingTo = movingTo + movementDirection; //grabs next node to move to.
        }
    }
}