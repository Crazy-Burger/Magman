using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform target;


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.sortingOrder = 2;

    }

    public void AssignTarget(Vector3 startPosition, Transform newTarget)
    {
        lr.positionCount = 2;
        lr.SetPosition(0, startPosition);
        target = newTarget;
    }

    public void Update()
    {
        lr.SetPosition(1, target.position);
        
    }
}
