using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform target;
    private Transform start;


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.sortingOrder = 2;

    }

    public void AssignTarget(Transform magnetTransform, Transform newTarget)
    {
        lr.positionCount = 2;
        //lr.SetPosition(0, startPosition);
        target = newTarget;
        start = magnetTransform;
    }

    public void Update()
    {
        lr.SetPosition(0, start.position);
        lr.SetPosition(1, target.position);
        
    }
}
