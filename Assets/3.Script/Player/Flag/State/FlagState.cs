using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAttack : FlagBaseState
{
    public override void Action(FlagControl player)
    {
        if (!player.gameObject.layer.Equals(player.defaultLayer))
        {
            player.gameObject.layer = player.defaultLayer;
        }
    }
    public override void Attack(FlagControl player)
    {
        if (player.currentModeStrategy is ModeGundam)
        {
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0))
            {
                player.SetState(player.attackState);
                player.currentModeStrategy.WeakAttack(player);
                player.StopCoroutine(nameof(player.ReturnToNomalState_co));
                player.StartCoroutine(player.ReturnToNomalState_co(player.EnterAttackAni_wait, player.ExitAttackAni_wait));
            }
        }
    }
}
public class FlagTransformation : FlagBaseState
{
    public override void Action(FlagControl player)
    {
        if (!player.gameObject.layer.Equals(player.invincibleLayer))
        {
            player.gameObject.layer = player.invincibleLayer;
        }
    }

    public override void Attack(FlagControl player)
    {
    }
}
public class FlagNomal : FlagBaseState
{
    private readonly KeyCode[] keysToCheck = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W };    // �뽬�� ���� ���� �̵�Ű ����
    private float lastKeyPressTime = 0f;
    private readonly float timeAllowedBetweenKeyPresses = 0.5f; // ���� �Է����� �Ǵ��ϱ� ���� �ִ� �ð���

    public override void Action(FlagControl player)
    {
        if (!player.gameObject.layer.Equals(player.defaultLayer))
        {
            player.gameObject.layer = player.defaultLayer;
        }
    }

    public override void Dash(FlagControl player)
    {
        foreach (KeyCode key in keysToCheck)
        {
            if (Input.GetKeyDown(key))
            {
                // ���� �ð� �ȿ� ���� ����Ű�� �ι� �������� �뽬
                if (player.lastKeyPressed == key && Time.time - lastKeyPressTime <= timeAllowedBetweenKeyPresses)
                {
                    player.SetState(player.dashState);
                    player.currentModeStrategy.Dash(player);
                    player.lastKeyPressed = KeyCode.None;
                }
                else
                {
                    player.lastKeyPressed = key;
                }
                lastKeyPressTime = Time.time;
            }
        }
    }
}
public class FlagDash : FlagBaseState
{
    public override void Action(FlagControl player)
    {
        if (!player.gameObject.layer.Equals(player.invincibleLayer))
        {
            player.gameObject.layer = player.invincibleLayer;
        }
    }
}
