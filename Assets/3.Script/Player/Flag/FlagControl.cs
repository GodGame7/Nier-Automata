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
    [SerializeField]
    private float damage = 5.0f;
    private float weakDamage = 2.0f;
    private float strong1Damage = 3.0f;
    private float strong2Damage = 3.1f;


    // 플레이어
    //private float animationBlend;
    //private float targetRotation = 0.0f;
    //private PlayerData player;


    private IFlagMoveStrategy currentStrategy;
    private IFlagState currentState;
    private FlagNomal nomalState;
    private FlagWeakAttack weakAttackState;
    private FlagStrongAttack1 strongAttackState1;
    private FlagStrongAttack2 strongAttackState2;
    private bool isStrongAttackCombo = false;

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

    // 캐싱
    private WaitUntil InputWeakAttackButton_wait;
    private WaitUntil InputStrongAttackButton_wait;
    private WaitUntil InputFireButton_wait;
    private WaitForSeconds FireDelay_wait;

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
        SetStrategy(new TopViewFlagMove());
        SetStrategy(new SideViewFlagMove());
        SetStrategy(new BackViewFlagMove());
        SetState(nomalState);
    }

    private void Update()
    {
        Move();
    }

    private void Init()
    {
        TryGetComponent(out anim);
        TryGetComponent(out controller);

        InputWeakAttackButton_wait = new WaitUntil(()=>Input.GetKey(KeyCode.Period) || Input.GetMouseButton(0));
        InputStrongAttackButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.Slash) || Input.GetMouseButton(1));
        InputFireButton_wait = new WaitUntil(() => Input.GetKey(KeyCode.LeftShift));
        FireDelay_wait = new WaitForSeconds(fireDelay);
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
        currentStrategy = strategy;
    }
    private void SetState(IFlagState state)
    {
        currentState = state;
    }
    public void ExecuteAttack()
    {
        currentState.Action();
    }

    private void Move()
    {
        Vector3 move;

        currentStrategy.Move(this, out move);

        anim.SetFloat(hashHSpeed, move.x);
        controller.Move(moveSpeed * Time.deltaTime * move);
    }

    private IEnumerator WeakAttack_co()
    {
        while (true)
        {
            yield return InputWeakAttackButton_wait;
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
                if (!isStrongAttackCombo)
                {
                    SetState(strongAttackState1);
                }
                else
                {
                    SetState(strongAttackState2);
                }
                ExecuteAttack();
                StartCoroutine(ReturnToNomalState_co(new WaitUntil(()=>anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f && (anim.GetCurrentAnimatorStateInfo(0).IsName("StrongAttack1")|| anim.GetCurrentAnimatorStateInfo(0).IsName("StrongAttack2")))));
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
