using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    public float range = 5.0f;
    public float strength = 10.0f;
    private List<MagnetizedObject> playerMagnetizedObjects;
    public GameObject player;
    public PlayerController playerController;
    public PlayerController.PlayerStates state;
    // Start is called before the first frame update
    void Awake()
    {
        playerMagnetizedObjects = new List<MagnetizedObject>();
        gameObject.GetComponent<CircleCollider2D>().radius = range;
        this.player = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerMagnetizedObjects.Count != 0)
        {
            foreach (MagnetizedObject v in playerMagnetizedObjects)
            {
                ApplyMagneticForce(v);
            }
        }
       

    }
    void ApplyMagneticForce(MagnetizedObject magnetizedObject)
    {
        Vector2 vector2 = transform.position - magnetizedObject.transform.position;
        float distance = vector2.magnitude;
        // float distanceScale = Mathf.Lerp(0f, strength, distance);
        float distanceScale = Mathf.InverseLerp(range, 0f, distance);
        float attractionStrength = Mathf.Lerp(0f, strength, distanceScale);
        magnetizedObject.rb.AddForce(vector2.normalized * attractionStrength * magnetizedObject.magneticPole, ForceMode2D.Force);

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        print("------------P1---------------");
        // If player is positive
        if (GameObject.Find("Player").GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Postitive)
        {
            
            // player attract negative
            if (collider.gameObject.tag == "NegativeMagnet" || collider.gameObject.tag == "Negative" || collider.gameObject.tag == "Iron")
            {
                MagnetizedObject newMag = new MagnetizedObject();
                newMag.collider = collider;
                newMag.rb = collider.GetComponent<Rigidbody2D>();
                newMag.transform = collider.transform;
                newMag.magneticPole = 1;
                playerMagnetizedObjects.Add(newMag);
                print(newMag.GetType());
            }
            // player repel negative
            else if (collider.gameObject.tag == "PositiveMagnet" || collider.gameObject.tag == "Positive")
            {
                MagnetizedObject newMag = new MagnetizedObject();
                newMag.collider = collider;
                newMag.rb = collider.GetComponent<Rigidbody2D>();
                newMag.transform = collider.transform;
                newMag.magneticPole = -1;
                playerMagnetizedObjects.Add(newMag);
            }
        }

        if (GameObject.Find("Player").GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Negative)
        {
            // player attract negative
            if (collider.gameObject.tag == "NegativeMagnet" || collider.gameObject.tag == "Negative" || collider.gameObject.tag == "Iron")
            {
                MagnetizedObject newMag = new MagnetizedObject();
                newMag.collider = collider;
                newMag.rb = collider.GetComponent<Rigidbody2D>();
                newMag.transform = collider.transform;
                newMag.magneticPole = -1;
                playerMagnetizedObjects.Add(newMag);
            }
            // player repel negative
            else if (collider.gameObject.tag == "PositiveMagnet" || collider.gameObject.tag == "Positive")
            {
                MagnetizedObject newMag = new MagnetizedObject();
                newMag.collider = collider;
                newMag.rb = collider.GetComponent<Rigidbody2D>();
                newMag.transform = collider.transform;
                newMag.magneticPole = 1;
                playerMagnetizedObjects.Add(newMag);
            }
        }
        print("magnetizedObjects count: " + playerMagnetizedObjects.Count);
        

    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("PositiveMagnet") || collider.CompareTag("NegativeMagnet") || collider.CompareTag("Iron") || collider.CompareTag("Positive") || collider.CompareTag("Negative"))
        {
            print("Player magnet list:" + collider.tag);
            for (int i = 0; i < playerMagnetizedObjects.Count; i++)
            {
                if (playerMagnetizedObjects[i].collider == collider)
                {
                    playerMagnetizedObjects.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
