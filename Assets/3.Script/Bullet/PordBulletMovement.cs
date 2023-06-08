using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PordBulletMovement : MonoBehaviour
{
    [SerializeField] private Vector3 Direction = Vector3.zero;
    [SerializeField] private float speed = 2f;

    private void FixedUpdate()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }

    public void Move(Vector3 direction)
    {
        Vector3 targetpos = direction;
        if (targetpos.y < 0)
        {
            targetpos.y = 0;
        }
        Direction = targetpos;

    }
    public void MoveRockOn(Vector3 direction)
    {

        Direction = direction;

    }
}
