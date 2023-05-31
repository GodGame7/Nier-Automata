using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopView : MonoBehaviour, ICameraSetStrategy
{
    Vector3 destPos = 0.33f * Vector3.up;
    Quaternion destRot = Quaternion.Euler(90, 0, 0);
    Camera mainCamera = Camera.main;

    public void Action()
    {
        mainCamera.transform.position = destPos;
        mainCamera.transform.rotation = destRot;
    }
}
