using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GundamTopViewMove : MonoBehaviour, IFlagViewStrategy
{
    public Vector3 Move(FlagControl player, out Vector3 move)
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Rotate(player, move);
        return move;
    }

    float _rotationVelocity;
    private void Rotate(FlagControl player, Vector3 move)
    {
        Vector3 inputDirection = new Vector3(move.x, 0.0f, move.z).normalized;
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            float targetRotation = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            if(targetRotation == 0 && move.z < 0)
            {
                targetRotation = -180f;
            }
            //float rotation = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, 0.12f);
            player.transform.rotation = Quaternion.Euler(player.transform.eulerAngles.x, 0.0f, targetRotation);
        }
    }
}
