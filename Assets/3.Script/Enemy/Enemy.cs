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

    [Header("���� ������ ��")]
    [Space(10f)]
    [SerializeField] protected int pattonNum = 4;
    [SerializeField] protected int damage = 4;

    [Header("�����Ÿ� (������ 0����)")]
    [Space(10f)]
    [Tooltip("���� �ȿ� ���� �����ؿ�~")]
    [SerializeField] float attackDistance = 2.8f;
    [Tooltip("���� ���̸� �뽬�ؿ�~")]
    [SerializeField] float dashDistance = 6f;

    [Header("ȸ���ӵ�")]
    [Space(10f)]
    [SerializeField] float rotationSpeed = 10f;


    //ü�� ���� �޾ƿ���
    protected EnemyHp enemyHp;

    //Ÿ�ٰ��� �Ÿ�
    protected float distance;

    //���ݿ� �ݶ��̴�
    BoxCollider[] boxCollider;


    //�ִϸ�����
    [HideInInspector] public Animator anim;
    //AnimatorClipInfo[] animatorinfo;

    //Ÿ�� ��ġ
    public Transform target;

    //Ÿ�� ��ġ�� �����ϴ� ����
    [SerializeField] protected Vector3 targetPosition;


    private void Awake()
    {

        //enemyHp = GetComponentInChildren<EnemyHp>();
        boxCollider = GetComponentsInChildren<BoxCollider>();

        TryGetComponent(out enemyHp);
        TryGetComponent(out anim);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void OnEnable()
    {

        state = State.IDLE;
    }

    //Ÿ�� �ٶ󺸱�
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

    //����
    protected virtual IEnumerator OnIdle()
    {
        if (state != State.IDLE)
        {
            Debug.Log("�������ֱ�");
            state = State.IDLE;

            anim.SetBool("Run", false);
        }

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

    //�ȱ�
    protected virtual void Onwalk()
    {
        anim.SetBool("Run", true);

        state = State.IDLE;
    }

    //�뽬
    protected virtual IEnumerator OnDash()
    {
        if (state != State.DASH)
        {
            state = State.DASH;
        }

        //�뽬 ���
        anim.SetTrigger("Dash");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle") ||
        anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Walk"));

        //�ִϸ��̼��� Idle�� �Ǹ�, ���µ� Idle�� ��
        state = State.IDLE;

    }

    //����
    protected virtual IEnumerator OnAttack(int PattonNum)
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
        if (anim.GetFloat("Patton") == 2 || anim.GetFloat("Patton") == 4 || anim.GetFloat("Patton") == 5)
        {
            anim.SetBool("IsAttack", true);
        }


        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle"));
        //yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));

        //���� �ʱ�ȭ �ǰ�
        anim.SetFloat("Patton", 0);
        anim.SetBool("IsAttack", false);

        //���� �ݶ��̴� ����
        for (int i = 0; i < boxCollider.Length; i++)
        {
            boxCollider[i].enabled = false;
        }

        //�ִϸ��̼��� Idle�� �Ǹ�, ���µ� Idle�� ��
        state = State.IDLE;
    }

    //���
    public virtual IEnumerator Die()
    {
        anim.SetTrigger("DieTrigger");
        anim.SetBool("Die", true);

        if (enemyHp.capsuleCollider != null)
        {
            enemyHp.capsuleCollider.enabled = false;
        }

        yield return new WaitUntil(() => anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Die") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);
        enemyHp.isdead_effect.SetActive(true);

        yield return new WaitForSeconds(3f);
        enemyHp.isdead = false;
        enemyHp.isdead_effect.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Main_Player.Instance.OnDamage(damage);
        }
    }
}




