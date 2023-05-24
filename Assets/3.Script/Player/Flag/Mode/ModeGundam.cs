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
        // �÷��̾� ��ġ�� ���콺 ��ġ�� �̿��Ͽ� �ﰢ�Լ� ���� rotate ���ϱ�
        angle = (Mathf.Atan2(direction.y, direction.x)) * Mathf.Rad2Deg;
        angle += 90;

        // �޼� ��ǥ�� ����
        angle *= -1;
        angle -= 180;

        player.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
    }

    #region ����
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
    #endregion ����
}
