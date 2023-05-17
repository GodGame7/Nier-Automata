using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewFlagMove : IFlagMoveStrategy
{
    public void Move(FlagControl player)
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float targetSpeed = player.moveSpeed;

        if (move.Equals(Vector2.zero))
        {
            targetSpeed = 0f;
        }
        float _speed = targetSpeed;

        player.controller.Move(_speed * Time.deltaTime * move);

        if (move.x * player.scale.x < 0)
        {
            player.scale.x *= -1;
            player.transform.localScale = player.scale;
        }
        player.anim.SetFloat(player.hashHSpeed, move.x);
    }
}
