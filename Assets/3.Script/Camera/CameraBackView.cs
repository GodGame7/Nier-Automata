using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackView : ICameraSetStrategy
{
    Vector3 destPos = -0.33f * Vector3.forward;
    Camera mainCamera = Camera.main;

    public void Action()
    {
        mainCamera.transform.position = destPos;
        mainCamera.transform.rotation = Quaternion.identity;
    }
}
