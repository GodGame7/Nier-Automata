using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagControl : MonoBehaviour
{
    [Header("플레이어 세팅")]
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float turnSpeed = 2.7f;
    [SerializeField]
    private float fireDelay = 0.1f;


    // 플레이어
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;

    private IFlagMoveStrategy _currentStrategy;

    // 애니매이션 해시
    private int hashHSpeed;
    private int hashTurn;
    private int hashToGundam;
    private int hashToFlag;
    private int hashAttack;

    // 컴포넌트
    private Animator anim;
    private CharacterController controller;
    private GameObject mainCamera;

    private const float _threshold = 0.01f;


    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        Init();
        GetAnimHash();
    }

    private void Start()
    {
        SetStrategy(new TopViewFlagMove());
        SetStrategy(new SideViewFlagMove());
        SetStrategy(new BackViewFlagMove());
    }

    private void Update()
    {
        Move();
    }

    private void Init()
    {
        TryGetComponent(out anim);
        TryGetComponent(out controller);
    }

    private void GetAnimHash()
    {
        hashHSpeed = Animator.StringToHash("hSpeed");
        hashTurn = Animator.StringToHash("turn");
        hashToGundam = Animator.StringToHash("toGundam");
        hashToFlag = Animator.StringToHash("toFlag");
        hashAttack = Animator.StringToHash("attack");
    }

    public void SetStrategy(IFlagMoveStrategy strategy)
    {
        _currentStrategy = strategy;
    }

    private void Move()
    {
        Vector3 move;

        _currentStrategy.Move(this, out move);

        anim.SetFloat(hashHSpeed, move.x);
        controller.Move(moveSpeed * Time.deltaTime * move);
    }
}
