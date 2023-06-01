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
    [SerializeField]
    private float speedChangeRate = 10f;
    [Tooltip("원거리 공격 연사 딜레이")]
    [SerializeField]
    private float fireDelay = 0.1f;


    //private PlayerData player;

    // 플레이어
    public float h { get; private set; }
    public float v { get; private set; }
    private float animationBlend;
    public int invincibleLayer { get; private set; }
    public int defaultLayer { get; private set; }
    private bool canMove = true;
    public float threshold = 1f;
    private readonly Vector3 defaultPos = new Vector3(0, 0.02f, 0);

    // 대쉬
    public KeyCode lastKeyPressed { get; private set; }  // 대쉬를 위해 이전 키입력 저장
    private readonly KeyCode[] keysToCheck = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };    // 대쉬를 위해 비교할 이동키 모음
    private float lastKeyPressTime = 0f;
    private readonly float timeAllowedBetweenKeyPresses = 0.5f; // 연속 입력으로 판단하기 위한 최대 시간차

    // 공격
    public bool isCombo { get; private set; }   // 건담 약공격 콤보
    private float preInputDelay = 0.9f;         // 선입력 유지시간

    // 전략, 상태
    private IFlagViewStrategy currentViewStrategy = new FlagTopViewMove();
    private IFlagModeStrategy currentModeStrategy = new ModeFlag();
    public IFlagState currentState = new FlagNomal();
    private FlagNomal nomalState;
    private FlagAttack attackState;
    private FlagDash dashState;
    private FlagTransformation transfomationState;


    //[Header("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ")]
    //[Space(50)]
    // 애니매이션 해시
    public int hashHSpeed { get; private set; }
    public int hashAniSpeed { get; private set; }
    public int hashDash { get; private set; }
    public int hashToGundam { get; private set; }
    public int hashToFlag { get; private set; }
    public int hashVerticalWeakAttack { get; private set; }
    public int hashHorizontalWeakAttack { get; private set; }
    public int hashFlagStrongAttack { get; private set; }
    public int hashGundamWeakAttack1 { get; private set; }
    public int hashGundamWeakAttack2 { get; private set; }
    public int hashGundamStrongAttack { get; private set; }

    // 컴포넌트
    private Animator anim;
    private CharacterController controller;
    private Rigidbody rigid;
    private FlagBulletSpawner[] bulletSpawners = new FlagBulletSpawner[2];

    // 캐싱
    #region 코루틴 Wait
    public WaitUntil InputFireButton_wait { get; private set; }
    public WaitUntil EnterDashAni_wait { get; private set; }
    public WaitUntil ExitDashAni_wait { get; private set; }
    public WaitUntil EnterAttackAni_wait { get; private set; }
    public WaitUntil ExitAttackAni_wait { get; private set; }
    public WaitUntil ResetCombo_wait { get; private set; }
    public WaitUntil EnterTransformation_wait { get; private set; }
    public WaitUntil ExitTransformation_wait { get; private set; }

    public WaitForSeconds FireDelay_wait { get; private set; }
    public WaitForSeconds AnimaReset_wait { get; private set; }
    #endregion 코루틴 Wait


    #region 초기화
    private void Awake()
    {
        GetComponentAndCashing();
        GetAnimHash();

        invincibleLayer = LayerMask.NameToLayer("Invincible");
        defaultLayer = LayerMask.NameToLayer("Default");

        nomalState = new FlagNomal();
        attackState = new FlagAttack();
        dashState = new FlagDash();
        transfomationState = new FlagTransformation();
    }
    private void OnEnable()
    {
        // 이벤트 구독 (시점 변환)
        Init();
        SetViewStrategy(new FlagTopViewMove());
    }
    private void OnDisable()
    {
        // 이벤트 해제 (시점 변환)
        StopCoroutine(nameof(Fire_co));
        StopCoroutine(nameof(ResetCombo_co));
        StopCoroutine(nameof(SetTransformationState_co));
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        //Cursor.lockState = CursorLockMode.Locked;
        // todo 버튼 눌러야돼서 나중에 바꿀것
        Cursor.lockState = CursorLockMode.None;
    }
    private void GetComponentAndCashing()
    {
        TryGetComponent(out anim);
        TryGetComponent(out controller);
        TryGetComponent(out rigid);
        bulletSpawners = GetComponentsInChildren<FlagBulletSpawner>();

        InputFireButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.LeftShift));
        EnterDashAni_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamDash"));
        ExitDashAni_wait = new WaitUntil(() => !(anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamDash")));
        EnterAttackAni_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("HorizontalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("VerticalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlagStrongAttack") ||
                                                  anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamStrongAttack"));
        ExitAttackAni_wait = new WaitUntil(() => !(anim.GetCurrentAnimatorStateInfo(0).IsName("HorizontalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("VerticalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlagStrongAttack") ||
                                                  anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamStrongAttack")));
        EnterTransformation_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("ToFlag") || anim.GetCurrentAnimatorStateInfo(0).IsName("ToGundam"));
        ExitTransformation_wait = new WaitUntil(() => !(anim.GetCurrentAnimatorStateInfo(0).IsName("ToFlag") || anim.GetCurrentAnimatorStateInfo(0).IsName("ToGundam")));
        FireDelay_wait = new WaitForSeconds(fireDelay);
        AnimaReset_wait = new WaitForSeconds(preInputDelay);
        ResetCombo_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f);
    }
    private void GetAnimHash()
    {
        hashHSpeed = Animator.StringToHash("hSpeed");
        hashAniSpeed = Animator.StringToHash("aniSpeed");
        hashDash = Animator.StringToHash("dash");
        hashToGundam = Animator.StringToHash("toGundam");
        hashToFlag = Animator.StringToHash("toFlag");
        hashVerticalWeakAttack = Animator.StringToHash("verticalWeakAttack");
        hashHorizontalWeakAttack = Animator.StringToHash("horizontalWeakAttack");
        hashFlagStrongAttack = Animator.StringToHash("flagStrongAttack");
        hashGundamWeakAttack1 = Animator.StringToHash("gundamWeakAttack1");
        hashGundamWeakAttack2 = Animator.StringToHash("gundamWeakAttack2");
        hashGundamStrongAttack = Animator.StringToHash("gundamStrongAttack");
    }
    private void Init()
    {
        currentState = nomalState;
        isCombo = false;
        SetModeStrategy(new ModeFlag());

        StartCoroutine(nameof(Fire_co));
        StartCoroutine(nameof(ResetCombo_co));
        StartCoroutine(nameof(SetTransformationState_co));
    }
    #endregion 초기화

    #region 전략, 상태
    public void SetTopViewStrategy()
    {
        SetViewStrategy(new FlagTopViewMove());
    }
    public void SetBackViewStrategy()
    {
        SetViewStrategy(new FlagBackViewMove());
    }
    public void SetSideViewStrategy()
    {
        SetViewStrategy(new FlagSideViewMove());
    }
    private void SetViewStrategy(IFlagViewStrategy strategy)
    {
        // 시점 변환하면 플레이어를 중앙으로 다시 이동시킴
        StartCoroutine(MoveToCenter(0.5f));
        currentViewStrategy = strategy;
    }
    public void TransGundam()
    {
        SetModeStrategy(new ModeGundam());
    }
    public void TransFlag()
    {
        SetModeStrategy(new ModeFlag());
    }
    private void SetModeStrategy(IFlagModeStrategy strategy)
    {
        // 모드 변경에 따른 애니메이션 출력
        if (!(currentModeStrategy is ModeGundam) && strategy is ModeGundam)
        {
            anim.SetTrigger(hashToGundam);
        }
        else if (!(currentModeStrategy is ModeFlag) && strategy is ModeFlag)
        {
            anim.SetTrigger(hashToFlag);
        }

        currentModeStrategy = strategy;
    }
    private void SetState(IFlagState state)
    {
        StopCoroutine(nameof(ReturnToNomalState_co));
        currentState = state;
    }
    public IEnumerator ReturnToNomalState_co(WaitUntil startAnimation = null, WaitUntil endAnimation = null)
    {
        yield return startAnimation;
        yield return null;
        yield return endAnimation;

        SetState(nomalState);
    }
    public IEnumerator ReturnToNomalState_co(WaitForSeconds wait)
    {
        yield return wait;

        SetState(nomalState);
    }
    private IEnumerator SetTransformationState_co()
    {
        while (true)
        {
            yield return EnterTransformation_wait;
            transform.rotation = Quaternion.Euler(-90f, 0, 0);
            SetState(transfomationState);
            yield return ExitTransformation_wait;
            SetState(nomalState);
        }
    }
    #endregion 전략, 상태

    private void Update()
    {
        currentState.Action(this);  // 상태에 따라 레이어 변경하여 무적여부 결정

        if (!currentState.Equals(transfomationState) && canMove)
        {
            InputMoveKey();
            if (currentState.Equals(nomalState))
            {
                CheckDash();
            }
            Attack();
        }
    }
    private void FixedUpdate()
    {
        if (!currentState.Equals(transfomationState) && canMove)
        {
            if (currentModeStrategy is ModeGundam)
            {
                currentModeStrategy.Rotate(this);
            }
            Move();
        }
    }

    #region 이동
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
        float targetSpeed = moveSpeed;
        Vector3 move;
        currentViewStrategy.Move(this, out move);

        if (currentModeStrategy is ModeFlag)
        {
            animationBlend = Mathf.Lerp(animationBlend, move.x, Time.deltaTime * speedChangeRate);
            if (Mathf.Abs(animationBlend) < 0.01f)
            {
                animationBlend = 0f;
            }
            anim.SetFloat(hashHSpeed, animationBlend);
        }
        else
        {
            if (Mathf.Abs(move.x) > Mathf.Abs(move.z))
            {
                animationBlend = Mathf.Lerp(animationBlend, Mathf.Abs(move.x), Time.deltaTime * speedChangeRate);
            }
            else
            {
                animationBlend = Mathf.Lerp(animationBlend, Mathf.Abs(move.z), Time.deltaTime * speedChangeRate);
            }
            if (animationBlend < 0.01f)
            {
                animationBlend = 0f;
            }
            anim.SetFloat(hashAniSpeed, animationBlend);
        }


        if (currentState is FlagDash)
        {
            targetSpeed *= dashSpeed;
        }
        Vector3 newPosition = new Vector3(Mathf.Clamp((rigid.position.x + targetSpeed * Time.deltaTime * move.x), -0.3f, 0.3f), Mathf.Clamp((rigid.position.y + targetSpeed * Time.deltaTime * move.y), -0.18f, 0.18f), Mathf.Clamp((rigid.position.z + targetSpeed * Time.deltaTime * move.z), -0.15f, 0.15f));
        rigid.MovePosition(newPosition);
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
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash"));
        yield return null;
        yield return new WaitUntil(() => !anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash"));
        transform.localScale = Vector3.one;
    }

    public IEnumerator MoveToCenter(float speed)
    {
        canMove = false;

        while (Vector3.SqrMagnitude(transform.position - defaultPos) >= 0.0004f)
        {
            Vector3 direction = (defaultPos - transform.position).normalized; // 방향 벡터 계산
            Vector3 moveVector = direction * speed * Time.deltaTime; // 이동 벡터 계산

            rigid.MovePosition(transform.position + moveVector);
            yield return null;
        }
        transform.position = defaultPos;
        canMove = true;
    }
    public void StopMove()
    {
        canMove = false;
    }
    public void CanMove()
    {
        canMove = true;
    }
    #endregion 이동

    #region 공격
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
                StartCoroutine(ReturnToNomalState_co(EnterAttackAni_wait, ExitAttackAni_wait));
            }
            if (Input.GetKeyDown(KeyCode.Slash) || Input.GetMouseButtonDown(1))
            {
                SetState(attackState);
                currentModeStrategy.StrongAttack(this);
                StopCoroutine(nameof(ReturnToNomalState_co));
                StartCoroutine(ReturnToNomalState_co(EnterAttackAni_wait, ExitAttackAni_wait));
            }
        }
        else if (currentModeStrategy is ModeGundam)
        {
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0))
            {
                SetState(attackState);
                currentModeStrategy.WeakAttack(this);
                StopCoroutine(nameof(ReturnToNomalState_co));
                StartCoroutine(ReturnToNomalState_co(EnterAttackAni_wait, ExitAttackAni_wait));
            }
        }
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
    public IEnumerator ResetCombo_co()
    {
        float waitTime = 5f;
        float startTime;
        while (true)
        {
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1"));
            isCombo = true;
            startTime = Time.time;
            yield return new WaitUntil(() => (Time.time >= startTime + waitTime) || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2"));
            isCombo = false;
        }
    }
    #endregion 공격

    public void SetAnimaTrigger(int hashAni)
    {
        anim.SetTrigger(hashAni);
        StopCoroutine(nameof(ResetAnimaTrigger_co));
        StartCoroutine(nameof(ResetAnimaTrigger_co), hashAni);
    }
    private IEnumerator ResetAnimaTrigger_co(int hashAni)
    {
        yield return AnimaReset_wait;
        anim.ResetTrigger(hashAni);
    }
}
