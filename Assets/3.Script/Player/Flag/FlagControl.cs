using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagControl : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    public float moveSpeed = 2.0f;
    public float turnSpeed = 2.7f;


    // �÷��̾�
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;

    private IFlagMoveStrategy _currentStrategy;

    // �ִϸ��̼� �ؽ�
    public int hashHSpeed;
    private int hashTurn;
    private int hashToGundam;
    private int hashToFlag;
    private int hashAttack;

    // ������Ʈ
    public Animator anim;
    public CharacterController controller;
    private GameObject mainCamera;

    private const float _threshold = 0.01f;

    public Vector3 scale;

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

        GetAnimHash();

        SetStrategy(new TopViewFlagMove());
    }

    private void Update()
    {
        Move();
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
        _currentStrategy.Move(this);        
    }
}
