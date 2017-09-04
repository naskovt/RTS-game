using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selection_manager : MonoBehaviour {

    public Camera cam;
    public Material selectionMaterial;
    public Material playerMaterial;

    /// <summary>
    /// drawing rect part
    /// </summary>
    public Texture textureRect;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Rect rect;

    //Mouse input manager Varables
    //private bool leftClick;
    //private bool rightClick;
    private RaycastHit firstRayHit;
    private RaycastHit secondRayHit;

    //target
    private Vector3 pointedSpotToMove;

    //selection
    private GameObject[] selectedObjects;
    private ushort populationLimit = 100;


    private ushort _nextEmptyArrayPos = 0;
    private ushort nextEmptyArrayPos
    {
        get
        {
            return _nextEmptyArrayPos;
        }

        set
        {
            _nextEmptyArrayPos = value;
        }
    }

    //all objects
    private GameObject[] selectableObjectsInScene;

    private void Start()
    {
        selectedObjects = new GameObject[populationLimit];
    }

    void Update()
    {
        #region Click and drag selection - Draw a rect(preparation) and check for selectable objects between the two points

        //
        //save the position of the first left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            firstPoint = Input.mousePosition;
            firstRayHit = RayHitFromMousePos();
        }

        //if left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            secondPoint = Input.mousePosition;
            secondRayHit = RayHitFromMousePos();

            if (firstRayHit.point != Vector3.zero || secondRayHit.point != Vector3.zero)
            {
                SelectObjectsBetween(firstRayHit.point, secondRayHit.point);
            }
        }
        ////////good

        #endregion

        #region Single click handling for selecting and moving objects

        //if mouse bttn is pressed
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out hit, 1000f))
            {
                SelectionManager(cameraRay, hit);
            }

        }
        #endregion

        //print(isCurrentSelectionEmpty());
        //print(nextEmptyArrayPos);
        PrintArray();
    }

    private void SelectObjectsBetween(Vector3 firstPointRect, Vector3 secondPointRect)
    {
        //loop through array
        /*
        for (int i = 0; i < populationLimit; i++)
        {
            if (selectedObjects[i] != null)
            {
                AssingPlayerMat(selectedObjects[i]);
            }
        }
        */
        //nextEmptyArrayPos = 0;
    }

    private RaycastHit RayHitFromMousePos()
    {
        Vector2 point;
        point = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(secondPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            return hit;
        }
        else
        {
            return new RaycastHit();
        }
    }

    private void DecideToMoveObject()
    {
        //
        if (!isCurrentSelectionEmpty())
        {
            for (int i = 0; i < nextEmptyArrayPos; i++)
            {
                if (selectedObjects[i] != null)
                {
                    selectedObjects[i].GetComponent<movement>().moveSelectedObjectToPoint(pointedSpotToMove);
                }
            }
        }
        else
        {
            pointedSpotToMove = Vector3.zero;
        }
    }

    private void SelectionManager(Ray ray, RaycastHit hit)
    {
        if (hit.transform.tag == "Player" && Input.GetMouseButtonUp(0) == true && !Input.GetKey(KeyCode.LeftControl))
        {
            ClearSelection();
            GameObject hitObject = hit.transform.gameObject;
            AddObjectToSelection(ref hitObject);
        }
        else if (Input.GetMouseButtonUp(0) == true && Input.GetKey(KeyCode.LeftControl) && !isSameObjectSelected(hit.transform.gameObject) && hit.transform.tag == "Player")
        {
            GameObject hitObject = hit.transform.gameObject;
            AddObjectToSelection(ref hitObject);
        }
        else if (Input.GetMouseButtonUp(1) == true)
        {
            pointedSpotToMove = hit.point;
            DecideToMoveObject();
        }
        else
        {
            ClearSelection();
        }
    }

    private void AddObjectToSelection(ref GameObject selectedObj)
    {
        if (nextEmptyArrayPos < populationLimit)
        {
            selectedObjects[nextEmptyArrayPos] = selectedObj;
            print(selectedObjects[nextEmptyArrayPos] + " --- " + selectedObj);
            HighlightSelection(selectedObj);
            nextEmptyArrayPos++;
        }
        else
        {
            throw new ArgumentOutOfRangeException("population limit reached, cannot select more than that!");
        }
    }

    private void PrintArray()
    {
        string oneLineArray = "";
        for (int i = 0; i < populationLimit; i++)
        {
            if (selectedObjects[i] != null)
            {
                oneLineArray += "1";
            }
            else
            {
                oneLineArray += "0";
            }
        }
        print(oneLineArray);
    }

    private bool isSameObjectSelected(GameObject targetSelection)
    {
        for (int i = 0; i < nextEmptyArrayPos; i++)
        {
            if (selectedObjects[i] != null && selectedObjects[i] == targetSelection)
            {
                    return true;
            }
        }
        return false;
    }

    private void ClearSelection()
    {
        //first dehighlight, cause the fucn uses the selected obj!
        DehighlightSelection();
        selectedObjects = new GameObject[populationLimit];
    }

    private void OnGUI()
    {
        if (Input.GetMouseButton(0))
        {
            //create rect with opposite Y
            rect = Rect.MinMaxRect(firstPoint.x, Screen.height - firstPoint.y, secondPoint.x, Screen.height - secondPoint.y);

            GUI.DrawTexture(rect, textureRect);
        }
    }

    private bool isCurrentSelectionEmpty()
    {
        for (int i = 0; i < populationLimit; i++)
        {
            if (selectedObjects[i] != null)
            {
                return false;
            }
        }
            return true;
    }

    private void HighlightSelection(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = selectionMaterial;
    }

    private void DehighlightSelection()
    {
        selectedObjects.DoForEach(AssingPlayerMat, nextEmptyArrayPos);
        nextEmptyArrayPos = 0;
    }

    private void AssingPlayerMat(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = playerMaterial;
    }

    //private GameObject[] ObjectsInSelectionRange()
    //{

    //}

    //////////////////test debug
    private void FixedUpdate()
    {
        //Debug.Log(pointedSpotToMove);
    }
    ///////////////////////////
    
}