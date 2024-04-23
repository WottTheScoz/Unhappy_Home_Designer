using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctionality : MonoBehaviour
{
    [System.NonSerialized]
    public float OriginalSize = 5f;
    [System.NonSerialized]
    public float ZoomedSize = 2.5f;

    public GameObject TimerSprite;

    private float TimerPosOffset = 2.29f;
    private float TimerScaleOffset = 0.31f;

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
            ChangeTimer(-TimerPosOffset, -TimerScaleOffset);
        }
        else
        {
            ThisCamera.orthographicSize = OriginalSize;
            ChangeTimer(TimerPosOffset, TimerScaleOffset);
        }
    }

    public void ManualCameraZoom(bool zoomIn)
    {
        if (zoomIn)
        {
            ThisCamera.orthographicSize = ZoomedSize;
            ChangeTimer(-TimerPosOffset, -TimerScaleOffset);
        }
        else
        {
            ThisCamera.orthographicSize = OriginalSize;
            ChangeTimer(TimerPosOffset, TimerScaleOffset);
        }
    }

    void ChangeTimer(float posOffset, float scaleOffset)
    {
        TimerSprite.transform.position += new Vector3(0, posOffset, 0);
        TimerSprite.transform.localScale += new Vector3(scaleOffset, scaleOffset, 0);
    }
}
