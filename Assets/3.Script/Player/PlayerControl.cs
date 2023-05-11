using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("플레이어 세팅")]
    public float moveSpeed = 2.0f;
    public float sprintSpeed = 2.7f;
    [Range(0.0f, 0.3f)]
    // 카메라 회전 속도 제한
    public float rotationSmoothTime = 0.12f;
    // ??
    public float speedChangeRate = 10f;

    [Space(10)]
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;
    // 낙하 애니메이션 돌입 시간
    // => 계단 내려갈때 점프모션 방지
    public float fallTime = 0.05f;

    [Header("땅 체크")]
    private bool isGrounded = true;
    // ??
    public float groundedOffset = -0.14f;
    // 땅체크 반경
    public float groundedRadius = 0.2f;
    public LayerMask GroundLayers;

    [Header("시네머신 카메라")]
    public GameObject CinemachineCameraTarget;
    // 아래 바라보는 각도
    public float TopClamp = 70.0f;
    // 위에 바라보는 각도
    public float BottomClamp = -30.0f;
    public bool LockCameraPosition = false;



    // 시네머신
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // 플레이어
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    // 애니매이션 해시
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
        hashSpeed = Animator.StringToHash("");
        hashGrounded = Animator.StringToHash("");
        hashJump = Animator.StringToHash("");
        hashFreeFall = Animator.StringToHash("");
        hashMotionSpeed = Animator.StringToHash("");
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
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed,
                Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
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
        // 추가
        if (move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        Debug.Log(_speed);
        controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

    }
    private void CameraRotation()
    {
        Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch -= look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
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
