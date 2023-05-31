using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSideView : ICameraSetStrategy
{
    Vector3 destPos = new Vector3(0.238f, 0.02f, 0);
    Quaternion destRot = Quaternion.Euler(0, -90, 0);
    Camera mainCamera = Camera.main;

    public void Action()
    {
        mainCamera.transform.position = destPos;
        mainCamera.transform.rotation = destRot;
    }
}
