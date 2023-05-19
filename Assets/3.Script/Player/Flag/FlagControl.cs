using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagControl : MonoBehaviour
{
    [Header("플레이어 세팅")]
    [Tooltip("플레이어 이동 속도")]
    [SerializeField]
    private float moveSpeed = 2.0f;
    [Tooltip("플레이어 대쉬 속도")]
    [SerializeField]
    private float dashSpeed = 2.7f;
    [Tooltip("플레이어 가감속도")]
    public float speedChangeRate = 10f;
    [Tooltip("원거리 공격 연사 딜레이")]
    [SerializeField]
    private float fireDelay = 0.1f;
    [Tooltip("플레이어 공격력")]
    [SerializeField]
    private float damage = 5.0f;
    private float weakDamage = 2.0f;
    private float strong1Damage = 3.0f;
    private float strong2Damage = 3.1f;


    // 플레이어
    // 움직임 자연스럽게 이어지도록 하기 위한 변수
    private float animationBlend;
    private float currentSpeedX = 0;
    public float h;
    public float v;
    //private float targetRotation = 0.0f;
    //private PlayerData player;

    // 대쉬
    private KeyCode lastKeyPressed = KeyCode.None;
    private KeyCode[] keysToCheck = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };
    private float lastKeyPressTime = 0f;
    private float timeAllowedBetweenKeyPresses = 0.5f;
    public int currentDirectX = 0;
    private bool isDashWaiting = false;

    private IFlagViewStrategy currentViewStrategy;
    private IFlagModeStrategy currentModeStrategy;
    private IFlagState currentState;
    private FlagNomal nomalState;
    private FlagWeakAttack weakAttackState;
    private FlagStrongAttack1 strongAttackState1;
    private FlagStrongAttack2 strongAttackState2;
    private bool isAttackCombo = false;

    // 애니매이션 해시
    private int hashHSpeed;
    private int hashDash;
    private int hashToGundam;
    private int hashToFlag;
    private int hashWeakAttack1;
    private int hashWeakAttack2;
    private int hashStrongAttack;

    // 컴포넌트
    private Animator anim;
    private CharacterController controller;
    private GameObject mainCamera;
    private Rigidbody rigid;

    private const float _threshold = 0.01f;

    // 캐싱
    private WaitUntil InputWeakAttackButton_wait;
    private WaitUntil InputStrongAttackButton_wait;
    private WaitUntil InputFireButton_wait;
    private WaitForSeconds FireDelay_wait;
    private WaitForSeconds AnimaReset_wait;

    #region 초기화
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        Init();
        GetAnimHash();

        nomalState = new FlagNomal();
        weakAttackState = new FlagWeakAttack();
        strongAttackState1 = new FlagStrongAttack1();
        strongAttackState2 = new FlagStrongAttack2();

        currentState = nomalState;
    }

    private void OnEnable()
    {
        // 이벤트 구독 (시점 변환)
        StartCoroutine(nameof(WeakAttack_co));
        StartCoroutine(nameof(StrongAttack_co));
        StartCoroutine(nameof(Fire_co));
    }
    private void OnDisable()
    {
        // 이벤트 해제 (시점 변환)
        StopCoroutine(nameof(WeakAttack_co));
        StopCoroutine(nameof(StrongAttack_co));
        StopCoroutine(nameof(Fire_co));
    }

    private void Start()
    {
        SetViewStrategy(new FlagBackViewMove());
        SetViewStrategy(new FlagSideViewMove());
        SetViewStrategy(new GundamTopViewMove());
        SetViewStrategy(new FlagTopViewMove());
        SetState(nomalState);

        SetModeStrategy(new ModeGundam());
        SetModeStrategy(new ModeFlag());
    }
    private void Init()
    {
        TryGetComponent(out anim);
        TryGetComponent(out controller);
        TryGetComponent(out rigid);

        InputWeakAttackButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.Period) || Input.GetMouseButton(0));
        InputStrongAttackButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.Slash) || Input.GetMouseButton(1));
        InputFireButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.LeftShift));
        FireDelay_wait = new WaitForSeconds(fireDelay);
        AnimaReset_wait = new WaitForSeconds(0.5f);
    }
    private void GetAnimHash()
    {
        hashHSpeed = Animator.StringToHash("hSpeed");
        hashDash = Animator.StringToHash("dash");
        hashToGundam = Animator.StringToHash("toGundam");
        hashToFlag = Animator.StringToHash("toFlag");
        hashWeakAttack1 = Animator.StringToHash("weakAttack1");
        hashWeakAttack2 = Animator.StringToHash("weakAttack2");
        hashStrongAttack = Animator.StringToHash("strongAttack");
    }

    #endregion 초기화
    #region 전략, 상태
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
    public void ExecuteAttack()
    {
        currentState.Action();
    }
    #endregion 전략, 상태

    private void Update()
    {
        InputKey();
        Move();
    }

    private void InputKey()
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
        currentDirectX = (int)move.x;
        Debug.Log(currentDirectX);

        animationBlend = Mathf.Lerp(animationBlend, move.x, Time.deltaTime * speedChangeRate);
        if (Mathf.Abs(animationBlend) < 0.01f)
        {
            animationBlend = 0f;
        }

        anim.SetFloat(hashHSpeed, animationBlend);
        controller.Move(moveSpeed * Time.deltaTime * move);

        CheckDash();
    }
    private void CheckDash()
    {
        // 방향키만 두번 입력 체크함
        foreach (KeyCode key in keysToCheck)
        {
            if (Input.GetKeyDown(key))
            {
                // 일정 시간 안에 같은 방향키를 두번 눌렀으면 대쉬
                if (lastKeyPressed == key && Time.time - lastKeyPressTime <= timeAllowedBetweenKeyPresses)
                {
                    Dash();
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
    private void Dash()
    {
        Vector3 playerScale = transform.localScale;

        // 왼쪽 대쉬시 스케일을 통해 애니메이션 반전
        if (currentDirectX < 0)
        {
            playerScale.x = -10;
            if (!transform.localScale.x.Equals(playerScale.x))
            {
                transform.localScale = playerScale;
                StartCoroutine(nameof(ResetScaleX_co));
            }
        }
        else
        {
            playerScale.x = 10;
            if (!transform.localScale.x.Equals(playerScale.x))
            {
                transform.localScale = playerScale;
            }
        }
        anim.SetTrigger(hashDash);
        StopCoroutine(nameof(ResetAnimaTrigger_co));
        StartCoroutine(nameof(ResetAnimaTrigger_co),(hashDash));
    }


    private IEnumerator ResetScaleX_co()
    {
        yield return new WaitUntil(()=>anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && (anim.GetCurrentAnimatorStateInfo(0).IsName("FlagTurn")));
        transform.localScale = 10 * Vector3.one;
    }
    private IEnumerator ResetAnimaTrigger_co(int hashAni)
    {
        yield return AnimaReset_wait;
        anim.ResetTrigger(hashAni);
    }
    private IEnumerator WeakAttack_co()
    {
        while (true)
        {
            yield return InputWeakAttackButton_wait;

            currentModeStrategy.WeakAttack1(this);

            if (currentState is FlagNomal)
            {
                SetState(weakAttackState);
                ExecuteAttack();
                StartCoroutine(ReturnToNomalState_co(new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f && anim.GetCurrentAnimatorStateInfo(0).IsName("WeakAttack"))));
            }
        }
    }
    private IEnumerator StrongAttack_co()
    {
        while (true)
        {
            yield return InputStrongAttackButton_wait;

            if (currentState is FlagNomal)
            {
                if (!isAttackCombo)
                {
                    SetState(strongAttackState1);
                }
                else
                {
                    SetState(strongAttackState2);
                }
                ExecuteAttack();
                StartCoroutine(ReturnToNomalState_co(new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f && (anim.GetCurrentAnimatorStateInfo(0).IsName("StrongAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("StrongAttack2")))));
            }
            else if (currentState is FlagStrongAttack1)
            {
                SetState(strongAttackState2);
            }


        }
    }
    private IEnumerator Fire_co()
    {
        while (true)
        {
            yield return FireDelay_wait;
            yield return InputFireButton_wait;

            Debug.Log("발사");
        }
    }
    private IEnumerator ReturnToNomalState_co(WaitUntil waitAnimationEnd)
    {
        //yield return waitAnimationEnd;
        yield return null;
        SetState(nomalState);
    }
}
