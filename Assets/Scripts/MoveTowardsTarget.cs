using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveTowardsTarget : MonoBehaviour, Intention
{
    public Transform target;
    public Vector3 targetDistance;


    public float GetAxis(string axis)
    {
        var dist = 0f;

        switch(axis)
        {
            case "Horizontal":
                dist = target.position.x - transform.position.x;
                if (MathF.Abs(dist) < targetDistance.x)
                {
                    return 0;
                }
                else
                {
                    return Mathf.Clamp(dist, -1, 1);
                }
            case "Vertical":
                dist = target.position.y - transform.position.y;
                if(MathF.Abs(dist) < targetDistance.y)
                {
                    return 0;
                }
                else
                {
                    return Mathf.Clamp(dist, -1, 1);
                }
            default:
                throw new ArgumentException("axis must be \"Horizontal\" or \"Vertical\"", axis);

        }


    }
}
