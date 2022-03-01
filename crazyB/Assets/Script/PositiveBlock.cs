using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveBlock : MonoBehaviour
{
    private float MagFieldRaidus;
    public GameData PlayerData;


    private void Start()
    {
        MagFieldRaidus = PlayerData.OrangeMagFieldRaidus;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MagFieldRaidus);
    }
}
