using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideViewFlagMove : IFlagMoveStrategy
{
    public Vector3 Move(FlagControl player, out Vector3 move)
    {
        move = new Vector3(0, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        
        return move;
    }
}
