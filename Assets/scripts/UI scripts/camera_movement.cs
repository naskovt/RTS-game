using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour {

    private Vector2 pointerPosScreen;
    private Vector3 mouseMovement;
    private float scrollWheel;
    private float scrollSpeed = 12;
    private int layerMaskGround = 1 << 8;
    private float rotationSpeed = 4;
    private Vector3 orbitPointForRotation;
    private Vector3 defaultCameraAngles;
    private float defaultHeightFromGround;

    private float maxHeight = 18;
    private float minHeight = 5;
    private float maxAngleY = 80;
    private float minAngleY = 10;

    //child of the camera, that gets the camera's position and y rotation only, used for moving the camera
    public Transform gizmoNoRotation;

    private void Start()
    {
        defaultCameraAngles = transform.eulerAngles;
        defaultHeightFromGround = transform.position.y;
        ResetGizmo();
    }

    void Update () {
        //get the inputs from the computer interface
        pointerPosScreen = Input.mousePosition;
        mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        scrollWheel = Input.GetAxis("Mouse ScrollWheel");
	}

    private void FixedUpdate()
    {
        #region fire raycast ONCE to get the point, which the camera is looking at
        if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetMouseButtonDown(1)))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(pointerPosScreen);

            if (Physics.Raycast(ray, out hit, 1000, layerMaskGround))
            {
                orbitPointForRotation = hit.point;
            }
        }
        #endregion

        //check if the player wants to reset the camera view, otherwise check for other movements
        if (Input.GetKeyDown("f"))
        {
            ResetCamera();
        }
        else
        {
            //check if the player wants to zoom with the scrollwheel
            if (scrollWheel != 0)
            {
                ZoomCamera();
            }

            //move camera or rotate it by holding alt + right mouse around the orbit of the raycast hit.point ont the ground
            // if orbit rotation of the camera is active - moving up/down/left/right is disabled
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
            {
                RotateCamera();
            }
            else
            {
                MoveCamera();
            }
        }

        ResetGizmo();
    }

    private void RotateCamera()
    {
        Vector3 safeMouseReadings = mouseMovement;

        //if the player wants to rotate the camera outside the safety orbit, zero his input to prevent glitches
        if (transform.localEulerAngles.x > maxAngleY && safeMouseReadings.y < 0 || (transform.localEulerAngles.x < minAngleY && safeMouseReadings.y > 0))
        {
            safeMouseReadings.y = 0;
        }

        //rotate the camera inside the boundries
        transform.RotateAround(orbitPointForRotation, -gizmoNoRotation.right, safeMouseReadings.y * rotationSpeed);
        transform.RotateAround(orbitPointForRotation, -gizmoNoRotation.up, safeMouseReadings.x * rotationSpeed);

        //hardcode the z euler angle to 0
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

        ResetGizmo();
    }

    private void MoveCamera()
    {
        #region move camera left/right

        if (pointerPosScreen.x < 0)
        {
            transform.Translate(-gizmoNoRotation.right, Space.World);
        }
        else if (pointerPosScreen.x > Screen.width)
        {
            transform.Translate(gizmoNoRotation.right, Space.World);
        }
        #endregion

        #region move camera forward/backwards
        if (pointerPosScreen.y < 0)
        {
            transform.Translate(-gizmoNoRotation.forward, Space.World);
        }
        else if (pointerPosScreen.y > Screen.height)
        {
            transform.Translate(gizmoNoRotation.forward, Space.World);
        }
        #endregion
    }

    private void ZoomCamera()
    {
        //check if player wants to zoom inside boundries
        if (transform.position.y > minHeight && scrollWheel < 0 || transform.position.y < maxHeight && scrollWheel > 0)
        {
            Vector3 direction = gizmoNoRotation.position + new Vector3(0, gizmoNoRotation.position.y + (scrollWheel * 1000), 0);

            transform.position = Vector3.MoveTowards(transform.position, direction, 1);
        }
    }

    private void ResetCamera()
    {
        //reset rotation angles
        transform.eulerAngles = defaultCameraAngles;
        ResetGizmo();

        //move camera to the default height
        transform.position = new Vector3(transform.position.x, defaultHeightFromGround, transform.position.z);
        ResetGizmo();
    }

    private void ResetGizmo()
    {
        gizmoNoRotation.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
