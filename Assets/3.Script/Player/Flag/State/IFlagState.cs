using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlagBaseState
{
    public virtual void Attack(FlagControl player)
    {
        if (!player.canFire)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0))
        {
            player.SetState(player.attackState);
            // ����� ����� ������� ������ ���� �������� ���� �������� ���� �����ϱ� ���� ����
            bool isHorizontal;
            if (player.currentViewStrategy is FlagSideViewMove)
            {
                isHorizontal = false;
            }
            else
            {
                isHorizontal = true;
            }

            player.currentModeStrategy.WeakAttack(player, isHorizontal);
            player.StopCoroutine(nameof(player.ReturnToNomalState_co));
            player.StartCoroutine(player.ReturnToNomalState_co(player.EnterAttackAni_wait, player.ExitAttackAni_wait));
        }
        else if (Input.GetKeyDown(KeyCode.Slash) || Input.GetMouseButtonDown(1))
        {
            player.SetState(player.attackState);
            player.currentModeStrategy.StrongAttack(player);
            player.StopCoroutine(nameof(player.ReturnToNomalState_co));
            player.StartCoroutine(player.ReturnToNomalState_co(player.EnterAttackAni_wait, player.ExitAttackAni_wait));
        }
    }
    public virtual void Dash(FlagControl player)
    {

    }
    public abstract void Action(FlagControl player);
}
