using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected enum State
    {
        IDLE,
        WALK,
        ATTACK,
        DASH,
        JUMP
    }

    [Header("Enemy 상태")]
    [SerializeField] protected State state;

    [Header("Enemy 체력 관련")]
    [Space(10f)]
    [SerializeField] private Slider Hpslider;
    [SerializeField] private float MaxHP;
    [SerializeField] private float CurrentHP;
    [Tooltip("이정도 이상의 데미지면 아파함")]
    [SerializeField] private float MinDamage;


    [Header("사망시 효과")]
    [Space(10f)]
    [SerializeField] GameObject explosion_effect;

    [Header("패턴의 갯수")]
    [Space(10f)]
    [SerializeField] protected int pattonNum = 4;

    [Header("사정거리 (없으면 0으로)")]
    [Space(10f)]
    [Tooltip("범위 안에 오면 공격해요~")]
    [SerializeField] float attackDistance = 2.8f;
    [Tooltip("범위 밖이면 대쉬해요~")]
    [SerializeField] float dashDistance = 6f;

    [Header("회전속도")]
    [Space(10f)]
    [SerializeField] float rotationSpeed = 10f;

    //체력 프로퍼티
    public float maxHp => MaxHP;
    public float currentHp
    {
        get { return CurrentHP; }
        set
        {
            CurrentHP = value;

            if (CurrentHP <= 0f)
            {
                StartCoroutine(Die());
            }
        }
    }

    private int HitNum;
    public int hitNum
    {
        get { return HitNum; }
        set
        {
            HitNum = value;

            if (value >= 4)
            {
                HitNum = 1;
            }
        }
    }

    //타겟과의 거리
    protected float distance;
    protected bool isdead = false;

    //타겟
    [SerializeField] protected Transform target;

    //공격용 콜라이더
    BoxCollider[] boxCollider;

    //피격용 콜라이더
    CapsuleCollider capsuleCollider;

    //애니메이터
    protected Animator anim;
    protected AnimatorClipInfo[] animatorinfo;

    //타겟 위치
    Vector3 targetPosition;


    private void Awake()
    {
        boxCollider = GetComponentsInChildren<BoxCollider>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();

        TryGetComponent(out anim);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void OnEnable()
    {
        currentHp = maxHp;
        state = State.IDLE;
    }


    public void TargetLookat()
    {
        targetPosition = target.position;
        targetPosition.y = 0f;

        //타겟을 쳐다봄
        //대쉬 할 경우에는 바로 쳐다보고, 아닌경우 자연스러운 회전
        if (state == State.DASH)
        {
            transform.LookAt(targetPosition);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //타겟과의 거리를 측정
        distance = Vector3.Distance(transform.position, target.transform.position);

    }

    protected virtual IEnumerator UpdateIdle()
    {
        if (state != State.IDLE)
        {
            state = State.IDLE;

            anim.SetBool("Run", false);
        }

        Debug.Log("가자고..");
        yield return null;

        if (distance <= attackDistance)
        {
            state = State.ATTACK;
        }
        else if (distance >= dashDistance)
        {
            //대쉬가 없는 경우는 0
            if (dashDistance == 0)
            {
                state = State.WALK;
            }
            else
            {
                state = State.DASH;
            }

        }
        else
        {
            state = State.WALK;
        }
    }

    protected virtual void UpdateWalk()
    {
        anim.SetBool("Run", true);

        state = State.IDLE;
    }

    protected virtual IEnumerator UpdateDash()
    {
        if (state != State.DASH)
        {
            state = State.DASH;
        }

        //대쉬 출력
        anim.SetTrigger("Dash");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"));
        //yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));

        //애니메이션이 Idle이 되면, 상태도 Idle이 됌
        state = State.IDLE;

    }

    protected virtual IEnumerator UpdateAttack(int PattonNum)
    {
        if (state != State.ATTACK)
        {
            state = State.ATTACK;
        }

        for (int i = 0; i < boxCollider.Length; i++)
        {
            boxCollider[i].enabled = true;
        }

        int value = Random.Range(1, PattonNum + 1);
        anim.SetFloat("Patton", value);


        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"));
        //yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));

        //패턴 초기화 되고
        anim.SetFloat("Patton", 0);

        //무기 콜라이더 끄고
        for (int i = 0; i < boxCollider.Length; i++)
        {
            boxCollider[i].enabled = false;
        }

        //애니메이션이 Idle이 되면, 상태도 Idle이 됌
        state = State.IDLE;
    }


    //데미지를 받을 때
    public void TakeDamage(int Damage)
    {

        //앞 뒤에 있는지 확인
        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 forwardDirection = transform.forward;
        bool isTargetInFront = Vector3.Dot(targetDirection, forwardDirection) > 0;
        anim.SetBool("FrontPlayer", isTargetInFront);

        currentHp -= Damage;

        //이 데미지 이상이면 충격받아요~
        if (Damage >= MinDamage)
        {        
            hitNum++;
            anim.SetFloat("HitNum", hitNum);
            anim.SetTrigger("Hit");
        }

    }

    //사망
    IEnumerator Die()
    {
        isdead = true;
        anim.SetTrigger("DieTrigger");
        anim.SetBool("Die", true);
        capsuleCollider.enabled = false;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Die") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        explosion_effect.SetActive(true);
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}




