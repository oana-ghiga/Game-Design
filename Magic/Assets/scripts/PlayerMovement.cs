using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
       
        
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (body.velocity.y) <= 0f)
        {
            body.velocity = new Vector2(body.velocity.x, 5);
        }
    }
}