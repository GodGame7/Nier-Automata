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
    private FlagAttack attackState;
    private FlagDash dashState;
    private bool isAttackCombo = false;

    // 애니매이션 해시
    public int hashHSpeed;
    public int hashDash;
    public int hashToGundam;
    public int hashToFlag;
    public int hashWeakAttack1;
    public int hashWeakAttack2;
    public int hashStrongAttack;

    // 컴포넌트
    public Animator anim;
    private CharacterController controller;
    private GameObject mainCamera;
    private Rigidbody rigid;
    private BulletControl[] bullets = new BulletControl[2];

    private const float _threshold = 0.01f;

    // 캐싱
    private WaitUntil InputWeakAttackButton_wait;
    private WaitUntil InputStrongAttackButton_wait;
    private WaitUntil InputFireButton_wait;
    public WaitUntil EndDash_wait;
    public WaitUntil EndAttack_wait;
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
        attackState = new FlagAttack();
        dashState = new FlagDash();

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

        InputWeakAttackButton_wait = new WaitUntil(() => Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0));
        InputStrongAttackButton_wait = new WaitUntil(() => Input.GetKeyDown(KeyCode.Slash) || Input.GetMouseButtonDown(1));
        InputFireButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.LeftShift));
        EndDash_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                                             (anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamDash")));
        EndAttack_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                                             (anim.GetCurrentAnimatorStateInfo(0).IsName("FlagWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlagStrongAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamStrongAttack")));
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
        InputMoveKey();
        Move();
        // todo 여기 상태 체크
        if (currentState.Equals(nomalState))
        {
            CheckDash();
        }
        // 공격 입력 판단 => 코루틴 말고 메소드 써야되나?
        // ㄴ 일단 Fire는 코루틴 유지 => 어차피 상태랑 상관 없는애임

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
        currentDirectX = (int)move.x;

        animationBlend = Mathf.Lerp(animationBlend, move.x, Time.deltaTime * speedChangeRate);
        if (Mathf.Abs(animationBlend) < 0.01f)
        {
            animationBlend = 0f;
        }

        anim.SetFloat(hashHSpeed, animationBlend);
        controller.Move(moveSpeed * Time.deltaTime * move);

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
    

    public IEnumerator ResetScaleX_co()
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && (anim.GetCurrentAnimatorStateInfo(0).IsName("FlagTurn")));
        transform.localScale = Vector3.one;
    }
    public IEnumerator ResetAnimaTrigger_co(int hashAni)
    {
        yield return AnimaReset_wait;
        anim.ResetTrigger(hashAni);
    }
    private IEnumerator WeakAttack_co()
    {
        while (true)
        {
            yield return InputWeakAttackButton_wait;
            yield return null;

            currentModeStrategy.WeakAttack(this);

            SetState(attackState);
            ExecuteAttack();
            StartCoroutine(ReturnToNomalState_co());//new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                                            //anim.GetCurrentAnimatorStateInfo(0).IsName("WeakAttack"))));
        }
    }
    private IEnumerator StrongAttack_co()
    {
        while (true)
        {
            yield return InputStrongAttackButton_wait;
            yield return null;

            currentModeStrategy.StrongAttack(this);

            SetState(attackState); ExecuteAttack();
            StartCoroutine(ReturnToNomalState_co(new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                                            (anim.GetCurrentAnimatorStateInfo(0).IsName("StrongAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("StrongAttack2")))));
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
    public IEnumerator ReturnToNomalState_co(WaitUntil waitAnimationEnd = null)
    {
        yield return waitAnimationEnd;
        //yield return null;
        Debug.Log("노말됨");
        SetState(nomalState);
    }
}
