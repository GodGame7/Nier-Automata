using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [Header("Enemy ü�� ����")]
    [SerializeField] private float MaxHP;
    [SerializeField] private float CurrentHP;
    [Tooltip("������ �̻��� �������� ������")]
    [SerializeField] private float MinDamage;
    [SerializeField] private Slider enemyHPBar;
    [SerializeField] private Canvas enemyUI;


    [Header("Enemy ��� ����")]
    [Space(10f)]
    public bool isdead = false;
    public GameObject isdead_effect;

    //�ǰݿ� �ݶ��̴�
    public CapsuleCollider capsuleCollider;

    //��ũ��Ʈ
    Enemy enemy;

    Camera camera;

    //ü�� ������Ƽ
    public float maxHp => MaxHP;
    public float currentHp
    {
        get { return CurrentHP; }
        set
        {
            CurrentHP = value;

            if (CurrentHP <= 0f && !isdead)
            {
                isdead = true;
                StartCoroutine(enemy.Die());
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


    void Awake()
    {
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();

        TryGetComponent(out enemy);

        camera = Camera.main;
    }
    void OnEnable()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        if (enemyHPBar != null)
        {
            //Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);
            //enemyHPBar.transform.position = screenPosition + new Vector3(0, 150f, 0);
            enemyUI.transform.LookAt(camera.transform);
            enemyHPBar.value = currentHp / maxHp;
        }


    }

    //�������� ���� ��
    public void TakeDamage(int Damage)
    {

        //�� �ڿ� �ִ��� Ȯ��
        Vector3 targetDirection = enemy.target.transform.position - transform.position;
        Vector3 forwardDirection = transform.forward;
        bool isTargetInFront = Vector3.Dot(targetDirection, forwardDirection) > 0;
        enemy.anim.SetBool("FrontPlayer", isTargetInFront);

        currentHp -= Damage;

        //�� ������ �̻��̸� ��ݹ޾ƿ�~
        if (Damage >= MinDamage )
        {
            hitNum++;
            enemy.anim.SetFloat("HitNum", hitNum);
            enemy.anim.SetTrigger("Hit");

            if (enemy.anim.GetFloat("Patton") == 2 || enemy.anim.GetFloat("Patton") == 4 || enemy.anim.GetFloat("Patton") == 5 || enemy.anim.GetBool("Attack2"))
            {
                enemy.anim.ResetTrigger("Hit");
            }
        }

    }

    ////���
    //IEnumerator Die()
    //{
    //    enemy.anim.SetTrigger("DieTrigger");
    //    enemy.anim.SetBool("Die", true);

    //    if (capsuleCollider != null)
    //    {
    //        capsuleCollider.enabled = false;
    //    }

    //    yield return new WaitUntil(() => enemy.anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Die") && enemy.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);
    //    isdead_effect.SetActive(true);

    //    yield return new WaitForSeconds(3f);
    //    isdead = false;
    //    isdead_effect.SetActive(false);
    //    gameObject.SetActive(false);
    //}
}
