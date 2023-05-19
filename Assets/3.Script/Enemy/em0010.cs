using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em0010 : MonoBehaviour
{
    enum State
    {
        IDLE,
        WALK,
        JUMP,
        DASH,
        ATTACK,
        DIE
    }

    //Ÿ�ٰ��� �Ÿ�
    float distance;
    bool isdead = false;

    //BoxCollider[] boxCollider;

    //Ÿ��
    [SerializeField] Transform target;

    Animator anim;
    AnimatorClipInfo[] animatorinfo;

    //����
    [SerializeField] State state;

    private void Awake()
    {
        TryGetComponent(out anim);
        //boxCollider = GetComponentsInChildren<BoxCollider>();
    }


    private void Start()
    {

        //Ÿ�� ��ġ ã��
        target = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.IDLE;

        //�ݶ��̴� ���� => �ٵ� �ȉ�
        //for (int i = 0; i < boxCollider.Length; i++)
        //{
        //    boxCollider[i].enabled = false;
        //}

        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {
        while (!isdead)
        {

            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
            string current_animation = animatorinfo[0].clip.name;
            Debug.Log(current_animation);

            if (current_animation == "Attack_Double" ||
                current_animation == "Attack_Foot" ||
                current_animation == "Attack_Left" ||
                current_animation == "Attack_Right")
            {
                yield return null;
               continue;
            }



            // Ÿ�� ��ġ�� ���´�.
            Vector3 targetPosition = target.position;
            targetPosition.y = 0f;

            // Ÿ�� ������ �ٶ󺸵��� ȸ��

            transform.LookAt(targetPosition);


            //Ÿ�ٰ� �Ÿ��� ����
            distance = Vector3.Distance(transform.position, target.transform.position);

            switch (state)
            {
                case State.IDLE:
                    //1�� �ڿ� �ȱ� ����
                    yield return new WaitForSeconds(1f);
                    state = State.WALK;
                    break;

                case State.WALK:
                    UpdateWalk();
                    break;

                case State.JUMP:
                    break;

                case State.DASH:
                    //UpdateDash();
                    break;

                case State.ATTACK:
                    Attack();
                    break;

            }
            yield return null;
        }

    }

    void UpdateWalk()
    {
        //�ȱ�
        anim.SetBool("Run", true);

        //���� �Ÿ��� 3f ���ϸ� �����ϰ�, 6f �̻��̸� �뽬��
        if (distance < 2.8f)
        {
            anim.SetFloat("Patton", 0);
            anim.SetBool("Run", false);
            state = State.ATTACK;
        }
        //else if (distance >= 6f)
        //{
        //    state = State.DASH;
        //}
    }

    void Attack()
    {
        float value = Random.Range(1, 5);
        Debug.Log(value);

        if (anim.GetFloat("Patton") <= 0f)
        {
            anim.SetFloat("Patton", (float)value);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
            {
                state = State.IDLE;
            }
        }
    }

    //void UpdateDash()
    //{
    //    anim.SetTrigger("Dash");
    //    state = State.WALK;
    //}


    public void TakeDamage(float damage)
    {

    }
}
