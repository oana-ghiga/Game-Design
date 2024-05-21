using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3 destination;
    private float checkTimer;
    

    private bool atacking;

    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (atacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                // checkTimer = 0;
                CheckForPlayer();
            }
        }
    }
    
    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if (hit.collider != null && !atacking)
            {
                atacking = true;
                destination = directions[i];
                checkTimer = 0;

            }
        }
    }
    
    private void CalculateDirections()
    {
        directions[0] = transform.right*range; //right
        directions[1] = -transform.right*range; //left
        directions[2] = transform.up*range; //up
        directions[3] = -transform.up*range; //down
        
    }

    private void Stop()
    {
        destination=transform.position;
        atacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       base.OnTriggerEnter2D(collision);
       Stop();
       
    }
}
