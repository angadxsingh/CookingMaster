using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(moveUp))
            movement.y += 1;
        if (Input.GetKey(moveDown))
            movement.y -= 1;
        if (Input.GetKey(moveLeft))
            movement.x -= 1;
        if (Input.GetKey(moveRight))
            movement.x += 1;

        movement = movement.normalized * speed;
        rb.velocity = movement;
    }
}
