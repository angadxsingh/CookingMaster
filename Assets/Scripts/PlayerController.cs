using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//First player basic movement script to test functionality 

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float X = Input.GetAxis("Horizontal");  
        float Y = Input.GetAxis("Vertical");    

        rb.velocity = new Vector2(X, Y) * speed;
    }
}
