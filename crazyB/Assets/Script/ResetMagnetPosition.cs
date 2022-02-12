using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMagnetPosition : MonoBehaviour
{
    private GameObject[] positiveMagnets;
    private GameObject[] negativeMagnets;
    private Vector3[] positiveInitialPositions;
    private Vector3[] negativeInitialPositions;
    // Start is called before the first frame update
    void Start()
    {
        positiveMagnets = GameObject.FindGameObjectsWithTag("PositiveDynamic");
        negativeMagnets = GameObject.FindGameObjectsWithTag("NegativeDynamic");
        for(int i = 0; i < positiveMagnets.Length; i++)
        {
            positiveInitialPositions[i] = positiveMagnets[i].transform.position;
        }
        for (int i = 0; i < negativeMagnets.Length; i++)
        {
            negativeInitialPositions[i] = negativeMagnets[i].transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("R"))
        {
            ResetMagnetPositions();
        }
    }

    void ResetMagnetPositions()
    {
        for(int i = 0; i < positiveMagnets.Length; i++)
        {
            positiveMagnets[i].transform.position = positiveInitialPositions[i];
        }
        for (int i = 0; i < negativeMagnets.Length; i++)
        {
            negativeMagnets[i].transform.position = positiveInitialPositions[i];
        }
    }
}
