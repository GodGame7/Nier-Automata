using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
{
    public override void Enter(State before)
    {
        Main_Player.Instance.anim_player.SetTrigger("Idle");
        Main_Player.Instance.anim_sword.SetTrigger("Idle");
        Main_Player.Instance.anim_bigsword.SetTrigger("Idle");
    }

    public override void Exit(State next)
    {
        
    }

    public override void StateFixedUpdate()
    {
        
    }

    public override void StateUpdate()
    {
        Main_Player.Instance.Rotate();
    }
}
