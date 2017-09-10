using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class movement : MonoBehaviour {

    public Image selectionIndicator;
    internal bool isShooting = false;
    internal bool isSelected = false;

    private unit_sense unitSense;
    private Rigidbody rb;
    private bool isMoving = true;
    private Vector3 targetPointToAttack;
    private Vector3 targetPointToMove;
    private NavMeshAgent navAgent;
    private string enemyTagToAttack = "enemy";
    private Transform eyesTransform;
    private GameObject gun;

        float a;
    //animation
    private Animator animator;
    //private bool isMoving = false;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        unitSense = GetComponentInChildren<unit_sense>();
        eyesTransform = Custom.findChildWithTag(gameObject,"eyes").transform;
        gun = Custom.findChildWithTag(gameObject, "gun");
        animator = GetComponent<Animator>();
        targetPointToMove = transform.position;

    }

    private void Update()
    {
        #region attack


        if (unitSense.isEnemyInRange)
        {
            targetPointToAttack = unitSense.enemyPosition;

            //rotate the transform of the eyes to the target, so that a raycast can be fired in that direction later to check if the eyes can see the target
            eyesTransform.LookAt(targetPointToAttack);

            if (IsEnemyVisibleToPlayer())
            {
                //print("visible");
                RotateUnitToAimEnemy();
                Shoot();
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

        #region movement

        //if player is not on his place
        if (Vector3.Distance(transform.position, targetPointToMove) > global_const.deviationRadius + 1)
        {
            isMoving = true;
            //print("move player");
        }
        else
        {
            //print("not movinf player");

            isMoving = false;
        }


        if (isMoving)
        {
            navAgent.enabled = true;
            navAgent.SetDestination(targetPointToMove);
        }
        else
        {
            navAgent.enabled = false;
        }

        #endregion

        #region selection indicator

        if (isSelected)
        {
            selectionIndicator.enabled = true;
        }
        else
        {
            selectionIndicator.enabled = false;
        }
        #endregion

        Animation();

        //testinggggggggggggggggggggggggggggggg
        //print("Moving: " + isMoving);
        //print(CalcDeviationRadius());
        //print("Shooting: " + isShooting);
    }


    //shooting method
    private void Shoot()
    {
        isShooting = true;
        animator.SetBool("isShooting", isShooting);
    }
    //

    //moving methods
    private float CalcDeviationRadius()
    {
    //is the player near the target location in order to stop moving
        //if selected object is more than one
        if (!array_manager.IsArrayWithOneObj(selection_manager.selectedObjects) &&
            !array_manager.IsArrayEmpty((selection_manager.selectedObjects)))
        {
            //return the deviation radius, so that multiple players with one direction point won't hit eachother at the end
            return array_manager.objectsCountInArray(selection_manager.selectedObjects) * global_const.playerWidth;
        }

        return global_const.playerWidth;
    }

    internal void moveSelectedObjectToPoint(Vector3 targetPointToMove)
    {
        //Decide to move or not - if object is further then one unit distance, move him
        if (Vector3.Distance( transform.position, targetPointToMove) > global_const.playerWidth)
        {
            this.targetPointToMove = targetPointToMove;

            isMoving = true;

            global_const.deviationRadius = CalcDeviationRadius();
        }
    }
    //

    //shooting methods
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
        navAgent.updateRotation = true;
        transform.LookAt(new Vector3 (targetPointToAttack.x, transform.position.y, targetPointToAttack.z));
    }

    private void UnitForgetTheTarget()
    {
        isShooting = false;
        targetPointToAttack = Vector3.zero;
    }
    //

    //animation methods
    private void Animation() {

        //animation

        animator.SetBool("isMoving", isMoving);


        animator.SetBool("isShooting", isShooting);

    }


}