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

    [Header("현재 상태")]
    [SerializeField] protected State state;


    [Header("Enemy 체력")]
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


    //타겟과의 거리
    protected float distance;
    protected bool isdead = false;

    //타겟
    Transform target;

    //공격용 콜라이더
    //BoxCollider[] boxCollider;

    //몸체 콜라이더
    CapsuleCollider capsuleCollider;

    protected Animator anim;
    protected AnimatorClipInfo[] animatorinfo;

    [Header("패턴의 갯수")]
    [SerializeField] protected int pattonNum = 4;
    [Header("공격 사정거리")]
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
        //콜라이더 끄기 => 근데 안됌
        //for (int i = 0; i < boxCollider.Length; i++)
        //{
        //    boxCollider[i].enabled = false;
        //}

        //타겟 위치 찾기
        state = State.IDLE;
    }

    public void TargetLookat()
    {
        targetPosition = target.position;
        targetPosition.y = 0f;

        // 타겟 방향을 바라보도록 회전
        transform.LookAt(targetPosition);

        //타겟과 거리를 측정
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


    //실험
    protected virtual IEnumerator UpdateWalk(WaitUntil waitAnimationEnd = null)
    {
        anim.SetBool("Run", true);

        //남은 거리가 3f 이하면 공격하고, 6f 이상이면 대쉬함
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
        //걷기
        anim.SetBool("Run", true);

        //남은 거리가 3f 이하면 공격하고, 6f 이상이면 대쉬함
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
