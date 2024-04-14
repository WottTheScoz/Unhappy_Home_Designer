using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctionality : MonoBehaviour
{
    [System.NonSerialized]
    public float OriginalSize = 5f;
    [System.NonSerialized]
    public float ZoomedSize = 2.5f;

    private Camera ThisCamera;

    void Start()
    {
        ThisCamera = gameObject.GetComponent<Camera>();
        ThisCamera.orthographicSize = OriginalSize;
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            ZoomCamera();
        }
    }

    public float GetCurrentCameraSize()
    {
        return ThisCamera.orthographicSize;
    }

    void ZoomCamera()
    {
        if (GetCurrentCameraSize() == OriginalSize)
        {
            ThisCamera.orthographicSize = ZoomedSize;
        }
        else
        {
            ThisCamera.orthographicSize = OriginalSize;
        }
    }

    public void ManualCameraZoom(bool zoomIn)
    {
        if (zoomIn)
        {
            ThisCamera.orthographicSize = ZoomedSize;
        }
        else
        {
            ThisCamera.orthographicSize = OriginalSize;
        }
    }
}
