using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selection_manager : MonoBehaviour
{

    public Camera cam;
    public Material selectionMaterial;
    public Material playerMaterial;
    //public GameObject cube;

    /// <summary>
    /// drawing rect part with a custom texture with alpha
    /// </summary>
    public Texture textureRect;
    //2D points of the mouse on the Screen
    private Vector2 firstPoint;
    private Vector2 secondPoint;
    private Rect rect;

    // var for position of mouseClick in 3D space
    private RaycastHit firstRayHit;
    private RaycastHit secondRayHit;
    private int rayLayer = 1 << 8;

    //target
    //private Vector3 pointedSpotToMove;

    //selection
    internal static GameObject[] selectedObjects;
    internal static ushort populationLimit = 100;
    private static ushort nextEmptyArrayPos = 0;
    private bool isLasoSelectionThisFrame = false;
    private float differenceBetweenClickAndDrag = 15;

    //all objects
    private GameObject[] selectableObjectsInScene
    {
        get
        {
            return GameObject.FindGameObjectsWithTag("Player");
        }
    }

    private void Start()
    {
        //initialize the selection array, that holds the currently selected objs
        selectedObjects = new GameObject[populationLimit];
    }

    //--------CORE--------
    void Update()
    {
        #region Click and drag selection - Draw a rect(preparation) and check for selectable objects between the two points

        //if you are not building a wall --> then use selection
        //if (!build_wall.isBuildingWall)
        //{


            #region save the position of the first left mouse click and the raycast hit
            if (Input.GetMouseButtonDown(0))
            {
                firstPoint = Input.mousePosition;
                firstRayHit = RayHitFromMousePos(firstPoint);
            }
            #endregion

            #region if left mouse button is held down
            if (Input.GetMouseButton(0))
            {
                #region save the second mouse click and raycast hit
                secondPoint = Input.mousePosition;
                secondRayHit = RayHitFromMousePos(secondPoint);
                #endregion

                #region check is the lasso slecection active or the one click

                if (differenceBetween2DVectors(firstPoint, secondPoint) > differenceBetweenClickAndDrag)
                {
                    isLasoSelectionThisFrame = true;
                }
                else
                {
                    isLasoSelectionThisFrame = false;
                }
                #endregion

                #region activate the laso selection
                //change later, son! is the selection raycast hit gonna be outside the map?
                if (firstRayHit.point != Vector3.zero && secondRayHit.point != Vector3.zero && isLasoSelectionThisFrame)
                {
                    //notice passing the Vector3 x and z to Vector2 x and y
                    SelectObjectsBetween(new Vector2(firstRayHit.point.x, firstRayHit.point.z),
                        new Vector2(secondRayHit.point.x, secondRayHit.point.z));
                }
                #endregion

            }
            #endregion
            ////////good
            #endregion

            #region Single left click handling for selecting objects

            if (Input.GetMouseButtonDown(0) && !isLasoSelectionThisFrame)
            {
                RaycastHit hit;
                Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(cameraRay, out hit, 10000f, rayLayer))
                {
                    SelectionManagerOneClick(cameraRay, hit);
                }
            }
            #endregion

            #region right click movement
            if (Input.GetMouseButtonDown(1) == true && !Input.GetKey(KeyCode.LeftAlt))
            {
                Vector2 mousePos = Input.mousePosition;
                Vector3 pointedSpotToMove;

                pointedSpotToMove = RayHitFromMousePos(mousePos).point;
                DecideToMoveObject(pointedSpotToMove);
            }
            #endregion

        //}
        //#region activate UI info if objects are selected
        //if (!IsCurrentSelectionEmpty())
        //{
        //    ui_selection.PrintNames(selectableObjectsInScene);
        //}
        //#endregion

        //PrintArray();
        //ui_selection.PrintNames(selectedObjects);
        //print(selectableObjectsInScene.Length);
    }
    //--------------------

    private void SelectObjectsBetween(Vector2 firstPointRect, Vector2 secondPointRect)
    {

        //loop through array
        for (int i = 0; i < selectableObjectsInScene.Length; i++)
        {
            if (selectableObjectsInScene[i] != null)
            {
                //print(IsObjectInSelectionLaso(firstPointRect, secondPointRect, selectableObjectsInScene[i]));

                if (IsObjectInSelectionLaso(firstPointRect, secondPointRect, selectableObjectsInScene[i]))
                {
                    AddObjectToSelection(selectableObjectsInScene[i]);
                }
            }
            else
            {
                throw new NullReferenceException("No selectable objects in scene!");
            }
        }
        //nextEmptyArrayPos = 0;
    }

    private bool IsObjectInSelectionLaso(Vector2 firstPoint, Vector2 secondPoint, GameObject obj)
    {
        //print("XXX between:" + firstPoint.x + "   " + obj.transform.position.x + "   " + secondPoint.x + " : " +
        //    IsFloatBetweenTwoOther(firstPoint.x, secondPoint.x, obj.transform.position.x));

        //print("Y between:" + firstPoint.y + "   " + obj.transform.position.y + "   " + secondPoint.y +
        //    IsFloatBetweenTwoOther(firstPoint.y, secondPoint.y, obj.transform.position.y));

        if (IsFloatBetweenTwoOther(firstPoint.x, secondPoint.x, obj.transform.position.x)
            && IsFloatBetweenTwoOther(firstPoint.y, secondPoint.y, obj.transform.position.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AddObjectToSelection(GameObject selectedObj)
    {
        if (!IsSameObjectSelected(selectedObj))
        {
            if (nextEmptyArrayPos < populationLimit)
            {
                selectedObjects[nextEmptyArrayPos] = selectedObj;
                HighlightSelection(selectedObj);
                nextEmptyArrayPos++;
            }
            else
            {
                throw new ArgumentOutOfRangeException("population limit reached, cannot select more than that!");
            }
        }
    }



    ///////checked methods, no worries!

    private float differenceBetween2DVectors(Vector2 first, Vector2 second)
    {
        //calculate the difference between radiuses of two vectors2d
        float firstRadius = (float)(first.magnitude * Math.PI * 2);
        float secondRadius = (float)(second.magnitude * Math.PI * 2);
        return Math.Abs(firstRadius - secondRadius);
    }

    private bool IsFloatBetweenTwoOther(float firstFloat, float secondFloat, float value)
    {
        var smallerFloat = firstFloat;
        var biggerFloat = secondFloat;

        if (firstFloat > secondFloat)
        {
            smallerFloat = secondFloat;
            biggerFloat = firstFloat;
        }

        if (smallerFloat <= value && value <= biggerFloat)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private RaycastHit RayHitFromMousePos(Vector2 pointOnScreen)
    {
        Ray ray = cam.ScreenPointToRay(pointOnScreen);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000f, rayLayer))
        {
            return hit;
        }
        else
        {
            return new RaycastHit();
        }
    }

    private void DecideToMoveObject(Vector3 pointedSpotToMove)
    {
        //
        if (!IsCurrentSelectionEmpty())
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

    private void SelectionManagerOneClick(Ray ray, RaycastHit hit)
    {
        if (hit.transform.tag == "Player" && Input.GetMouseButtonDown(0) == true && !Input.GetKey(KeyCode.LeftControl))
        {
            ClearSelection();
            AddObjectToSelection(hit.transform.gameObject);
        }
        else if (Input.GetMouseButtonDown(0) == true && Input.GetKey(KeyCode.LeftControl) && hit.transform.tag == "Player")
        {
            AddObjectToSelection(hit.transform.gameObject);
        }
        else
        {
            ClearSelection();
        }
    }

    internal static bool IsSameObjectSelected(GameObject targetSelection)
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
        nextEmptyArrayPos = 0;
    }

    private bool IsCurrentSelectionEmpty()
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

    private void TestPrintAllSelObjects()
    {
        string oneLine = "";

        foreach (var item in selectableObjectsInScene)
        {
            oneLine += item.name;
        }
        print(oneLine);
    }

    internal static void PrintArray()
    {
        string oneLineArray = "";
        for (int i = 0; i < populationLimit; i++)
        {
            if (selectedObjects[i] != null)
            {
                oneLineArray += selectedObjects[i].name;

            }
            else
            {
                oneLineArray += "0";
            }
        }
        print(oneLineArray);
    }

    private void OnGUI()
    {
        if (Input.GetMouseButton(0))
        {
            //print(firstPoint + " , " + secondPoint);
            //create rect with opposite Y
            rect = Rect.MinMaxRect(firstPoint.x, Screen.height - firstPoint.y, secondPoint.x, Screen.height - secondPoint.y);

            GUI.DrawTexture(rect, textureRect);
        }
    }

    ///////////////////////////
}