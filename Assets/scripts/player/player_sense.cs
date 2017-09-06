using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_sense : MonoBehaviour {

    public GameObject marker;

    internal bool isEnemyInRange = false;
    internal Vector3 enemyPosition;

    private GameObject[] targetsInRange;
    private Vector3 currentPlayerPosition;

    void Start()
    {
        targetsInRange = new GameObject[10];
    }

    void FixedUpdate()
    {
        currentPlayerPosition = transform.parent.position;

        //if enemies have entered the sight of the unit, check when they are gone = no elements in the array

        if (isEnemyInRange)
        {
            enemyPosition = ai_closestObj.ClosestEnemyPosition(currentPlayerPosition, targetsInRange);
        }

        marker.transform.position = currentPlayerPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyCollider")
        {
            //once a player enters the radius(sight) set it to true
            isEnemyInRange = true;

            array_manager.addObjInArray(other.transform.parent.gameObject, targetsInRange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemyCollider")
        {
            array_manager.removeObjFromArray(other.transform.parent.gameObject, targetsInRange);

            //if player exits and if he was the last one in range(is array empty) - set the playerinrange to false
            isEnemyInRange = !array_manager.IsArrayEmpty(targetsInRange);
        }
    }

}
