using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_movement : MonoBehaviour
{
    public GameObject mainPlayerBase;
    public GameObject aiDestination;

    private Vector3 targetDirection;
    private Vector3 verticalVector;
    private Vector3 rotateVector;
    private float minDistanceToMove = 3;
    private float timeLapsed;
    private NavMeshAgent navAgent;
    private Rigidbody rb;
    private unit_sense unitSense;

    public float movingSpeed = 1;
    public float rotateMovingSpeed = 1;
    public int dummInterval = 3;
    public int smartInterval = 3;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        unitSense = GetComponentInChildren<unit_sense>();
    }

    void Update()
    {
        //if target in range ---> go and attack it
        if (unitSense.isEnemyInRange)
        {
            targetDirection = unitSense.enemyPosition;
        }
        //else go and attack the main base
        else
        {
            targetDirection = mainPlayerBase.transform.position;
        }

        timeLapsed += Time.deltaTime;

        #region Move the enemy towards the player

        //is the enemy close to the player?
        if (Vector3.Distance(targetDirection,transform.position) > minDistanceToMove)
        {

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
                rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(targetDirection), rotateMovingSpeed * Time.deltaTime);
                navAgent.isStopped = true;
                rb.velocity = targetDirection.normalized * movingSpeed * Time.deltaTime;
                //print("dumm");
            }

        }


        #endregion


    }



}