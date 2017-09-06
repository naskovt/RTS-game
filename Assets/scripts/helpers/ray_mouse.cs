using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray_mouse : MonoBehaviour {

    internal static Camera cam;
    internal static Vector2 firstPoint;
    internal static Vector2 secondPoint;
    internal static RaycastHit firstRayHit;
    internal static RaycastHit secondRayHit;
    private static short rayLayer = 1 << 8;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            firstPoint = Input.mousePosition;
            firstRayHit = RayHitFromMousePos(firstPoint);
        }
        if (Input.GetMouseButton(1) == true)
        {
            secondPoint = Input.mousePosition;
            secondRayHit = RayHitFromMousePos(secondPoint);
        }
    }

    internal static RaycastHit RayHitFromMousePos(Vector2 pointOnScreen)
    {
        Ray ray = cam.ScreenPointToRay(pointOnScreen);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000f , rayLayer))
        {
            return hit;
        }
        else
        {
            return new RaycastHit();
        }
    }

}
