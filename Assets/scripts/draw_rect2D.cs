using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw_rect2D : MonoBehaviour {

    public Texture textureRect;

    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Rect rect;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            firstPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            secondPoint = Input.mousePosition;
        }

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
}
