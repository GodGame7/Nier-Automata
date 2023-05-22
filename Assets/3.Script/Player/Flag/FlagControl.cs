using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagControl : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    [Tooltip("�÷��̾� �̵� �ӵ�")]
    [SerializeField]
    private float moveSpeed = 2.0f;
    [Tooltip("�÷��̾� �뽬 �ӵ�")]
    [SerializeField]
    private float dashSpeed = 2.7f;
    [Tooltip("�÷��̾� �����ӵ�")]
    public float speedChangeRate = 10f;
    [Tooltip("���Ÿ� ���� ���� ������")]
    [SerializeField]
    private float fireDelay = 0.1f;
    [Tooltip("�÷��̾� ���ݷ�")]
    [SerializeField]
    private float damage = 5.0f;
    private float weakDamage = 2.0f;
    private float strong1Damage = 3.0f;
    private float strong2Damage = 3.1f;


    // �÷��̾�
    // ������ �ڿ������� �̾������� �ϱ� ���� ����
    private float animationBlend;
    private float currentSpeedX = 0;
    public float h;
    public float v;
    //private float targetRotation = 0.0f;
    //private PlayerData player;

    // �뽬
    public KeyCode lastKeyPressed = KeyCode.None;
    private KeyCode[] keysToCheck = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };
    private float lastKeyPressTime = 0f;
    private float timeAllowedBetweenKeyPresses = 0.5f;
    private bool isDashWaiting = false;

    private IFlagViewStrategy currentViewStrategy;
    private IFlagModeStrategy currentModeStrategy;
    private IFlagState currentState;
    private FlagNomal nomalState;
    private FlagAttack attackState;
    private FlagDash dashState;
    private bool isAttackCombo = false;

    // �ִϸ��̼� �ؽ�
    public int hashHSpeed;
    public int hashDash;
    public int hashToGundam;
    public int hashToFlag;
    public int hashVerticalWeakAttack;
    public int hashHorizontalWeakAttack;
    public int hashWeakAttack1;
    public int hashWeakAttack2;
    public int hashFlagStrongAttack;

    // ������Ʈ
    public Animator anim;
    private CharacterController controller;
    private GameObject mainCamera;
    private Rigidbody rigid;
    public FlagBulletSpawner[] bulletSpawners = new FlagBulletSpawner[2];

    private const float _threshold = 0.01f;

    // ĳ��
    private WaitUntil InputFireButton_wait;
    public WaitUntil EnterDashAni_wait;
    public WaitUntil EnterAttackAni_wait;
    private WaitForSeconds FireDelay_wait;
    private WaitForSeconds AnimaReset_wait;

    #region �ʱ�ȭ
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        Init();
        GetAnimHash();

        nomalState = new FlagNomal();
        attackState = new FlagAttack();
        dashState = new FlagDash();

        currentState = nomalState;
    }
    private void OnEnable()
    {
        // �̺�Ʈ ���� (���� ��ȯ)
        StartCoroutine(nameof(Fire_co));
    }
    private void OnDisable()
    {
        // �̺�Ʈ ���� (���� ��ȯ)
        StopCoroutine(nameof(Fire_co));
    }
    private void Start()
    {
        SetViewStrategy(new FlagBackViewMove());
        SetViewStrategy(new GundamTopViewMove());
        SetViewStrategy(new FlagTopViewMove());
        SetViewStrategy(new FlagSideViewMove());
        SetState(nomalState);

        SetModeStrategy(new ModeGundam());
        SetModeStrategy(new ModeFlag());
    }
    private void Init()
    {
        TryGetComponent(out anim);
        TryGetComponent(out controller);
        TryGetComponent(out rigid);
        bulletSpawners = GetComponentsInChildren<FlagBulletSpawner>();

        InputFireButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.LeftShift));
        EnterDashAni_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamDash"));
        EnterAttackAni_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("HorizontalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("VerticalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlagStrongAttack") ||
                                                  anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamStrongAttack"));
        FireDelay_wait = new WaitForSeconds(fireDelay);
        AnimaReset_wait = new WaitForSeconds(0.5f);
    }
    private void GetAnimHash()
    {
        hashHSpeed = Animator.StringToHash("hSpeed");
        hashDash = Animator.StringToHash("dash");
        hashToGundam = Animator.StringToHash("toGundam");
        hashToFlag = Animator.StringToHash("toFlag");
        hashVerticalWeakAttack = Animator.StringToHash("verticalWeakAttack");
        hashHorizontalWeakAttack = Animator.StringToHash("horizontalWeakAttack");
        hashWeakAttack1 = Animator.StringToHash("weakAttack1");
        hashWeakAttack2 = Animator.StringToHash("weakAttack2");
        hashFlagStrongAttack = Animator.StringToHash("flagStrongAttack");
    }
    #endregion �ʱ�ȭ

    #region ����, ����
    public void SetViewStrategy(IFlagViewStrategy strategy)
    {
        currentViewStrategy = strategy;
    }
    public void SetModeStrategy(IFlagModeStrategy strategy)
    {
        currentModeStrategy = strategy;
    }
    private void SetState(IFlagState state)
    {
        currentState = state;
    }
    #endregion ����, ����

    private void Update()
    {
        InputMoveKey();
        if (currentState.Equals(nomalState))
        {
            CheckDash();
        }
        Attack();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void InputMoveKey()
    {
        if (Input.GetKey(KeyCode.A))
        {
            h = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            h = 1;
        }
        else
        {
            h = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            v = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            v = -1;
        }
        else
        {
            v = 0;
        }
    }
    private void Move()
    {
        Vector3 move;
        currentViewStrategy.Move(this, out move);

        animationBlend = Mathf.Lerp(animationBlend, move.x, Time.deltaTime * speedChangeRate);
        if (Mathf.Abs(animationBlend) < 0.01f)
        {
            animationBlend = 0f;
        }

        anim.SetFloat(hashHSpeed, animationBlend);
        Vector3 newPosition = new Vector3(Mathf.Clamp((rigid.position.x + moveSpeed * Time.deltaTime * move.x), -0.27f, 0.27f), Mathf.Clamp((rigid.position.y + moveSpeed * Time.deltaTime * move.y), -0.18f, 0.18f), Mathf.Clamp((rigid.position.z + moveSpeed * Time.deltaTime * move.z), -0.15f, 0.15f));
        rigid.MovePosition(newPosition);
    }
    private void CheckDash()
    {
        // ����Ű�� �ι� �Է� üũ��
        foreach (KeyCode key in keysToCheck)
        {
            if (Input.GetKeyDown(key))
            {
                // ���� �ð� �ȿ� ���� ����Ű�� �ι� �������� �뽬
                if (lastKeyPressed == key && Time.time - lastKeyPressTime <= timeAllowedBetweenKeyPresses)
                {
                    SetState(dashState);
                    currentModeStrategy.Dash(this);
                    lastKeyPressed = KeyCode.None;
                }
                else
                {
                    lastKeyPressed = key;
                }
                lastKeyPressTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        if (!currentState.Equals(attackState))
        {
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0))
            {
                bool isHorizontal;
                if (currentViewStrategy is FlagSideViewMove)
                {
                    isHorizontal = false;
                }
                else
                {
                    isHorizontal = true;
                }
                SetState(attackState);
                currentModeStrategy.WeakAttack(this, isHorizontal);
                StopCoroutine(nameof(ReturnToNomalState_co));
                StartCoroutine(nameof(ReturnToNomalState_co),EnterAttackAni_wait);
            }
            if (Input.GetKeyDown(KeyCode.Slash) || Input.GetMouseButtonDown(1))
            {
                SetState(attackState);
                currentModeStrategy.StrongAttack(this);
                StopCoroutine(nameof(ReturnToNomalState_co));
                StartCoroutine(nameof(ReturnToNomalState_co), EnterAttackAni_wait);
            }
        }
    }

    public IEnumerator ResetScaleX_co()
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash"));
        yield return null;
        yield return new WaitUntil(() => !anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash"));
        transform.localScale = Vector3.one;
    }
    public IEnumerator ResetAnimaTrigger_co(int hashAni)
    {
        yield return AnimaReset_wait;
        anim.ResetTrigger(hashAni);
    }
    private IEnumerator Fire_co()
    {
        while (true)
        {
            yield return FireDelay_wait;
            yield return InputFireButton_wait;

            foreach (FlagBulletSpawner b in bulletSpawners)
            {
                b.Fire();
            }
        }
    }
    public IEnumerator ReturnToNomalState_co(WaitUntil waitAnimationEnd = null)
    {
        yield return waitAnimationEnd;
        yield return null;
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("FlagIdle Move"));

        Debug.Log("�븻��");
        SetState(nomalState);
    }
}
