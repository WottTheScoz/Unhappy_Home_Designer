using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDragging : MonoBehaviour
{
    //Boundary values are multiplied by the following two variables.
    public float BoundOffsetterX = 1f;
    public float BoundOffsetterY = 1f;

    [Space(20)]
    public bool ImageIsDraggable;

    private float MousePositionOffsetX;
    private float MousePositionOffsetY;

    private Vector3 ImageBoundsMin;
    private Vector3 ImageBoundsMax;

    void Start()
    {
        ImageIsDraggable = true;
        CalculateImageBounds();
    }

    //Calculates the boundaries of this image.
    void CalculateImageBounds()
    {
        Bounds ImageBounds = GetComponent<SpriteRenderer>().bounds;
        ImageBoundsMin = ImageBounds.min;
        ImageBoundsMax = ImageBounds.max;
    }

    private float GetMouseWorldPosition(string axis)
    {
        //capture mouse position on a specific axis & return WorldPoint
        if(axis == "x")
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        else if(axis == "y")
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        }
        else
        {
            Debug.Log("Error in GetMouseWorldPosition in ImageDragging.cs");
            return 1f;
        }
    }

    #region OnMouse Functions

    //Stops the image from scrolling when the player stops dragging.
    private void OnMouseDown()
    {
        MousePositionOffsetX = gameObject.transform.position.x - GetMouseWorldPosition("x");
        MousePositionOffsetY = gameObject.transform.position.y - GetMouseWorldPosition("y");
    }

    //Allows the image to be dragged under the constraints of its size.
    private void OnMouseDrag()
    {
        if (ImageIsDraggable)
        {
            float ThisPositionX = gameObject.transform.position.x;
            float ThisPositionY = gameObject.transform.position.y;

            //The inverse functions of OnMouseDown()
            ThisPositionX = Mathf.Clamp(GetMouseWorldPosition("x") + MousePositionOffsetX, ImageBoundsMin.x * BoundOffsetterX, ImageBoundsMax.x * BoundOffsetterX);
            ThisPositionY = Mathf.Clamp(GetMouseWorldPosition("y") + MousePositionOffsetY, ImageBoundsMin.y * BoundOffsetterY, ImageBoundsMax.y * BoundOffsetterY);

            gameObject.transform.position = new Vector3(ThisPositionX, ThisPositionY, 0);
        }
    }
    #endregion
}
