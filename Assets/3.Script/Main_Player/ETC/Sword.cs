using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    State_Atk2 player;
    [SerializeField]
    bool isContinueAtk;
    IEnumerator cor;
    private void Start()
    {
        player = FindObjectOfType<State_Atk2>();
    }
    void SwordHitboxOn()
    {
        Main_Player.Instance.collider_sword.enabled = true;
    }
    void SwordHitboxOff()
    {
        Main_Player.Instance.collider_sword.enabled = false;
    }

    void CheckComboAtk()
    {
        isContinueAtk = false;
        cor = CheckComboAtk_co();
        StartCoroutine(cor);

        //Local Function
        IEnumerator CheckComboAtk_co()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            isContinueAtk = true;
        }
    }
    void StartComboAtk()
    {
        Debug.Log(isContinueAtk);
        if (isContinueAtk)
        {
            player.Atk();
        }
        else
        {
            Debug.Log(1);
            StopCoroutine(cor); 
            player.EndAtk();
        }
    }
}
