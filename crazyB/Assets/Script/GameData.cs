using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class GameData : ScriptableObject
{
    public float OrangeMagFieldRaidus = 3;
    public float gravity = 3f;
    public float MaxForce = 100f;
}
