using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie_movement : MonoBehaviour {

    //assign from editor components
    public GameObject mainPlayerBase;
    public GameObject markerForDestinationTest;

    //changable from editor
    public float movingSpeed = 1;
    public float rotateMovingSpeed = 1;
    public int dummInterval = 3;
    public int smartInterval = 3;

    //get components
    private Animator animator;
    private NavMeshAgent navAgent;
    private Rigidbody rb;
    private unit_sense enemySense;

    //working varables
    private bool isMoving;
    private Vector3 targetDirection;
    private Vector3 verticalVector;
    private Vector3 rotateVector;
    private float minDistanceToMove = 1;
    private float timeLapsed;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemySense = GetComponentInChildren<unit_sense>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if target in range ---> go and attack it
        if (enemySense.isEnemyInRange)
        {
            targetDirection = enemySense.enemyPosition;
        }
        //else go and attack the main base
        else
        {
            targetDirection = mainPlayerBase.transform.position;
        }

        ////-----------------------------------test------------------------------------------------//

        timeLapsed += Time.deltaTime;


        #region Move the enemy towards the player
        //is the enemy close to the target? if not, than go to it
        if (Vector3.Distance(targetDirection, transform.position) > minDistanceToMove)
        {
            //the variable, that the animator uses
            isMoving = true;

            //move enemy smart after dummInterval finish
            if (dummInterval < timeLapsed)
            {
                navAgent.isStopped = false;
                navAgent.SetDestination(targetDirection);
                if (timeLapsed > smartInterval + dummInterval)
                {
                    timeLapsed = 0;
                }
                //print("smart");
            }
            else
            {
                //move enemy dumm
                navAgent.isStopped = true;
                rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(targetDirection), rotateMovingSpeed * Time.deltaTime);
                rb.velocity = targetDirection.normalized * movingSpeed * Time.deltaTime;
                //print("dumm");
            }

        }
        else
        {
            isMoving = false;
        }

        #endregion

        Animate();

    }

    private void Animate()
    {
        animator.SetBool("Moving", isMoving);
        //print(isMoving);
    }



}