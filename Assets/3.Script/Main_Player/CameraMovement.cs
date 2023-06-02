using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;
    public float distance = 5f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;
    public float verticalSpeed = 2f;

    private float currentRotation = 0f;
    private float currentVerticalAngle = 0f;

    private void LateUpdate()
    {
        // ���콺 ������ �Է�
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ���� �� ���� ȸ�� ������Ʈ
        currentRotation += mouseX * rotationSpeed;
        currentVerticalAngle -= mouseY * verticalSpeed;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);

        // ī�޶� ��ġ ������Ʈ
        Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentRotation, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);
        transform.position = target.position + offset;

        // ī�޶� ȸ�� ������Ʈ
        transform.rotation = Quaternion.Euler(currentVerticalAngle, currentRotation, 0f);
    }
}