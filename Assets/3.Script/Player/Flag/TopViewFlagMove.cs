using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewFlagMove : IFlagMoveStrategy
{
    public Vector3 Move(FlagControl player, out Vector3 move)
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        player.anim.SetFloat(player.hashHSpeed, move.x);

        return move;
    }
}
