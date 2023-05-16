using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagControl : MonoBehaviour
{
    [Header("플레이어 세팅")]
    public float moveSpeed = 2.0f;
    public float turnSpeed = 2.7f;

    // 플레이어
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;


    // 애니매이션 해시
    private int hashHSpeed;
    private int hashTurn;
    private int hashToGundam;
    private int hashToFlag;
    private int hashAttack;


    //private PlayerInput _playerInput;
    private Animator anim;
    private CharacterController controller;
    //private StarterAssetsInputs input;
    private GameObject mainCamera;

    private const float _threshold = 0.01f;

    private Vector3 scale;


    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        scale = transform.localScale;
    }

    private void Start()
    {
        TryGetComponent(out anim);
        TryGetComponent(out controller);
        //TryGetComponent(input);

        GetAnimHash();
    }

    private void Update()
    {
        Move1();
    }

    private void GetAnimHash()
    {
        hashHSpeed = Animator.StringToHash("hSpeed");
        hashTurn = Animator.StringToHash("turn");
        hashToGundam = Animator.StringToHash("toGundam");
        hashToFlag = Animator.StringToHash("toFlag");
        hashAttack = Animator.StringToHash("attack");
    }
    private void Move1()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float targetSpeed = moveSpeed;

        if (move.Equals(Vector2.zero))
        {
            targetSpeed = 0f;
        }
        _speed = targetSpeed;

        controller.Move(_speed * Time.deltaTime * move);

        if (move.x * scale.x < 0)
        {
            scale.x *= -1;
            transform.localScale = scale;
        }
        anim.SetFloat(hashHSpeed, move.x);
    }
}
