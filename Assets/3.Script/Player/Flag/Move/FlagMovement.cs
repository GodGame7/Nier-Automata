using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSideViewMove : IFlagViewStrategy
{
    public Vector3 Move(FlagControl player, out Vector3 move)
    {
        move = new Vector3(0, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        return move;
    }
}
public class FlagBackViewMove : IFlagViewStrategy
{
    public Vector3 Move(FlagControl player, out Vector3 move)
    {
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        return move;
    }
}
public class FlagTopViewMove : IFlagViewStrategy
{
    public Vector3 Move(FlagControl player, out Vector3 move)
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        return move;
    }
}
