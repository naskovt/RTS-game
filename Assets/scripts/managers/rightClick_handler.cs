using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightClick_handler : MonoBehaviour {

    /// <summary>
    /// tells if the right click button of the mouse is for gathering resources or finishing constructing a wall
    /// </summary>

    internal static selection_type rightClickCommand = selection_type.none;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //right clicked
        if (Input.GetMouseButtonDown(1) == true)
        {
            GameObject pointedObject = ray_mouse.RayHitFromMouseAllLayers(Input.mousePosition, 1 << 8).transform.gameObject;

            if (pointedObject.transform.tag == global_const.resourcesTag)
            {
                rightClickCommand = selection_type.resource;
            }
            else if (pointedObject.transform.tag == global_const.wallTag)
            {
                if (Custom.DoesObjectHaveComponent<wall_test>(pointedObject))
                {
                    rightClickCommand = selection_type.wallNotFinished;
                }

            }

        print(pointedObject.name);
        }

    }


}
