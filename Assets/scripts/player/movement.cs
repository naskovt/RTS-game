using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movement : MonoBehaviour {

    public GameObject markerForAiming;
    internal bool isShooting = false;

    private unit_sense unitSense;
    private Rigidbody rb;
    private bool isTargetOnPlace = true;
    private Vector3 targetPointToAttack;
    private Vector3 targetPointToMove;
    private NavMeshAgent navAgent;
    private float minDeltaDistanceToMove = 1f;
    private string enemyTagToAttack = "enemy";
    private Transform eyesTransform;
    private GameObject gun;
    private float unitWidth = 2;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        unitSense = GetComponentInChildren<unit_sense>();
        eyesTransform = Custom.findChildWithTag(gameObject,"eyes").transform;
        gun = Custom.findChildWithTag(gameObject, "gun");
    }

    private void Update()
    {
        if (true)
        {

        #region attack


            if (unitSense.isEnemyInRange)
        {
            targetPointToAttack = unitSense.enemyPosition;

            //rotate the transform of the eyes to the target, so that a raycast can be fired in that direction later to check if the eyes can see the target
            eyesTransform.LookAt(targetPointToAttack);

            if (IsEnemyVisibleToPlayer())
            {
                RotateUnitToAimEnemy();
                isShooting = true;
            }
            else
            {
                UnitForgetTheTarget();
            }
        }
        else
        {
            UnitForgetTheTarget();

            navAgent.updateRotation = true;
        }
        #endregion

        }

        #region movement
        //if player is not on his place

        if (!isTargetOnPlace)
        {
            navAgent.enabled = true;
            navAgent.SetDestination(targetPointToMove);
        }
        else
        {
            navAgent.enabled = false;
        }

        #endregion
    }

    private void deviationDestination()
    {
        //if selected object is more than one
        if (!array_manager.IsArrayWithOneObj(selection_manager.selectedObjects))
        {

        }
    }

    //the eyes of the player, can he see the enemy?
    private bool IsEnemyVisibleToPlayer()
    {
        Ray ray = new Ray(eyesTransform.position, eyesTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f) && hit.transform.tag == enemyTagToAttack)
        {
            //markerForAiming.transform.position = hit.transform.position;
            //print(hit.transform.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RotateUnitToAimEnemy()
    {
        navAgent.updateRotation = false;
        transform.LookAt(new Vector3 (targetPointToAttack.x, transform.position.y, targetPointToAttack.z));
    }


    internal void moveSelectedObjectToPoint(Vector3 targetPointToMove)
    {
        //Decide to move or not - if object is far
        if (Vector3.Distance( transform.position, targetPointToMove) > minDeltaDistanceToMove)
        {

            this.targetPointToMove = targetPointToMove;

            isTargetOnPlace = false;
        }

    }

    private void UnitForgetTheTarget()
    {
        isShooting = false;
        targetPointToAttack = Vector3.zero;
    }

}