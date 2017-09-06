using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movement : MonoBehaviour {

    public GameObject markerForAiming;

    private player_sense playerSense;
    private Rigidbody rb;
    private bool isTargetOnPlace = true;
    private Vector3 targetPointToAttack;
    private Vector3 targetPoint;
    private NavMeshAgent navAgent;
    private float minDeltaDistanceToMove = 1f;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        playerSense = GetComponentInChildren<player_sense>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        #region movement

        if (!isTargetOnPlace)
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(targetPoint);
        }
        #endregion
        //print(transform.name + " : " + transform.position);

        #region attack

        IsEnemyVisibleToPlayer();

        if (playerSense.isEnemyInRange && IsEnemyVisibleToPlayer())
        {
            //print(66);
            Vector3 targetPointToAttack = playerSense.enemyPosition;
            transform.LookAt(targetPointToAttack);
        }
        #endregion
        markerForAiming.transform.position = playerSense.enemyPosition;
    }

    //the eyes of the player, can he see the enemy?
    private bool IsEnemyVisibleToPlayer()
    {
        Ray ray = new Ray(transform.position, playerSense.enemyPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            print(hit.transform.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RotateUnitToAimEnemy()
    {

    }

    public void moveSelectedObjectToPoint(Vector3 targetPoint)
    {
        //Decide to move or not - if object is far
        if (Vector3.Distance( transform.position, targetPoint) > minDeltaDistanceToMove)
        {
            this.targetPoint = targetPoint;

            isTargetOnPlace = false;
        }

    }

}