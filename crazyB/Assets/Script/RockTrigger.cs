using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public float moveSpeed = 1;
    public int triggerIndex = 0;
    GameObject rockObject;
    // Start is called before the first frame update
    void Start()
    {
        rockObject = GameObject.FindGameObjectWithTag("Rock");

    }

    // Update is called once per frame
    void Update()
    {
        if (triggerIndex == 1)
        {
            Vector3 finalPosition = new Vector2(8, 2);
            rockObject.transform.position = Vector3.MoveTowards(rockObject.transform.position, finalPosition, moveSpeed * Time.deltaTime);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerIndex = 1;
    }

}
