using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    public float moveSpeed = 2.0f;
    public float sprintSpeed = 2.7f;
    [Range(0.0f, 0.3f)]
    [Tooltip("ĳ���� ȸ�� �ӵ�")]
    public float rotationSmoothTime = 0.12f;
    [Tooltip("�����ӵ�")]
    public float speedChangeRate = 10f;

    [Space(10)]
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;
    // ���� �ִϸ��̼� ���� �ð�
    // => ��� �������� ������� ����
    [Range(0.0f, 0.2f)]
    [Tooltip("���� ���� �����ϴ� �ð�")]
    public float fallTime = 0.05f;

    [Header("�� üũ")]
    private bool isGrounded = true;
    // ??
    public float groundedOffset = -0.14f;
    // ��üũ �ݰ�
    private float groundedRadius = 0.2f;
    [Tooltip("������ �Ǵ��� ���̾��")]
    public LayerMask GroundLayers;

    [Header("�ó׸ӽ� ī�޶�")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("������ �Ʒ� �ٶ󺸴� ����")]
    public float TopClamp = 70.0f;
    [Tooltip("�Ʒ����� �� �ٶ󺸴� ����")]
    public float BottomClamp = -30.0f;
    [Range(0.5f, 2.0f)]
    [Tooltip("ī�޶� ���� ����")]
    public float CameraVerticalSensitivity = 1.0f;
    [Range(1.0f, 4.0f)]
    [Tooltip("ī�޶� ���� ����")]
    public float CameraHorizontalSensitivity = 2.0f;
    [Tooltip("ī�޶� ����")]
    public bool LockCameraPosition = false;


    // �ó׸ӽ�
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // �÷��̾�
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    // �ִϸ��̼� �ؽ�
    private int hashSpeed;
    private int hashGrounded;
    private int hashJump;
    private int hashFreeFall;
    private int hashMotionSpeed;


    //private PlayerInput _playerInput;
    private Animator anim;
    private CharacterController controller;
    //private StarterAssetsInputs input;
    private GameObject mainCamera;

    private const float _threshold = 0.01f;


    private void Awake()
    {
        if(mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        
        TryGetComponent(out anim);
        TryGetComponent(out controller);
        //TryGetComponent(input);

        GetAnimHash();

        _fallTimeoutDelta = fallTime;
    }

    private void Update()
    {
        Move();
    }
    private void LateUpdate()
    {
        CameraRotation();
    }

    private void GetAnimHash()
    {
        hashSpeed = Animator.StringToHash("Speed");
        hashGrounded = Animator.StringToHash("Grounded");
        hashJump = Animator.StringToHash("Jump");
        hashFreeFall = Animator.StringToHash("FreeFall");
        hashMotionSpeed = Animator.StringToHash("MotionSpeed");
    }
    private void Move()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * sprintSpeed : moveSpeed;
        if(move.Equals(Vector2.zero))
        {
            targetSpeed = 0f;
        }

        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
        float speedOffset = 0.1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // Ŀ��
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed,
                Time.deltaTime * speedChangeRate);
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
        if (_animationBlend < 0.01f)
        {
            _animationBlend = 0f;
        }
        Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;
        if (move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);

            // ī�޶� ���� �������� ȸ��
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        anim.SetFloat(hashSpeed, _animationBlend);
        anim.SetFloat(hashMotionSpeed, 1);
    }
    private void CameraRotation()
    {
        // ���콺 �Է¿� ���� ȸ����
        Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            _cinemachineTargetYaw += look.x * CameraHorizontalSensitivity;
            _cinemachineTargetPitch -= look.y * CameraVerticalSensitivity;
        }
        
        // 360�� ����
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // ī�޶� ����
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
