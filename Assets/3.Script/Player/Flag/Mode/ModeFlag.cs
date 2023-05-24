using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeFlag : IFlagModeStrategy
{
    public void Dash(FlagControl player)
    {
        Vector3 playerScale = player.transform.localScale;
        // 왼쪽 대쉬시 스케일을 통해 애니메이션 반전
        if (player.lastKeyPressed == KeyCode.A)
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
        player.StartCoroutine(nameof(player.ResetAnimaTrigger_co), player.hashDash);
        player.StopCoroutine(nameof(player.ReturnToNomalState_co));
        player.StartCoroutine(player.ReturnToNomalState_co(player.EnterDashAni_wait, player.ExitDashAni_wait));
    }

    #region 공격
    public void StrongAttack(FlagControl player)
    {
        player.anim.SetTrigger(player.hashFlagStrongAttack);
    }
    public void WeakAttack(FlagControl player, bool isHorizontal)
    {
        if (isHorizontal)
        {
            HorizontalWeakAttack(player);
        }
        else
        {
            VerticalWeakAttack(player);
        }
    }
    private void VerticalWeakAttack(FlagControl player)
    {
        player.anim.SetTrigger(player.hashVerticalWeakAttack);
    }
    private void HorizontalWeakAttack(FlagControl player)
    {
        player.anim.SetTrigger(player.hashHorizontalWeakAttack);
    }
    #endregion 공격
}
