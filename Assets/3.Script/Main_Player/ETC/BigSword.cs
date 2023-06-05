using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSword : MonoBehaviour
{
    State_BigAtk player;
    [SerializeField]
    bool isContinueAtk;
    bool charging;
    bool charged;
    float holdtime = 0f;
    IEnumerator cor;
    IEnumerator cor_charge;
    private void Start()
    {
        player = FindObjectOfType<State_BigAtk>();
    }
    void HitboxOn()
    {
        Main_Player.Instance.collider_bigsword.enabled = true;
    }
    void HitboxOff()
    {
        Main_Player.Instance.collider_bigsword.enabled = false;
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
            yield return new WaitUntil(() => Input.GetMouseButtonDown(1));
            isContinueAtk = true;
        }
    }
    void StartComboAtk()
    {
        if (player.isCanStr)
        {
            player.AtkStrong();
            Debug.Log("°­°ø°Ý");
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
    void ChargeStart()
    {
        holdtime = 0f;
        charging = true;
        charged = false;
        cor_charge = CheckCharge_co();
        StartCoroutine(cor_charge);
        IEnumerator CheckCharge_co()
        {
            while (charging)
            {
                if (Input.GetMouseButton(1))
                {
                    holdtime += Time.deltaTime;
                    if (holdtime > 1f)
                    {
                        charged = true;
                    }
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    charging = false;
                }
                yield return null;
            }
            if (charged)
            {
                player.ChargeAtk();
            }
            else if (!charged)
            {
                player.ChargeCancle();
            }
        }
    }

    void LoadBig()
    {
        player.LoadBig();
    }
    void EndAtk()
    {
        player.EndAtk();
    }
}
