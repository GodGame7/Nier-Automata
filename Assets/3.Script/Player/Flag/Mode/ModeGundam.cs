using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    private Vector3 mousePos;
    private Vector3 playerPos;
    public float angle;

    public void Dash(FlagControl player)
    {
        throw new System.NotImplementedException();
    }

    public void Rotate(FlagControl player)
    {
        mousePos = Input.mousePosition;
        playerPos = Camera.main.WorldToScreenPoint(player.transform.position + 1f * Vector3.up);

        Vector3 direction = mousePos - playerPos;
        // 플레이어 위치와 마우스 위치를 이용하여 삼각함수 통해 rotate 구하기
        angle = (Mathf.Atan2(direction.y, direction.x)) * Mathf.Rad2Deg;
        angle += 90;

        // 왼손 좌표계 보정
        angle *= -1;
        angle -= 180;

        player.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
    }

    #region 공격
    public void StrongAttack(FlagControl player)
    {
        player.anim.SetTrigger(player.hashGundamStrongAttack);
    }
    public void WeakAttack(FlagControl player, bool isHorizontal)
    {
        if (!player.isCombo)
        {
            player.anim.SetTrigger(player.hashGundamWeakAttack1);
            player.isCombo = true;
        }
        else
        {
            player.anim.SetTrigger(player.hashGundamWeakAttack2);
            player.isCombo = false;
        }
    }
    #endregion 공격
}
