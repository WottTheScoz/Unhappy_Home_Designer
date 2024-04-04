using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctionality : MonoBehaviour
{
    private float OriginalSize = 5f;
    private float ZoomedSize = 2.5f;

    void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        camera.orthographicSize = OriginalSize;
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            ZoomCamera();
            //Recalculate Image Bounds in ImageDragging.
        }
    }

    void ZoomCamera()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        if (camera.orthographicSize == OriginalSize)
        {
            camera.orthographicSize = ZoomedSize;
        }
        else
        {
            camera.orthographicSize = OriginalSize;
        }
    }

    public void ManualCameraZoom(bool zoomIn)
    {
        Camera camera = gameObject.GetComponent<Camera>();
        if (zoomIn)
        {
            camera.orthographicSize = ZoomedSize;
        }
        else
        {
            camera.orthographicSize = OriginalSize;
        }
    }
}
