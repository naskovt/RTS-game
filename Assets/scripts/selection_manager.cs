using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selection_manager : MonoBehaviour {

    public Camera cam;
    public Material selectionMaterial;
    public Material playerMaterial;
    public Texture texture;

    //Mouse input manager Varables
    private bool leftClick;
    private bool rightClick;

    //target
    private Vector3 targetPoint;

    //selection
    private List<GameObject> selectedObject;

    private Color color;

    private Rect rect;

    private void Start()
    {
        selectedObject = new List<GameObject>();
        color = new Color(0, 0, 1, 0.5f);
    }

    void Update()
    {

        //get mouse click coordinates in 3D
        //fire a raycast from the main camera
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 firstPointRect;
            Vector3 secondPointRect;

            RaycastHit firstHit;
            RaycastHit secondHit;
            Ray firstCameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Ray secondCameraRay = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(firstCameraRay, out firstHit, 1000f) && Physics.Raycast(secondCameraRay, out secondHit, 1000f))
            {
                firstPointRect = firstHit.point;
                secondPointRect = secondHit.point;

                Draw2DRect(firstPointRect, secondPointRect);

                CheckForSelectableObjectsBetween(firstPointRect, secondPointRect);

            }


        }
        else if(Input.GetMouseButtonUp(0))
        {

        }

        if (Input.GetMouseButtonUp(0) | Input.GetMouseButtonUp(1))
        {

            RaycastHit hit;
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(cameraRay, out hit, 1000f))
            {
                SelectionManager(cameraRay, hit);
            }

        }
    }

    private void CheckForSelectableObjectsBetween(Vector3 firstPointRect, Vector3 secondPointRect)
    {
        
    }

    private void Draw2DRect(Vector3 firstPointRect, Vector3 secondPointRect)
    {
        Vector2 size;

        size = new Vector2(secondPointRect.x - firstPointRect.x, secondPointRect.y - firstPointRect.y);

        rect = new Rect(firstPointRect, size);
        //print("drawww");
    }

    //private void OnGUI(Rect rect)
    //{
    //    //GUI.Box(rect, texture);
    //}

    private void DecideToMoveObject()
    {
        if (targetPoint != Vector3.zero)
        {
            foreach (var obj in selectedObject)
            {
                obj.GetComponent<movement>().moveSelectedObjectToPoint(targetPoint);
            }
        }
    }

    private bool isTargetExisting()
    {
        if (true)
        {

        }

        return false;
    }

    private void SelectionManager(Ray ray, RaycastHit hit)
    {

        if (selectedObject.Count == 0)
        {
            if (hit.transform.tag == "Player" && Input.GetMouseButtonUp(0) == true)
            {
                selectedObject.Add(hit.transform.gameObject);
                HighlightSelection(hit.transform.gameObject);
            }
            else
            {
                DehighlightSelection();
                selectedObject.Clear();
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0) == true && !isSameObjectSelected( hit.transform.gameObject ) && hit.transform.tag == "Player")
            {
                selectedObject.Add(hit.transform.gameObject);
                HighlightSelection(hit.transform.gameObject);
            }
            else if (Input.GetMouseButtonUp(1) == true)
            {
                targetPoint = hit.point;
                DecideToMove();
            }
            else
            {
                DehighlightSelection();
                selectedObject.Clear();
            }

        }
    }

    private void DecideToMove()
    {
        //check if we have a target(destination) to move object to 
        if (selectedObject.Count == 0)
        {
            targetPoint = Vector3.zero;
        }
        else
        {
            DecideToMoveObject();
        }
    }

    private void HighlightSelection(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = selectionMaterial;
    }

    private void DehighlightSelection()
    {

        foreach (var obj in selectedObject)
        {
                obj.GetComponent<Renderer>().material = playerMaterial;
        }

    }

    private bool isSameObjectSelected(GameObject targetSelection)
    {

        foreach (var selected in selectedObject)
        {
            if (selected.Equals(targetSelection))
            {
                return true;
            }
        }

        return false;
    }


    //////////////////test debug
    private void FixedUpdate()
    {
        //Debug.Log(targetPoint);


        //move object


    }
    ///////////////////////////
    
    //
    //
    
}