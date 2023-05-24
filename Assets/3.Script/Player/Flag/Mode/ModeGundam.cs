using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    private Vector2 mouseMovement = Vector2.zero;
    private float angle;
    float waitTime = 2.3f;
    float startTime;

    #region �̵�
    public void Dash(FlagControl player)
    {
        player.StopCoroutine(nameof(player.ReturnToNomalState_co));
        startTime = Time.time;
        player.StartCoroutine(player.ReturnToNomalState_co(new WaitUntil(()=> (Time.time >= startTime + waitTime) && !(player.currentState is FlagAttack))));
        player.StartCoroutine(player.ReturnToNomalState_co(new WaitUntil(() => player.h == 0 && player.v == 0 && !(player.currentState is FlagAttack))));
    }

    public void Rotate(FlagControl player)
    {
        mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (player.h == 0 && player.v == 0)
        {
            if (mouseMovement.sqrMagnitude > player.threshold)
            {
                // ���콺 �̵����� �̿��Ͽ� �ﰢ�Լ� ���� rotate ���ϱ�
                angle = (Mathf.Atan2(mouseMovement.y, mouseMovement.x)) * Mathf.Rad2Deg;
                angle *= -1;
                angle += 90;
            }
        }
        else
        {
            // Ű���� �Է°��� �̿��Ͽ� �ﰢ�Լ� ���� rotate ���ϱ�
            angle = Mathf.Atan2(player.v, player.h) * Mathf.Rad2Deg;
            angle *= -1;
            angle += 90;
        }
        player.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
    }
    #endregion �̵�

    #region ����
    public void StrongAttack(FlagControl player)
    {
        player.SetAnimaTrigger(player.hashGundamStrongAttack);
    }
    public void WeakAttack(FlagControl player, bool isHorizontal)
    {
        if (!player.isCombo)
        {
            player.SetAnimaTrigger(player.hashGundamWeakAttack1);
        }
        else
        {
            player.SetAnimaTrigger(player.hashGundamWeakAttack2);
        }
    }
    #endregion ����
}
