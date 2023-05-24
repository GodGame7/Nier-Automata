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

    [Header("Enemy ����")]
    [SerializeField] protected State state;

    [Header("Enemy ü�� ����")]
    [Space(10f)]
    [SerializeField] private Slider Hpslider;
    [SerializeField] private float MaxHP;
    [SerializeField] private float CurrentHP;
    [Tooltip("������ �̻��� �������� ������")]
    [SerializeField] private float MinDamage;


    [Header("����� ȿ��")]
    [Space(10f)]
    [SerializeField] GameObject explosion_effect;

    [Header("������ ����")]
    [Space(10f)]
    [SerializeField] protected int pattonNum = 4;

    [Header("�����Ÿ� (������ 0����)")]
    [Space(10f)]
    [Tooltip("���� �ȿ� ���� �����ؿ�~")]
    [SerializeField] float attackDistance = 2.8f;
    [Tooltip("���� ���̸� �뽬�ؿ�~")]
    [SerializeField] float dashDistance = 6f;

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

    //Ÿ�ٰ��� �Ÿ�
    protected float distance;
    protected bool isdead = false;

    //Ÿ��
    [SerializeField] protected Transform target;

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

        //Ÿ���� �Ĵٺ�
        //�뽬 �� ��쿡�� �ٷ� �Ĵٺ���, �ƴѰ�� �ڿ������� ȸ��
        if (state == State.DASH)
        {
            transform.LookAt(targetPosition);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Ÿ�ٰ��� �Ÿ��� ����
        distance = Vector3.Distance(transform.position, target.transform.position);

    }

    protected virtual IEnumerator UpdateIdle()
    {
        if (state != State.IDLE)
        {
            state = State.IDLE;

            anim.SetBool("Run", false);
        }

        Debug.Log("���ڰ�..");
        yield return null;

        if (distance <= attackDistance)
        {
            state = State.ATTACK;
        }
        else if (distance >= dashDistance)
        {
            //�뽬�� ���� ���� 0
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


    //�������� ���� ��
    public void TakeDamage(int Damage)
    {

        //�� �ڿ� �ִ��� Ȯ��
        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 forwardDirection = transform.forward;
        bool isTargetInFront = Vector3.Dot(targetDirection, forwardDirection) > 0;
        anim.SetBool("FrontPlayer", isTargetInFront);

        currentHp -= Damage;

        //�� ������ �̻��̸� ��ݹ޾ƿ�~
        if (Damage >= MinDamage)
        {        
            hitNum++;
            anim.SetFloat("HitNum", hitNum);
            anim.SetTrigger("Hit");
        }

    }

    //���
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




