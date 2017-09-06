using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_sense : MonoBehaviour {

    internal bool isPlayerInRange = false;
    internal Vector3 playerPosition;

    private GameObject[] targetsInRange;
    private Vector3 currentEnemyPosition;

    void Start () {
        targetsInRange = new GameObject[10];
    }

    void FixedUpdate () {
        currentEnemyPosition = transform.parent.position;

        //if players have entered the sight of the enemy, check when he is gone = no elements in the array

        if (isPlayerInRange)
        {
            playerPosition = ai_closestObj.ClosestEnemyPosition(currentEnemyPosition, targetsInRange);
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerCollider")
        {
            //once a player enters the radius(sight) set it to true
            isPlayerInRange = true;

            //notice to give the parent of the collider!
            array_manager.addObjInArray(other.transform.parent.gameObject, targetsInRange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "playerCollider")
        {
            //notice to give the parent of the collider!
            array_manager.removeObjFromArray(other.transform.parent.gameObject, targetsInRange);

            //if player exits and if he was the last one in range(is array empty) - set the playerinrange to false
            isPlayerInRange = !array_manager.IsArrayEmpty(targetsInRange);
        }
    }

}
