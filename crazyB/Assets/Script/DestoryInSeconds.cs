using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryInSeconds : MonoBehaviour
{
    [SerializeField] private float secondsToDestory = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, secondsToDestory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
