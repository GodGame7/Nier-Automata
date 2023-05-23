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

    [Header("���� ����")]
    [SerializeField] protected State state;


    [Header("Enemy ü��")]
    [SerializeField] private float MaxHP;
    [SerializeField] private float CurrentHP;

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
    Transform target;

    //���ݿ� �ݶ��̴�
    //BoxCollider[] boxCollider;

    //�ǰݿ� �ݶ��̴�
    CapsuleCollider capsuleCollider;

    protected Animator anim;
    protected AnimatorClipInfo[] animatorinfo;

    [Header("������ ����")]
    [SerializeField] protected int pattonNum = 4;
    [Header("�����Ÿ� (������ 0���� �������ּ���)")]
    [SerializeField] float attackDistance = 2.8f;
    [SerializeField] float DashDistance = 6f;
     
    Vector3 targetPosition;

    public delegate void Del();

    private void Awake()
    {
        //boxCollider = GetComponentsInChildren<BoxCollider>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        TryGetComponent(out anim);
        target = GameObject.FindGameObjectWithTag("Player").transform;

        currentHp = maxHp;
    }

    void Start()
    {
        //�ݶ��̴� ���� => �ٵ� �ȉ�
        //for (int i = 0; i < boxCollider.Length; i++)
        //{
        //    boxCollider[i].enabled = false;
        //}

        //Ÿ�� ��ġ ã��
        state = State.IDLE;
    }

    public void TargetLookat()
    {
        targetPosition = target.position;
        targetPosition.y = 0f;

        // Ÿ�� ������ �ٶ󺸵��� ȸ��
        transform.LookAt(targetPosition);

        //Ÿ�ٰ� �Ÿ��� ����
        distance = Vector3.Distance(transform.position, target.transform.position);
    }

    public void Distance()
    {
        float target_distance = transform.localPosition.z - target.transform.position.z;

        if (target_distance <= 0f)
        {
            anim.SetBool("FrontPlayer", false);
        }
        else
        {
            anim.SetBool("FrontPlayer", true);
        }
    }


    protected virtual IEnumerator UpdateIdle2()
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


    protected virtual IEnumerator UpdateDash2()
    {
        if (state != State.DASH)
        {
            state = State.DASH;
        }

        anim.SetTrigger("Dash");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"));
        //yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));

        state = State.IDLE;

    }

    protected virtual IEnumerator UpdateAttack2(int PattonNum)
    {
        if (state != State.ATTACK)
        {
            state = State.ATTACK;
        }

        int value = Random.Range(1, PattonNum + 1);
        anim.SetFloat("Patton", value);

        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"));
        //yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));
        anim.SetFloat("Patton", 0);

        state = State.IDLE;
    }




    protected virtual void UpdateWalk()
    {
        //�ȱ�
        anim.SetBool("Run", true);

        state = State.IDLE;
        //else if (distance >= 6f)
        //{
        //    state = State.DASH;
        //}
    }


    public void TakeDamage(int Damage)
    {
        currentHp -= Damage;

        if (Vector3.Distance(transform.position, target.position) <= 0f)
        {
            anim.SetBool("FrontPlayer", false);
        }
        else
        {
            anim.SetBool("FrontPlayer", true);
        }

        hitNum++;
        anim.SetFloat("HitNum", hitNum);
        anim.SetTrigger("Hit");
    }


    //protected virtual void UpdateAttack(int PattonNum)
    //{
    //    int value = Random.Range(1, PattonNum + 1);
    //    anim.SetFloat("Patton", value);

    //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
    //    {
    //        state = State.IDLE;
    //        return;
    //    }
    //}


}




