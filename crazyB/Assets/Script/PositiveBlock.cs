using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveBlock : MonoBehaviour
{
    private float MagFieldRaidus;
    public GameData PlayerData;
    public GameObject Player;

    private void Start()
    {
        MagFieldRaidus = PlayerData.OrangeMagFieldRaidus;
        this.Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance < MagFieldRaidus && Player.gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            Vector2 direction = Player.transform.position - transform.position;
            Player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * (Mathf.Lerp(0, 10, distance)));
        }

        if (distance < MagFieldRaidus && Player.gameObject.GetComponent<Renderer>().material.color == Color.blue)
        {
            Vector2 direction = Player.transform.position - transform.position;
            Player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * -(Mathf.Lerp(0, 10, distance)));
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MagFieldRaidus);
    }
}
