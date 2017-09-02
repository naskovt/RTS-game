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
    private float rotationSpeed = 1;
    private Vector3 orbitPointForRotation;
    private Vector3 defaultCameraAngles;
    private float defaultHeightFromGround;

    public Transform gizmoNoRotation;

    private void Start()
    {
        defaultCameraAngles = transform.eulerAngles;
        defaultHeightFromGround = transform.position.y;
        ResetGizmo();
    }

    void Update () {
        pointerPosScreen = Input.mousePosition;
        mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        scrollWheel = Input.GetAxis("Mouse ScrollWheel");
	}

    private void FixedUpdate()
    {
        // if orbit rotation of the camera is active - moving up/down/left/right is disabled

        #region fire raycast ONCE to get the point, which the camera is looking at
        if ((Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetMouseButtonDown(1)))
        {
            print(123);
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(pointerPosScreen);

            if (Physics.Raycast(ray, out hit, 1000, layerMaskGround))
            {
                orbitPointForRotation = hit.point;
            }
        }
        #endregion

        //move camera or rotate it by holding alt + right mouse around the orbit of the raycast hit.point ont the ground
        if (Input.GetKeyDown("f"))
        {
            ResetCamera();
        }
        else
        {
            if (scrollWheel != 0)
            {
                ZoomCamera();
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
            {
                RotateCamera();
            }
            else
            {
                MoveCamera();
            }

        }
    }

    private void RotateCamera()
    {
        transform.RotateAround(orbitPointForRotation, transform.right, mouseMovement.y * rotationSpeed);
        transform.RotateAround(orbitPointForRotation, -gizmoNoRotation.up, mouseMovement.x * rotationSpeed);
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
        else if (pointerPosScreen.y > Screen.height + 100)
        {
            transform.Translate(gizmoNoRotation.forward, Space.World);
        }
        #endregion
    }

    private void ZoomCamera()
    {
        transform.Translate(transform.forward * scrollWheel * scrollSpeed);
    }

    private void ResetCamera()
    {
        //determine the height needed to reset the camera with
        float neededHeightFromGround = defaultHeightFromGround - gizmoNoRotation.up.y;

        //reset rotation angles
        transform.eulerAngles = defaultCameraAngles;
        ResetGizmo();

        //move camera to the default height
        transform.position = new Vector3( transform.position.x, defaultHeightFromGround, transform.position.z);
    }

    private void ResetGizmo()
    {
        gizmoNoRotation.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
