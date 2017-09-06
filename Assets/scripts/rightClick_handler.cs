using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightClick_handler : MonoBehaviour {

    internal static bool isRightClickOnResource = false;
    internal static bool isRightClickOnEnemy = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



        if (Input.GetMouseButtonDown(1) == true)
        {
            isRightClickOnResource = false;
            isRightClickOnEnemy = false;

            if (ray_mouse.secondRayHit.transform.tag == "resource")
            {
                isRightClickOnResource = true;
            }
            else if (ray_mouse.secondRayHit.transform.tag == "enemy")
            {
                isRightClickOnEnemy = true;
            }
        }
    }


}
