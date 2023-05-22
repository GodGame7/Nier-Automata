using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected enum State
    {
        IDLE,
        WALK,
        ATTACK
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

    //��ü �ݶ��̴�
    CapsuleCollider capsuleCollider;

    protected Animator anim;
    protected AnimatorClipInfo[] animatorinfo;

    [Header("������ ����")]
    [SerializeField] protected int pattonNum = 4;
    [Header("���� �����Ÿ�")]
    [SerializeField] float attackDistance = 2.8f;
    [Header("Idle Delay")]
    [SerializeField] float waitdelay;

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
        Debug.Log(anim.GetBool("FrontPlayer"));
    }

    public void UpdateIdle()
    {
        float Timer = 0; 
        Timer += Time.time;

        Debug.Log(Timer);

        if (Timer >= 3f)
        {
            state = State.WALK;
        }
    }


    //����
    protected virtual IEnumerator UpdateWalk(WaitUntil waitAnimationEnd = null)
    {
        anim.SetBool("Run", true);

        //���� �Ÿ��� 3f ���ϸ� �����ϰ�, 6f �̻��̸� �뽬��
        if (distance < attackDistance)
        {
            anim.SetBool("Run", false);
            yield return waitAnimationEnd;
            yield return null;
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(""));
        }
    }

    protected virtual void UpdateWalk()
    {
        //�ȱ�
        anim.SetBool("Run", true);

        //���� �Ÿ��� 3f ���ϸ� �����ϰ�, 6f �̻��̸� �뽬��
        if (distance < attackDistance)
        {
            anim.SetBool("Run", false);
            state = State.ATTACK;
            return;
        }
        //else if (distance >= 6f)
        //{
        //    state = State.DASH;
        //}
    }

    protected virtual void UpdateAttack(int PattonNum)
    {
        int value = Random.Range(1, PattonNum+1);
        anim.SetFloat("Patton", value);

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            state = State.IDLE;
            return;
        }
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




    public IEnumerator Wait_co(float delay, Del func)
    {
        yield return new WaitForSeconds(delay);

        func?.Invoke();
    }


}
