using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_sense : MonoBehaviour {

    //public GameObject marker;
    public string enemyTagToAttack;
    //public GameObject testObj;

    internal bool isEnemyInRange = false;
    internal Vector3 enemyPosition;

    private GameObject[] targetsInRange;
    private GameObject[] targetsInRangeTest;
    private Vector3 currentUnitPosition;

    void Start()
    {
        targetsInRangeTest = new GameObject[10];
    }

    void Update()
    {
        currentUnitPosition = transform.parent.position;

        //if enemies have entered the sight of the unit, check when they are gone = no elements in the array

        if (isEnemyInRange)
        {
            enemyPosition = ai_closestObj.ClosestEnemyPosition(currentUnitPosition, targetsInRangeTest);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTagToAttack)
        {
            //once a player enters the radius(sight) set it to true
            isEnemyInRange = true;

            array_manager.addObjInArray(other.transform.parent.gameObject, targetsInRangeTest);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == enemyTagToAttack)
        {
            array_manager.removeObjFromArray(other.transform.parent.gameObject, targetsInRangeTest);

            //if player exits and if he was the last one in range(is array empty) - set the playerinrange to false
            isEnemyInRange = !array_manager.IsArrayEmpty(targetsInRangeTest);
        }
    }

}

