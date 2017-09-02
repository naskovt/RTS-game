using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    //list path

    //speed
    public float speed = 1;

    private Vector3 targetPoint;
    private bool isTargetOnPlace = true;
    private Rigidbody rb;
    private Vector3 movementVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!isTargetOnPlace)
        {
            StartMovingToTarget();
        }
    }

    private void StartMovingToTarget()
    {
        movementVector = (targetPoint - transform.position).normalized * speed;

        if (Vector3.Distance(targetPoint, transform.position) > 1f )
        {
            transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));
            //preventing spinning on stand
            rb.angularVelocity = Vector3.zero;
        }


        rb.velocity = movementVector;
    }

    //overall goals
    //generating shortcut path till no collision is detected with static object
    // while (collision)
    //goAroundCollider(hit.obj);

    //goAroundCollider();
    //if collision

    //hit.obj.center check for which side of it the player is left/right

    //movePlayer();


    //Decide to move or not
    public void moveSelectedObjectToPoint(Vector3 targetPoint)
    {
        if (transform.position != targetPoint)
        {
            isTargetOnPlace = false;

            this.targetPoint = targetPoint;
        }
    }

}