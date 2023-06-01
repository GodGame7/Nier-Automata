using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private ICameraSetStrategy currentViewStrategy;

    float moveSpeed = 0.01f;

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
        currentViewStrategy.Action(this);
    }

    public void ChangeCameraPosition(Vector3 destPos, Quaternion destRot)
    {
        //StartCoroutine(test_co(destPos, destRot));
    }

    private IEnumerator test_co(Vector3 destPos, Quaternion destRot)
    {
        int cnt = 0;
        Vector3 center = 0.02f * Vector3.up;
        Vector3 rotation = Vector3.zero;
        float rotationSpeed = 45f;

        while (Vector3.SqrMagnitude(transform.position - destPos) > 0.00001f && cnt++ < 1000)
        {
            transform.position = Vector3.Slerp(transform.position, destPos, moveSpeed * Time.deltaTime * cnt);
            transform.LookAt(center);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, rotationSpeed * Time.deltaTime);

            yield return null;
        }
        transform.position = destPos;
        transform.LookAt(center);
    }
    private void FixedUpdate()
    {
        transform.LookAt(0.02f * Vector3.up);
    }
}
