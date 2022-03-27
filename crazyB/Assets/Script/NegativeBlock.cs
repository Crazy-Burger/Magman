using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeBlock : MonoBehaviour
{
    private float MagFieldRaidus;
    public GameData PlayerData;
    public GameObject Player;

    public GameObject[] positiveObjectList;
    public GameObject[] negativeObjectList;
    public BoxCollider2D mycollider;

    private void Start()
    {
        MagFieldRaidus = PlayerData.OrangeMagFieldRaidus;
        this.Player = GameObject.FindWithTag("Player");
        this.positiveObjectList = GameObject.FindGameObjectsWithTag("PositiveMagnet");
        this.negativeObjectList = GameObject.FindGameObjectsWithTag("NegativeMagnet");
        this.mycollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Debug.Log(this.mycollider.size);
        // check distance between player and the static object
        float distance = this.calculateDist(this.Player);
        if (distance < MagFieldRaidus && Player.gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            Debug.Log(this.mycollider.size);
            Vector2 direction = Player.transform.position - transform.position;
            Player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * -(Mathf.Lerp(0, 100, distance)));
        }

        if (distance < MagFieldRaidus && Player.gameObject.GetComponent<Renderer>().material.color == Color.blue)
        {
            Vector2 direction = Player.transform.position - transform.position;
            Player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * (Mathf.Lerp(0, 100, distance)));
        }
        // check distance between positive dynamic objects and the static object
        for(int i=0; i < this.positiveObjectList.Length; i++){
            distance = this.calculateDist(this.positiveObjectList[i]);
            if (distance < MagFieldRaidus)
            {
                Vector2 direction = this.positiveObjectList[i].transform.position - transform.position;
                this.positiveObjectList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * -(Mathf.Lerp(0, 100, distance)));
            }
        }
        // check distance between negative dynamic objects and the static object
        for(int i=0; i < this.negativeObjectList.Length; i++){
            distance = this.calculateDist(this.negativeObjectList[i]);
            if (distance < MagFieldRaidus)
            {
                Vector2 direction = this.negativeObjectList[i].transform.position - transform.position;
                this.negativeObjectList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * (Mathf.Lerp(0, 100, distance)));
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MagFieldRaidus);
    }

    private float calculateDist(GameObject ob){
        return Vector2.Distance(transform.position, ob.transform.position);
    }
}
