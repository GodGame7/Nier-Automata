using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // �÷��̾� ������Ʈ�� Transform ������Ʈ

    public float rotationSpeed = 5f; // ī�޶� ȸ�� �ӵ�
    public float distance = 5f; // �÷��̾���� �Ÿ�

    private float currentRotation = 0f;

    private void LateUpdate()
    {
        // ī�޶� ��ġ ������Ʈ
        Vector3 offset = Quaternion.Euler(0f, currentRotation, 0f) * new Vector3(0f, 3f, -distance);
        transform.position = target.position + offset;

        // ī�޶� ȸ�� ������Ʈ
        currentRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        transform.LookAt(target);
    }
}