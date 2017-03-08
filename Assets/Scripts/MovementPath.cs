using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{

    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathSequence;

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
    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathSequence == null || PathSequence.Length < 1)
        {
            yield break;
        }
        while (true)
        {
            yield return PathSequence[movingTo];

            if (PathSequence.Length == 1)
            {
                continue;
            }
            if (movingTo <= 0)
            {
                movementDirection = 1;
            }
            else if (movingTo >= PathSequence.Length - 1)
            {
                movementDirection = -1;
            }

            movingTo = movingTo + movementDirection;
        }

    }
}