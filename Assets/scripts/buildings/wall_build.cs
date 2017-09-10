using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_build : MonoBehaviour {

    private int unitsBuildingIt = 0;
    private wall_test wallBuildScript;

	// Use this for initialization
	void Start () {
        wallBuildScript = GetComponentInChildren<wall_test>();
    }
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == global_const.playersTag)
        {
            wallBuildScript.numberUnitsBuilding++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == global_const.playersTag)
        {
            if (unitsBuildingIt > 0)
            {
                wallBuildScript.numberUnitsBuilding--;
            }

        }
    }
}
