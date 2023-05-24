using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    private Vector2 mouseMovement = Vector2.zero;
    private float angle;
    private WaitForSeconds dash_wait = new WaitForSeconds(2.3f);

    public void Dash(FlagControl player)
    {
        player.StopCoroutine(nameof(player.ReturnToNomalState_co));
        player.StartCoroutine(player.ReturnToNomalState_co(dash_wait));
    }

    public void Rotate(FlagControl player)
    {
        mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (player.h == 0 && player.v == 0)
        {
            if (mouseMovement.sqrMagnitude > player.threshold)
            {
                // 마우스 이동값을 이용하여 삼각함수 통해 rotate 구하기
                angle = (Mathf.Atan2(mouseMovement.y, mouseMovement.x)) * Mathf.Rad2Deg;
                angle *= -1;
                angle += 90;
            }
        }
        else
        {
            // 키보드 입력값을 이용하여 삼각함수 통해 rotate 구하기
            angle = Mathf.Atan2(player.v, player.h) * Mathf.Rad2Deg;
            angle *= -1;
            angle += 90;
        }
        player.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
    }

    #region 공격
    public void StrongAttack(FlagControl player)
    {
        player.SetAnimaTrigger(player.hashGundamStrongAttack);
    }
    public void WeakAttack(FlagControl player, bool isHorizontal)
    {
        if (!player.isCombo)
        {
            player.SetAnimaTrigger(player.hashGundamWeakAttack1);
            player.isCombo = true;
        }
        else
        {
            player.SetAnimaTrigger(player.hashGundamWeakAttack2);
            player.isCombo = false;
        }
    }
    #endregion 공격
}
