using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private ICameraSetStrategy currentViewStrategy;

    private void Awake()
    {
        SetTopViewStrategy();
    }

    public void SetTopViewStrategy()
    {
        SetViewStrategy(new CameraTopView());
    }
    public void SetBackViewStrategy()
    {
        SetViewStrategy(new CameraBackView());
    }
    public void SetSideViewStrategy()
    {
        SetViewStrategy(new CameraSideView());
    }
    private void SetViewStrategy(ICameraSetStrategy strategy)
    {
        currentViewStrategy = strategy;
        currentViewStrategy.Action();
    }
}
