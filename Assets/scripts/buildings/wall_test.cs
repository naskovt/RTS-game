using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wall_test : MonoBehaviour {

    public Material matFinishedBuilding;
    /// <summary>
    /// /----------------------------------------------------------make it false when script is ready!!!!!!!!!!!!!!!!!
    /// </summary>
    private static bool isUnitBuildingIt = false;

    private static int wallHeight = 4;



    // Use this for initialization
    void Start () {
        this.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        //if wall's height is less then the maximum, build it!
        if (transform.localScale.y < wallHeight)
        {
            //if somebody is building the wall
            if (isUnitBuildingIt)
            {
                transform.localScale = transform.localScale + new Vector3(0, Time.deltaTime, 0);
            }
        }
        else
        {
            GetComponent<Renderer>().material = matFinishedBuilding;
            Destroy(this);
        }
	}

}
