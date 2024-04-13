using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDragging : MonoBehaviour
{
    public GameObject MainCamera;

    [System.NonSerialized]
    public bool ImageIsDraggable;

    private float MousePositionOffsetX;
    private float MousePositionOffsetY;

    float CameraOffsetterX = 9f;
    float CameraOffsetterY = 5f;

    private Vector3 ImageBoundsMin;
    private Vector3 ImageBoundsMax;

    private CameraFunctionality cameraFunctionality;

    void Start()
    {
        cameraFunctionality = MainCamera.GetComponent<CameraFunctionality>();
        ImageIsDraggable = true;
        CalculateImageBounds();
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            float CurrentCameraSize = cameraFunctionality.GetCurrentCameraSize();
            if(CurrentCameraSize != cameraFunctionality.OriginalSize)
            {
                //Hard-coded for now.
                gameObject.transform.position *= 0.7f;
            }
        }
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
            //Offsets the image to edge of the camera instead of the middle of it.
            float TempCameraOffsetterX = CameraOffsetterX;
            float TempCameraOffsetterY = CameraOffsetterY;

            //Changes the offsetter values based on the current camera size.
            float CurrentCameraSize = cameraFunctionality.GetCurrentCameraSize();
            float originalSize = cameraFunctionality.OriginalSize;

            TempCameraOffsetterX *= CurrentCameraSize / originalSize;
            TempCameraOffsetterY *= CurrentCameraSize / originalSize;
            
            //Holds the changes in the image's position.
            float ThisPositionX = gameObject.transform.position.x;
            float ThisPositionY = gameObject.transform.position.y;

            //The inverse functions of OnMouseDown()
            ThisPositionX = Mathf.Clamp(GetMouseWorldPosition("x") + MousePositionOffsetX, ImageBoundsMin.x + TempCameraOffsetterX, ImageBoundsMax.x - TempCameraOffsetterX);
            ThisPositionY = Mathf.Clamp(GetMouseWorldPosition("y") + MousePositionOffsetY, ImageBoundsMin.y + TempCameraOffsetterY, ImageBoundsMax.y - TempCameraOffsetterY);

            //Changes the image's position
            gameObject.transform.position = new Vector3(ThisPositionX, ThisPositionY, 0);
        }
    }
    #endregion
}
