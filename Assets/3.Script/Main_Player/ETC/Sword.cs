using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    State_Atk2 player;
    Swords SM;
    [SerializeField]
    bool isContinueAtk;
    IEnumerator cor;
    private void Start()
    {
        player = FindObjectOfType<State_Atk2>();
        SM = FindObjectOfType<Swords>();
    }
    void SwordHitboxOn()
    {
        Main_Player.Instance.collider_sword.enabled = true;
    }
    void SwordHitboxOff()
    {
        Main_Player.Instance.collider_sword.enabled = false;
    }
    void ThrowingOn()
    {
        SM.ThrowingSword();
        SwordHitboxOff();
        Main_Player.Instance.collider_throwingsword.enabled = true;
    }
    void ThrowingOff()
    {
        SM.ThrowingSwordOff();
        SwordHitboxOff();
        Main_Player.Instance.collider_throwingsword.enabled = false;
    }
    void CheckComboAtk()
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
    void StartComboAtk()
    {
        if (player.isCanStr)
        {
            player.AtkStrong();
            Debug.Log("������");
        }
        else if (isContinueAtk)
        {
            player.Atk();
        }
        else
        {
            StopCoroutine(cor);
        }       
    }
    void EndAtk()
    {
        player.EndAtk();
    }
}
