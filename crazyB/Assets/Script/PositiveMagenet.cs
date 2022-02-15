using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveMagenet : MonoBehaviour
{

    public float range;
    public float strength;
    List<MagnetizedObject> magnetizedObjects;

    // Start is called before the first frame update
    void Start()
    {
        magnetizedObjects = new List<MagnetizedObject>();
        gameObject.GetComponent<CircleCollider2D>().radius = range;
    }

    public void FixedUpdate()
    {
        foreach (MagnetizedObject v in magnetizedObjects)
        {
            ApplyMagneticForce(v);
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
        if (collider.gameObject.tag == "Negative" || collider.gameObject.tag == "Iron")
        {
            MagnetizedObject newMag = new MagnetizedObject();
            newMag.collider = collider;
            newMag.rb = collider.GetComponent<Rigidbody2D>();
            newMag.transform = collider.transform;
            newMag.magneticPole = 1;
            magnetizedObjects.Add(newMag);
            print("Negative");
        }
        else if (collider.gameObject.tag == "Positive")
        {
            MagnetizedObject newMag = new MagnetizedObject();
            newMag.collider = collider;
            newMag.rb = collider.GetComponent<Rigidbody2D>();
            newMag.transform = collider.transform;
            newMag.magneticPole = -1;
            magnetizedObjects.Add(newMag);
            print("Positive");
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Positive") || collider.CompareTag("Negative") || collider.CompareTag("Iron"))
        {
            for (int i = 0; i < magnetizedObjects.Count; i++)
            {
                if (magnetizedObjects[i].collider == collider)
                {
                    magnetizedObjects.RemoveAt(i);
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