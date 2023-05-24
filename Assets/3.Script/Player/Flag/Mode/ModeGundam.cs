using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    private Vector2 mouseMovement = Vector2.zero;
    public float angle;

    public void Dash(FlagControl player)
    {
        throw new System.NotImplementedException();
    }

    public void Rotate(FlagControl player)
    {
        mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (mouseMovement.sqrMagnitude > player.threshold)
        {
            // 마우스 이동값을 이용하여 삼각함수 통해 rotate 구하기
            angle = (Mathf.Atan2(mouseMovement.y, mouseMovement.x)) * Mathf.Rad2Deg;
            angle += 90;

            // 왼손 좌표계 보정
            angle *= -1;
            angle -= 180;

            player.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
        }
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
