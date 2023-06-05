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
    [SerializeField]
    private float speedChangeRate = 10f;
    [Tooltip("���Ÿ� ���� ���� ������")]
    [SerializeField]
    private float fireDelay = 0.1f;


    //private PlayerData player;

    // �÷��̾�
    public float h { get; private set; }
    public float v { get; private set; }
    private float animationBlend;
    public int invincibleLayer { get; private set; }
    public int defaultLayer { get; private set; }
    private bool canMove = true;
    public float threshold = 1f;
    private readonly Vector3 defaultPos = new Vector3(0, 0.02f, 0);

    // �뽬
    public KeyCode lastKeyPressed { get; private set; }  // �뽬�� ���� ���� Ű�Է� ����
    private readonly KeyCode[] keysToCheck = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };    // �뽬�� ���� ���� �̵�Ű ����
    private float lastKeyPressTime = 0f;
    private readonly float timeAllowedBetweenKeyPresses = 0.5f; // ���� �Է����� �Ǵ��ϱ� ���� �ִ� �ð���

    // ����
    public bool isCombo { get; private set; }   // �Ǵ� ����� �޺�
    private float preInputDelay = 0.9f;         // ���Է� �����ð�

    // ����, ����
    private IFlagViewStrategy currentViewStrategy = new FlagTopViewMove();
    private IFlagModeStrategy currentModeStrategy = new ModeFlag();
    public IFlagState currentState = new FlagNomal();
    private FlagNomal nomalState;
    private FlagAttack attackState;
    private FlagDash dashState;
    private FlagTransformation transfomationState;


    //[Header("�ѤѤѤѤѤѤѤѤѤѤѤ�")]
    //[Space(50)]
    // �ִϸ��̼� �ؽ�
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

    // ������Ʈ
    private Animator anim;
    private Rigidbody rigid;
    private FlagBulletSpawner[] bulletSpawners = new FlagBulletSpawner[2];

    // ĳ��
    #region �ڷ�ƾ Wait
    public WaitUntil InputFireButton_wait { get; private set; }
    public WaitUntil ResetCombo_wait { get; private set; }
    public WaitUntil EnterDashAni_wait { get; private set; }
    public WaitUntil ExitDashAni_wait { get; private set; }
    public WaitUntil EnterAttackAni_wait { get; private set; }
    public WaitUntil ExitAttackAni_wait { get; private set; }
    public WaitUntil EnterTransformation_wait { get; private set; }
    public WaitUntil ExitTransformation_wait { get; private set; }
    public WaitUntil EnterGundamAttack1_wait { get; private set; }

    public WaitForSeconds FireDelay_wait { get; private set; }
    public WaitForSeconds AnimaReset_wait { get; private set; }
    #endregion �ڷ�ƾ Wait

    #region �ʱ�ȭ
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
        // �̺�Ʈ ���� (���� ��ȯ)
        Init();
        SetViewStrategy(new FlagTopViewMove());
    }
    private void OnDisable()
    {
        // �̺�Ʈ ���� (���� ��ȯ)
        StopCoroutine(nameof(Fire_co));
        StopCoroutine(nameof(ResetCombo_co));
        StopCoroutine(nameof(SetTransformationState_co));
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        //Cursor.lockState = CursorLockMode.Locked;
        // todo ��ư �����ߵż� ���߿� �ٲܰ�
        Cursor.lockState = CursorLockMode.None;
    }
    private void GetComponentAndCashing()
    {
        TryGetComponent(out anim);
        TryGetComponent(out rigid);
        bulletSpawners = GetComponentsInChildren<FlagBulletSpawner>();

        InputFireButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        EnterDashAni_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamDash"));
        ExitDashAni_wait = new WaitUntil(() => !(anim.GetCurrentAnimatorStateInfo(0).IsName("FlagDash") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamDash")));
        EnterAttackAni_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("HorizontalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("VerticalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlagStrongAttack") ||
                                                  anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamStrongAttack"));
        ExitAttackAni_wait = new WaitUntil(() => !(anim.GetCurrentAnimatorStateInfo(0).IsName("HorizontalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("VerticalWeakAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("FlagStrongAttack") ||
                                                  anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamStrongAttack")));
        EnterTransformation_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("ToFlag") || anim.GetCurrentAnimatorStateInfo(0).IsName("ToGundam"));
        ExitTransformation_wait = new WaitUntil(() => !(anim.GetCurrentAnimatorStateInfo(0).IsName("ToFlag") || anim.GetCurrentAnimatorStateInfo(0).IsName("ToGundam")));
        EnterGundamAttack1_wait = new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack1"));
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
    #endregion �ʱ�ȭ

    #region ����, ����
    // ����
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
        // ���� ��ȯ�ϸ� �÷��̾ �߾����� �ٽ� �̵���Ŵ
        StartCoroutine(MoveToCenter(0.5f));
        currentViewStrategy = strategy;
    }

    // ���
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
        // ��� ���濡 ���� �ִϸ��̼� ���
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

    // ����
    private void SetState(IFlagState state)
    {
        StopCoroutine(nameof(ReturnToNomalState_co));
        currentState = state;
    }
    public IEnumerator ReturnToNomalState_co(WaitUntil wait1 = null, WaitUntil wait2 = null)
    {
        yield return wait1; // �ִϸ��̼� ����, Ű�Է� �� 
        yield return wait2; // �ִϸ��̼� �Ϸ�, null ��

        SetState(nomalState);
    }
    private IEnumerator SetTransformationState_co()
    {
        while (true)
        {
            yield return EnterTransformation_wait;
            // ��� ��ȯ �ִϸ��̼� ����Ǹ� �÷��̾� ȸ���� �ʱ�ȭ (�Ǵ� ��忡�� ���ư��� ������)
            transform.rotation = Quaternion.Euler(-90f, 0, 0);
            SetState(transfomationState);
            yield return ExitTransformation_wait;
            // ��� ��ȯ �Ϸ� �� �븻 ���·� ����
            SetState(nomalState);
        }
    }
    #endregion ����, ����

    private void Update()
    {
        currentState.Action(this);  // ���¿� ���� ���̾� �����Ͽ� �������� ����

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
            currentModeStrategy.Rotate(this);
            Move();
        }
    }

    #region �̵�
    private void InputMoveKey()
    {
        // Ű�Է¿� ���� �̵� ���� ����
        // ���⸸ �����ϴ� ������ ������ ���� �̵��� ������ �޶� �̵� ��ü�� �ٸ� ������ ó���ϱ� ����
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
        float targetSpeed = moveSpeed;  // �뽬 �ӵ� �� ��� ���� ���� �̵��ӵ�
        Vector3 moveDir;
        // ������ ���� �̵� ���� ����
        currentViewStrategy.Move(this, out moveDir);    

        // �̵� ���⿡ ���� �ִϸ��̼� ����
        if (currentModeStrategy is ModeFlag)
        {
            animationBlend = Mathf.Lerp(animationBlend, moveDir.x, Time.deltaTime * speedChangeRate);
            if (Mathf.Abs(animationBlend) < 0.01f)
            {
                animationBlend = 0f;
            }
            anim.SetFloat(hashHSpeed, animationBlend);
        }
        else
        {
            if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.z))
            {
                animationBlend = Mathf.Lerp(animationBlend, Mathf.Abs(moveDir.x), Time.deltaTime * speedChangeRate);
            }
            else
            {
                animationBlend = Mathf.Lerp(animationBlend, Mathf.Abs(moveDir.z), Time.deltaTime * speedChangeRate);
            }
            if (animationBlend < 0.01f)
            {
                animationBlend = 0f;
            }
            anim.SetFloat(hashAniSpeed, animationBlend);
        }

        // �÷��̾� �̵�
        if (currentState is FlagDash)
        {
            targetSpeed *= dashSpeed;
        }
        Vector3 newPosition = new Vector3(
            Mathf.Clamp((rigid.position.x + targetSpeed * Time.deltaTime * moveDir.x), -0.3f, 0.3f),
            Mathf.Clamp((rigid.position.y + targetSpeed * Time.deltaTime * moveDir.y), -0.18f, 0.18f),
            Mathf.Clamp((rigid.position.z + targetSpeed * Time.deltaTime * moveDir.z), -0.15f, 0.15f));
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
    public IEnumerator ResetScaleX_co()
    {
        yield return EnterDashAni_wait;
        yield return ExitDashAni_wait;
        transform.localScale = Vector3.one;
    }

    // �������� �� �÷��̾� �߾����� ���� �̵���Ű�� �޼ҵ�
    public IEnumerator MoveToCenter(float speed)
    {
        canMove = false;

        while (Vector3.SqrMagnitude(transform.position - defaultPos) >= 0.0001f)
        {
            Vector3 direction = (defaultPos - transform.position).normalized;   // �̵��ؾ� �� ���� ���
            Vector3 moveVector = direction * speed * Time.deltaTime;            // �ӵ��� ����� �̵� �ؾ��� ��ġ ���

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
    #endregion �̵�

    #region ����
    private void Attack()
    {
        if (!currentState.Equals(attackState))
        {
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0))
            {
                // ����� ����� ������� ������ ���� �������� ���� �������� ���� �����ϱ� ���� ����
                bool isHorizontal;
                if (currentViewStrategy is FlagSideViewMove)
                {
                    isHorizontal = false;
                }
                else
                {
                    isHorizontal = true;
                }

                currentModeStrategy.WeakAttack(this, isHorizontal);
                SetState(attackState);
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
        // �Ǵ���� �����߿��� ����� �޺��� �ߵ� ����
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
                AudioManager.Instance.PlaySfx(Define.SFX.Shot);
            }
        }
    }
    public IEnumerator ResetCombo_co()
    {
        float waitTime = 5f;
        float startTime;
        while (true)
        {
            // �Ǵ� 1�� ����
            yield return EnterGundamAttack1_wait;
            isCombo = true;
            startTime = Time.time;
            // 2�� ������ ����ǰų� 1�� ������ ���� �� �� ���� ������ �޺� ����
            yield return new WaitUntil(() => (Time.time >= startTime + waitTime) || anim.GetCurrentAnimatorStateInfo(0).IsName("GundamWeakAttack2"));
            isCombo = false;
        }
    }
    public void PlayGundamAttackSfx()
    {
        AudioManager.Instance.PlaySfx(Define.SFX.GundamAttack);
    }
    #endregion ����

    public void SetAnimaTrigger(int hashAni)
    {
        // Ʈ���Ű� �ߵ��Ǳ� ������ On������ �ʰ� ���� �� �������� �ϱ� ���� �޼ҵ�
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
