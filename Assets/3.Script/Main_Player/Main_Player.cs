using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{
    private static Main_Player instance;
    public static Main_Player Instance { get { return instance; } }

    [Header("컴포넌트")]
    StateManager sm;
    public Rigidbody rb;
    public Animator anim_player;
    public Animator anim_sword;
    public Animator anim_bigsword; 
    public CapsuleCollider collider_body;
    public BoxCollider collider_sword;
    public BoxCollider collider_throwingsword;
    public BoxCollider collider_bigsword;
    [Header("검 거치대")]
    public GameObject idleSword;
    public GameObject idleBigSword;
    public GameObject bigSword;
    public GameObject sword;
    public GameObject throwingsword;
    [Header("손 위치")]
    public Transform leftHand;
    public Transform rightHand;
    [Header("손에 맞출 포지션")]
    public Transform swordBone;
    public Transform bigSwordBone;


    [Header("컨트롤용 불변수")]
    public bool isGrounded;
    public bool isDash;
    public bool isAtk;
    public bool isDodge;
    public bool isHitted;
    public bool isInvincible;
    float invinsibletimer = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        sm = GetComponent<StateManager>();
        isGrounded = true;
        isDash = false;
        isAtk = false;
        isDodge = false;
        isHitted = false;
        collider_sword.enabled = false;
        collider_bigsword.enabled = false;
    }

    public void ResetBool()
    {
        isDash = false;
        isAtk = false;
        isDodge = false;
        isHitted = false;
    }
    public void Rotate()
    {
        Vector3 rotation = new Vector3(0f, Input.GetAxis("Horizontal"), 0f) * 1f;
        transform.Rotate(rotation);
    }
    public bool isCanHit()
    {
        if (isDodge || isHitted || isDash || isInvincible) return false;
        else return true;
    }
    public bool isCanAttack()
    {
        if (isDodge || isHitted || isAtk) return false;
        else return true;
    }

    IEnumerator invin;
    void BeInvincible(float time)
    {
        invin = Invincible_co(time);
        if (invin != null) { 
        StopCoroutine(invin);
        }
        StartCoroutine(invin);
        IEnumerator Invincible_co(float time)
        {
            isInvincible = true;
            invinsibletimer = time;
            while (isInvincible && invinsibletimer > 0f)
            {
                invinsibletimer -= Time.deltaTime;
                yield return null;
            }
            isInvincible = false;
        }
    }
    public void OnDamage(float damage)
    {
        if (isCanHit())
        {
            sm.ChangeState(sm.hitted);
            BeInvincible(1f);
            PlayerData.Instance.OnDamage(damage);
        }
        else if(isInvincible)
        {
            Debug.Log("무적 상태입니다");
        }
        else if (isDodge || isDash) { BeInvincible(1f); HittedWhileDodge(); }
    }
    public void SwordToHand()
    {
        swordBone.position = leftHand.position;
    }
    public void BigSwordToHand()
    {
        bigSwordBone.position = rightHand.position;
    }

    [Header("메쉬베이크용")]
    [SerializeField] public MeshBake meshBake;
    public void HittedWhileDodge()
    {
        //todo 포스트프로세싱, 타임스케일 등 연출요소
        AudioManager.Instance.PlaySfx(Define.SFX.Dodge);
        meshBake.DodgeEffect();
    }
}
