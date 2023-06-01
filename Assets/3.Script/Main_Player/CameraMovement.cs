using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // 플레이어 오브젝트의 Transform 컴포넌트

    public float rotationSpeed = 5f; // 카메라 회전 속도
    public float distance = 5f; // 플레이어와의 거리

    private float currentRotation = 0f;

    private void LateUpdate()
    {
        // 카메라 위치 업데이트
        Vector3 offset = Quaternion.Euler(0f, currentRotation, 0f) * new Vector3(0f, 3f, -distance);
        transform.position = target.position + offset;

        // 카메라 회전 업데이트
        currentRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        transform.LookAt(target);
    }
}