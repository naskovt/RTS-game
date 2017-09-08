using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class build_wall : MonoBehaviour {

    public Text canceledText;
    public GameObject wallPrefab;

    private GameObject wall;
    private Rigidbody wallRB;
    private NavMeshObstacle wallNavObstacle;
    private Collider wallColl;
    internal static bool isBuildingWall = false;
    internal static bool isWallSpawned = true;

    private string cancelingConstruction = "Wall canceled!";


    private void Update()
    {
        if (isBuildingWall)
        {
            //if you press escape or right click during building time
            if (isBuildingWall && Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                pop_up_text.StrToPop = cancelingConstruction;

                Destroy(wall);

                isBuildingWall = false;

                isWallSpawned = true;
            }
            else
            {
                //move the planning of the wall to desired building location pointed with the mouse
                Vector3 buildLocation = ray_mouse.RayHitFromMousePos(Input.mousePosition).point;

                if (!isWallSpawned)
                {
                    wall = Instantiate(wallPrefab, buildLocation, Quaternion.Euler(0, 0, 0));

                    InitializePlannedWall();
                }

                wall.transform.position = buildLocation;

                if (Input.GetMouseButtonDown(0))
                {
                    SpawnPlannedWall();

                    //enable the constructing method for the wall itself(if player is building it)
                    wall.GetComponent<wall_test>().enabled = true;
                }

            }
        }

        selection_manager.PrintArray();
    }

    public void BuildWall()
    {
        isBuildingWall = !isBuildingWall;
        isWallSpawned = !isBuildingWall;
    }

    private void InitializePlannedWall()
    {
        wallRB = wall.GetComponent<Rigidbody>();
        wallColl = wall.GetComponent<Collider>();
        wallNavObstacle = wall.GetComponent<NavMeshObstacle>();

        //disable beign an obstacle 
        wallNavObstacle.enabled = false;
        //disable physics
        wallRB.isKinematic = true;
        //disable collider
        wallColl.enabled = false;
        //say to the script, that the wall is instantiated, no need again!
        isWallSpawned = true;
    }

    private void SpawnPlannedWall()
    {
        wallRB.isKinematic = false;
        wallColl.enabled = true;
        isBuildingWall = false;
        wallNavObstacle.enabled = true;
    }
}


