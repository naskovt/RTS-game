using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_closestObj : MonoBehaviour {

    public static Vector3 ClosestEnemyPosition(Vector3 myLocation, GameObject[] objsInRadius)
    {
        if (!array_manager.IsArrayEmpty(objsInRadius))
        {
            Vector3 closestEnemy = Vector3.zero;
            float smallestDistance = float.MaxValue;

            for (int i = 0; i < objsInRadius.Length; i++)
            {
                if (objsInRadius[i] != null)
                {
                    //calculate the distance between the script's object and the ones that are in range
                    float distanceBetweenObjects = Vector3.Distance(objsInRadius[i].transform.position, myLocation);

                    //print("distance to " + objsInRadius[i].name + " is :" + distanceBetweenObjects);

                    //keep the smallest distance in memory
                    if (distanceBetweenObjects < smallestDistance)
                    {
                        smallestDistance = distanceBetweenObjects;

                        closestEnemy = objsInRadius[i].transform.position;
                    }
                }
            }

            return closestEnemy;

        }
        else
        {
            throw new MissingReferenceException("The array in detecting ClosestEnemyPosition() is empty!");
        }

    } 

}
