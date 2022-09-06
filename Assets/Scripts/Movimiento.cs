using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, 50);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-50, 0, 50);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(50, 0, 0);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -50);
        }
    }
}
