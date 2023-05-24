using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    [Header("���� Enemy ����")]
    [SerializeField] protected State state;

    [Header("Enemy ü��")]
    [Space(10f)]
    [SerializeField] private float MaxHP;
    [SerializeField] private float CurrentHP;

    [Header("������ ����")]
    [Space(10f)]
    [SerializeField] protected int pattonNum = 4;

    [Header("�����Ÿ� (������ 0����)")]
    [Space(10f)]
    [Tooltip("���� �ȿ� ���� �����ؿ�~")]
    [SerializeField] float attackDistance = 2.8f;
    [Tooltip("���� ���̸� �뽬�ؿ�~")]
    [SerializeField] float DashDistance = 6f;

    [Header("ȸ���ӵ�")]
    [Space(10f)]
    [SerializeField] float rotationSpeed = 10f;

    //ü�� ������Ƽ
    public float maxHp => MaxHP;
    public float currentHp
    {
        get { return CurrentHP; }
        set
        {
            CurrentHP = value;

            if (CurrentHP <= 0f)
            {
                isdead = true;
                anim.SetTrigger("DieTrigger");
                anim.SetBool("Die", true);
                capsuleCollider.enabled = false;
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

    //Ÿ�ٰ��� �Ÿ�
    protected float distance;
    protected bool isdead = false;

    //Ÿ��
    protected Transform target;

    //���ݿ� �ݶ��̴�
    BoxCollider[] boxCollider;

    //�ǰݿ� �ݶ��̴�
    CapsuleCollider capsuleCollider;

    //�ִϸ�����
    protected Animator anim;
    protected AnimatorClipInfo[] animatorinfo;

    //Ÿ�� ��ġ
    Vector3 targetPosition;

    private void Awake()
    {
        boxCollider = GetComponentsInChildren<BoxCollider>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        TryGetComponent(out anim);
        target = GameObject.FindGameObjectWithTag("Player").transform;


        currentHp = maxHp;
        state = State.IDLE;
    }


    public void TargetLookat()
    {
        targetPosition = target.position;
        targetPosition.y = 0f;

        // Ÿ�� ������ �ٶ󺸵��� ȸ��
        //transform.LookAt(targetPosition);

        // �ڿ������� ȸ��
        if (state == State.DASH)
        {
            transform.LookAt(targetPosition);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        //Ÿ�ٰ� �Ÿ��� ����
        distance = Vector3.Distance(transform.position, target.transform.position);

    }

    protected virtual IEnumerator UpdateIdle()
    {
        if (state != State.IDLE)
        {
            state = State.IDLE;

            anim.SetBool("Run", false);
        }


        yield return null;

        if (distance <= attackDistance)
        {
            state = State.ATTACK;
        }
        else if (distance >= DashDistance)
        {
            //�뽬�� ���� ���� 0
            if (DashDistance == 0)
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

    protected virtual IEnumerator UpdateDash()
    {
        if (state != State.DASH)
        {
            state = State.DASH;
        }

        //�뽬 ���
        anim.SetTrigger("Dash");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"));
        //yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));

        //�ִϸ��̼��� Idle�� �Ǹ�, ���µ� Idle�� ��
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

        //���� �ʱ�ȭ �ǰ�
        anim.SetFloat("Patton", 0);

        //���� �ݶ��̴� ����
        for (int i = 0; i < boxCollider.Length; i++)
        {
            boxCollider[i].enabled = false;
        }

        //�ִϸ��̼��� Idle�� �Ǹ�, ���µ� Idle�� ��
        state = State.IDLE;
    }

    protected virtual void UpdateWalk()
    {
        anim.SetBool("Run", true);

        state = State.IDLE;
    }


    //�������� ���� ��
    public void TakeDamage(int Damage)
    {
        currentHp -= Damage;

        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 forwardDirection = transform.forward;

        bool isTargetInFront = Vector3.Dot(targetDirection, forwardDirection) > 0;

        anim.SetBool("FrontPlayer", isTargetInFront);

        hitNum++;
        anim.SetFloat("HitNum", hitNum);
        anim.SetTrigger("Hit");
    }

}




