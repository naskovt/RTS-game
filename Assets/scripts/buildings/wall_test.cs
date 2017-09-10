using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wall_test : MonoBehaviour
{

    public Material matFinishedBuilding;
    /// <summary>
    /// /----------------------------------------------------------make it false when script is ready!!!!!!!!!!!!!!!!!
    /// </summary>
    internal int numberUnitsBuilding = 0;

    private static int wallHeight = 40;



    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().mass = 5;
        GetComponent<NavMeshObstacle>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        //if wall's height is less then the maximum, build it!
        if (transform.localScale.y < global_const.wallHeight && numberUnitsBuilding > 0)
        {
            //if somebody is building the wall
            if (transform.localScale.y < global_const.wallHeight)
            {
                transform.localScale = transform.localScale + new Vector3(0, Time.deltaTime * global_const.buildingSpeed * numberUnitsBuilding, 0);
            }

        }
        else if (transform.localScale.y >= global_const.wallHeight)
        {
            //wall built
            GetComponent<Rigidbody>().mass = 100;
            GetComponent<NavMeshObstacle>().enabled = true;
            GetComponent<Renderer>().material = matFinishedBuilding;
            //Destroy(this);
        }
    }
}
