using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackViewFlagMove : IFlagMoveStrategy
{
    public Vector3 Move(FlagControl player, out Vector3 move)
    {
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        player.anim.SetFloat(player.hashHSpeed, move.x);

        return move;
    }
}
