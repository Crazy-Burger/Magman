using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveMagenet : MonoBehaviour
{

    public float range = 5.0f;
    public float strength = 10.0f;
    private List<MagnetizedObject> postiveMagnetizedObjects;

    // Start is called before the first frame update
    void Awake()
    {
        postiveMagnetizedObjects = new List<MagnetizedObject>();
        gameObject.GetComponent<CircleCollider2D>().radius = range;
    }

    public void FixedUpdate()
    {
        if (postiveMagnetizedObjects.Count != 0)
        {
            foreach (MagnetizedObject v in postiveMagnetizedObjects)
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
        if (collider.gameObject.tag == "NegativeMagnet" || collider.gameObject.tag == "Negative" || collider.gameObject.tag == "Iron" ||
            (collider.tag == "Player" && collider.GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Negative)
            )
        {
            MagnetizedObject newMag = new MagnetizedObject();
            newMag.collider = collider;
            newMag.rb = collider.GetComponent<Rigidbody2D>();
            newMag.transform = collider.transform;
            newMag.magneticPole = 1;
            postiveMagnetizedObjects.Add(newMag);
        }
        else if (collider.gameObject.tag == "PositiveMagnet" || collider.gameObject.tag == "Positive" ||
            (collider.tag == "Player" && collider.GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Postitive)
            )
        {
            MagnetizedObject newMag = new MagnetizedObject();
            newMag.collider = collider;
            newMag.rb = collider.GetComponent<Rigidbody2D>();
            newMag.transform = collider.transform;
            newMag.magneticPole = -1;
            postiveMagnetizedObjects.Add(newMag);
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("PositiveMagnet") || collider.CompareTag("NegativeMagnet") || collider.CompareTag("Iron") || collider.CompareTag("Positive") || collider.CompareTag("Negative") ||
            (collider.tag == "Player" && collider.GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Postitive)
            ||(collider.tag == "Player" && collider.GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Negative)
            )
        {
            print("Positive magnet list:" + collider.tag);
            for (int i = 0; i < postiveMagnetizedObjects.Count; i++)
            {
                if (postiveMagnetizedObjects[i].collider == collider)
                {
                    postiveMagnetizedObjects.RemoveAt(i);
                    break;
                }
            }
        }
    }
}

//public class MagnetizedObject
//{
//    public Collider2D collider;
//    public Rigidbody2D rb;
//    public Transform transform;
//    public int magneticPole;
//}