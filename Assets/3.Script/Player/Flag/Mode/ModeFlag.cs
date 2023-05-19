using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeFlag : IFlagModeStrategy
{
    public void Dash(FlagControl player)
    {
        Vector3 playerScale = player.transform.localScale;

        // ���� �뽬�� �������� ���� �ִϸ��̼� ����
        if (player.currentDirectX < 0)
        {
            playerScale.x = -1;
            if (!player.transform.localScale.x.Equals(playerScale.x))
            {
                player.transform.localScale = playerScale;
                player.StartCoroutine(nameof(player.ResetScaleX_co));
            }
        }
        else
        {
            playerScale.x = 1;
            if (!player.transform.localScale.x.Equals(playerScale.x))
            {
                player.transform.localScale = playerScale;
            }
        }
        player.anim.SetTrigger(player.hashDash);
        player.StopCoroutine(nameof(player.ResetAnimaTrigger_co));
        player.StartCoroutine(nameof(player.ResetAnimaTrigger_co), player.hashDash);
        player.StartCoroutine(nameof(player.ReturnToNomalState_co), player.EndDash_wait);
    }

    public void StrongAttack(FlagControl player)
    {
        Debug.Log("����� ������");
    }

    public void WeakAttack(FlagControl player)
    {
        Debug.Log("����� �����");
    }
}
