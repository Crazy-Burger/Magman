using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeBlock : MonoBehaviour
{
    private float MagFieldRaidus;
    public GameData PlayerData;
    public GameObject Player;

    private void Start()
    {
        MagFieldRaidus = PlayerData.OrangeMagFieldRaidus;
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance < MagFieldRaidus && Player.gameObject.GetComponent<Renderer>().material.color == Color.black)
        {
            Vector2 direction =  transform.position - Player.transform.position;
            Player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * -(Mathf.Lerp(0, 10, distance)));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MagFieldRaidus);
    }
}
