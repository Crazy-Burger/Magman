using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeBlock : MonoBehaviour
{
    private float MagFieldRaidus;
    private float MaxMegnetForce;
    public GameData PlayerData;
    public GameObject Player;

    public GameObject[] positiveObjectList;
    public GameObject[] negativeObjectList;

    [SerializeField]
    private LineController linePrefab;
    private LineController newLine;

    private void Start()
    {
        MagFieldRaidus = PlayerData.OrangeMagFieldRaidus;
        
        this.MaxMegnetForce = PlayerData.MaxForce;
        this.Player = GameObject.FindWithTag("Player");
        this.positiveObjectList = GameObject.FindGameObjectsWithTag("PositiveMagnet");
        this.negativeObjectList = GameObject.FindGameObjectsWithTag("NegativeMagnet");

        // line render initialize
        LineController newLine = Instantiate(linePrefab);
        this.newLine = newLine;
        newLine.AssignTarget(transform, this.Player.transform);
        newLine.transform.parent = gameObject.transform;
    }

    private void FixedUpdate()
    {
        this.positiveObjectList = GameObject.FindGameObjectsWithTag("PositiveMagnet");
        this.negativeObjectList = GameObject.FindGameObjectsWithTag("NegativeMagnet");
        // check distance between player and the static object
        // float distance = this.calculateDist(this.Player);
        newLine.gameObject.SetActive(false);
        float distance = this.distToSphere(this.Player);
        float distInterpolates = Mathf.Min(distance / MagFieldRaidus, 1.0f);
        if (distance < MagFieldRaidus && Player.gameObject.GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Postitive)
        {
            newLine.gameObject.SetActive(true);
            SoundManager.PlaySound("magenetfield");
            Vector2 direction = Player.transform.position - transform.position;
            Player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * minSideLength(Player) * - (Mathf.Lerp(this.MaxMegnetForce, 0, distInterpolates)));
        }

        if (distance < MagFieldRaidus && Player.gameObject.GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Negative)
        {
            newLine.gameObject.SetActive(true);
            SoundManager.PlaySound("magenetfield");
            Vector2 direction = Player.transform.position - transform.position;
            Player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * minSideLength(Player) * (Mathf.Lerp(this.MaxMegnetForce, 0, distInterpolates)));
        }
        // check distance between positive dynamic objects and the static object
        for(int i=0; i < this.positiveObjectList.Length; i++){
            distance = this.distToSphere(this.positiveObjectList[i]);
            if (distance < MagFieldRaidus)
            {
                Vector2 direction = this.positiveObjectList[i].transform.position - transform.position;
                this.positiveObjectList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * minSideLength(this.positiveObjectList[i]) * - (Mathf.Lerp(this.MaxMegnetForce, 0, distInterpolates)));
            }
        }
        // check distance between negative dynamic objects and the static object
        for(int i=0; i < this.negativeObjectList.Length; i++){
            distance = this.distToSphere(this.negativeObjectList[i]);
            if (distance < MagFieldRaidus)
            {
                Vector2 direction = this.negativeObjectList[i].transform.position - transform.position;
                this.negativeObjectList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * minSideLength(this.negativeObjectList[i]) *  (Mathf.Lerp(this.MaxMegnetForce, 0, distInterpolates)));
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

    private float distToSphere(GameObject ob){
        float width = transform.localScale[0];
        float height = transform.localScale[1];
        float posX = transform.position[0];
        float posY = transform.position[1];
        float minX = posX - width/2;
        float maxX = posX + width/2;
        float minY = posY - height/2;
        float maxY = posY + height/2;
        float dx = Mathf.Max(minX - ob.transform.position[0], 0);
        dx = Mathf.Max(dx, ob.transform.position[0] - maxX);
        float dy = Mathf.Max(minY - ob.transform.position[1], 0);
        dy = Mathf.Max(dy, ob.transform.position[1] - maxY);
        
        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    private float minSideLength(GameObject ob)
    {
        float width = transform.localScale[0];
        float height = transform.localScale[1];
        return Mathf.Min(width, height);
    }
}
