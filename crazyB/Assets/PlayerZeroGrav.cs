using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZeroGrav : MonoBehaviour
{
    private Rigidbody2D rb;

    public float zeroGravMoveForce = 0f;
    public float normalMoveForce = 0f;
    private float moveForce = 0f;
    public float jumpForce = 0f;
    public bool inZeroGravityZone = false;
    private float origGravityScale = 0f;
    public string zeroGravTag = "";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        origGravityScale = rb.gravityScale;
    }

  

    void FixedUpdate() 
    {
        float h = Input.GetAxisRaw("Horizontal") * moveForce;
        float v = inZeroGravityZone ? Input.GetAxisRaw("Vertical") * moveForce : 0f;
        rb.AddForce(new Vector2(h, v));
    }

  // Update is called once per frame
    void Update()
    {
        moveForce = inZeroGravityZone ? zeroGravMoveForce : normalMoveForce;
        rb.gravityScale = inZeroGravityZone ? 0f : origGravityScale;

        if(Input.GetKeyDown(KeyCode.Space)&& !inZeroGravityZone)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == zeroGravTag)
        {
            inZeroGravityZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == zeroGravTag)
        {
            inZeroGravityZone = false;
        }
    }
}
