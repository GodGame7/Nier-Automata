using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Atk2 : State
{
    private Camera mainCamera;
    private GameObject sword;
    private GameObject bigSword;
    [SerializeField] private Transform body;
    private GameObject idleSword;
    private GameObject idleBigSword;
    public bool isCanStr;
    [SerializeField]bool stronged;
    int index = 0;
    float holdTime = 0;
    Swords SM;
    private void Start()
    {
        SM = FindObjectOfType<Swords>();
        mainCamera = Camera.main;
        sword = Main_Player.Instance.sword;
        bigSword = Main_Player.Instance.bigSword;
        idleSword = Main_Player.Instance.idleSword;
        idleBigSword = Main_Player.Instance.idleBigSword;
    }
    public override void Enter(State before)
    {
        ResetBool();
        Main_Player.Instance.isAtk = true;
        Atk();
        Main_Player.Instance.anim_sword.SetTrigger("Atk");
        Main_Player.Instance.anim_player.SetTrigger("Atk");
        ResetStrong();
    }

    public override void Exit(State next)
    {        
        ResetSword();
        if (listener != null) { 
        StopCoroutine(listener);
        }
        isCanStr = false;
        stronged = false;
        index = 0;
        Main_Player.Instance.rb.velocity = Vector3.zero;
        ResetBool();
    }

    public override void StateFixedUpdate()
    {
 
    }

    public override void StateUpdate()
    {
        if (!stronged) { 
            if (Input.GetMouseButtonDown(0))
            {
                holdTime = 0;
            }
            else if (Input.GetMouseButton(0))
            {
                holdTime += Time.deltaTime;
                if (holdTime > 0.45f)
                {
                    isCanStr = true;
                }
            }
        }
    }


    void ResetBool()
    {
        for (int i = 1; i <= 5; i++)
        {
            Main_Player.Instance.anim_sword.SetBool("Atk" + i, false);
            Main_Player.Instance.anim_player.SetBool("Atk" + i, false);
            if (i <= 3)
            {
                Main_Player.Instance.anim_bigsword.SetBool("Atk" + i, false);
            }
        }
    }
    public void AtkStrong()
    {
        stronged = true;
        isCanStr = false;//강공격 불변수 체크
        //강공격 애니메이션 재생
        SMoveAnim(1f, 600f);
        Main_Player.Instance.anim_sword.SetTrigger("AtkStrong");
        Main_Player.Instance.anim_player.SetTrigger("AtkStrong");
        //holdtime 초기화
        holdTime = 0f;
    }
    void ResetStrong()
    {
        Main_Player.Instance.anim_player.SetBool("CanStrong", true);
        Main_Player.Instance.anim_sword.SetBool("CanStrong", true);
        stronged = false;
        isCanStr = false;
        holdTime = 0f;
    }
    void Rotate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 movement = (moveHorizontal * mainCamera.transform.right + moveVertical * cameraForward).normalized;

        Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
        transform.rotation = toRotation;
    }
    void Rotate2()
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, mainCamera.transform.rotation.eulerAngles.y, initialRotation.eulerAngles.z);
        transform.rotation = targetRotation;
    }
    //============================ 공격 메소드 =========================
    public void Atk()
    {
        index++;
        int i = index;
        if (i <= 5)
        {
            Atk_co(i);
        }
    }
    void Atk_co(int i)
    {
        Rotate2();
        if (Input.GetKey(KeyCode.W))
        {
            switch (i)
            {
                case 1: MoveAnim(0.1f, 220f); AudioManager.Instance.PlaySfx(Define.SFX.Atk1); break;
                case 2: MoveAnim(0.15f, 130f); AudioManager.Instance.PlaySfx(Define.SFX.Atk2); break;
                case 3: MoveAnim(0.5f, 15); AudioManager.Instance.PlaySfx(Define.SFX.Atk3); break;
                case 4: MoveAnim(0f, 0f); AudioManager.Instance.PlaySfx(Define.SFX.Atk4); break;
                case 5: MoveAnim(0.4f, 15); break;
            }
        }
        Atk_anim(i);
    }

    void Atk_anim(int i)
    {
        Main_Player.Instance.anim_sword.SetBool("Atk" + i, true);
        Main_Player.Instance.anim_player.SetBool("Atk" + i, true);
    }

    IEnumerator move;
    void MoveAnim(float time, float power)
    {
        float count = 0f;
        move = Move();
        StopCoroutine(move);
        StartCoroutine(move);
        IEnumerator Move()
        {
            while (count < time)
            {
                Main_Player.Instance.rb.velocity += (transform.forward * power * Time.fixedDeltaTime);
                count += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            Main_Player.Instance.rb.velocity = Vector3.zero;
        }
    }
    IEnumerator smove;
    void SMoveAnim(float time, float power)
    {
        float count = 0f;
        smove = Move();
        StopCoroutine(smove);
        StartCoroutine(smove);
        IEnumerator Move()
        {
            while (count < time)
            {                
                Main_Player.Instance.rb.velocity = (transform.forward * power * Time.fixedDeltaTime);
                count += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            Main_Player.Instance.rb.velocity = Vector3.zero;
        }
    }

    IEnumerator listener;
    public void EndAtk()
    {
        listener = CancleListener();
        StopCoroutine(listener);
        StartCoroutine(listener);
    }

    IEnumerator CancleListener()
    {
        yield return new WaitForEndOfFrame();
        float count = 0f;
        while (count < 2f)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetMouseButton(0))
            {
                break;
            }
            count += Time.deltaTime;
            yield return null;
        }
        Main_Player.Instance.isAtk = false;
        ResetSword();
    }

    //==애니메이션이벤트용==

    [SerializeField]
    bool isContinueAtk;
    IEnumerator cor;

    public void S_HitboxOn()
    {
        Main_Player.Instance.collider_sword.enabled = true;
    }
    public void S_HitboxOff()
    {
        Main_Player.Instance.collider_sword.enabled = false;
    }

    public void B_HitboxOn()
    {
        Main_Player.Instance.collider_bigsword.enabled = true;
    }
    public void B_HitboxOff()
    {
        Main_Player.Instance.collider_bigsword.enabled = false;
    }
    public void CheckComboAtk()
    {
        isContinueAtk = false;
        cor = CheckComboAtk_co();
        StartCoroutine(cor);

        //Local Function
        IEnumerator CheckComboAtk_co()
        {
            yield return new WaitForSeconds(0.08f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            isContinueAtk = true;
        }
    }
    public void StartComboAtk()
    {
        if (isCanStr)
        {
            AtkStrong();
            Debug.Log("강공격");
        }
        else if (isContinueAtk)
        {
            Atk();
        }
        else
        {
            StopCoroutine(cor);
        }
    }





    #region 메소드 Load,Reset + Sword, Big
    public void LoadSword()
    {
        SM.HandSword();
    }
    public void ResetSword()
    {
        //sword.SetActive(false);
        SM.NoSword();
    }
    public void LoadBig()
    {
        //bigSword.SetActive(true);
        SM.HandBSword();
    }
    public void ResetBig()
    {
        SM.NoSword();
    }
    #endregion
}
