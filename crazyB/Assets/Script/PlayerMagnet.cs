using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    private float MagFieldRaidus;
    private float MaxMegnetForce;
    public GameData PlayerData;

    public GameObject[] positiveObjectList;
    public GameObject[] negativeObjectList;

    [SerializeField]
    private List<GameObject> positiveObjects;
    public List<LineController> allLines;
    [SerializeField]
    private LineController linePrefab;

    private void Awake()
    {
        allLines = new List<LineController>();
    }
    // Start is called before the first frame update
    void Start()
    {

        MagFieldRaidus = PlayerData.OrangeMagFieldRaidus;
        this.MaxMegnetForce = PlayerData.MaxForce;
        this.positiveObjectList = GameObject.FindGameObjectsWithTag("PositiveMagnet");
        this.negativeObjectList = GameObject.FindGameObjectsWithTag("NegativeMagnet");
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        this.positiveObjectList = GameObject.FindGameObjectsWithTag("PositiveMagnet");
        this.negativeObjectList = GameObject.FindGameObjectsWithTag("NegativeMagnet");
        float distance;
        bool isNormal = GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Normal;
        if (!isNormal)
        {
            bool isPositive = GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerState == PlayerController.PlayerStates.Postitive;
            int direct = isPositive ? 1 : -1;
            for (int i = 0; i < this.positiveObjectList.Length; i++)
            {
                distance = this.distToSphere(this.positiveObjectList[i]);
                if (distance < MagFieldRaidus)
                {
                    //LineController newLine = Instantiate(linePrefab);
                    //Debug.Log("Instantiate the prefab.");
                    //allLines.Add(newLine);
                    //newLine.AssignTarget(transform.position, this.positiveObjectList[i].transform);
                    Vector2 direction = this.positiveObjectList[i].transform.position - transform.position;
                    this.positiveObjectList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * minSideLength(this.positiveObjectList[i]) * (direct) * (Mathf.Lerp(0, this.MaxMegnetForce, distance)));
                }
            }
            // check distance between negative dynamic objects and the static object
            for (int i = 0; i < this.negativeObjectList.Length; i++)
            {
                distance = this.distToSphere(this.negativeObjectList[i]);
                if (distance < MagFieldRaidus)
                {
                    //LineController newLine = Instantiate(linePrefab);
                    //Debug.Log("!!!!!!!!!!!!!!");
                    //allLines.Add(newLine);
                    //newLine.AssignTarget(transform.position, this.positiveObjectList[i].transform);
                    Vector2 direction = this.negativeObjectList[i].transform.position - transform.position;
                    this.negativeObjectList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * minSideLength(this.negativeObjectList[i]) * (-direct) * (Mathf.Lerp(0, this.MaxMegnetForce, distance)));
                }
            }
        }

    }
    private float distToSphere(GameObject ob)
    {
        float width = transform.localScale[0];
        float height = transform.localScale[1];
        float posX = transform.position[0];
        float posY = transform.position[1];
        float minX = posX - width / 2;
        float maxX = posX + width / 2;
        float minY = posY - height / 2;
        float maxY = posY + height / 2;
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
