using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle2 : State
{
    private Camera mainCamera;
    float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    private void Start()
    {
        mainCamera = Camera.main;
    }
    public override void Enter(State before)
    {
        Main_Player.Instance.anim_player.SetBool("Idle", true);
        Main_Player.Instance.anim_player.SetTrigger("Idle 0");
        Main_Player.Instance.anim_sword.SetTrigger("Idle");
        Main_Player.Instance.anim_bigsword.SetTrigger("Idle");
    }

    public override void Exit(State next)
    {
        Main_Player.Instance.anim_player.SetBool("Idle", false);
        Main_Player.Instance.anim_player.SetFloat("Speed", 0f);
    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateUpdate()
    { 
        // �÷��̾� �̵�
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ī�޶��� ���� ���͸� ȹ��
        Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �Է°��� ī�޶��� ���� ���͸� �����Ͽ� �̵� ���� ���
        Vector3 movement = (moveHorizontal * mainCamera.transform.right + moveVertical * cameraForward).normalized;

        // �÷��̾� �̵�
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 8f;
                Main_Player.Instance.anim_player.SetFloat("Speed", 1.5f);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                moveSpeed = 2f;
                Main_Player.Instance.anim_player.SetFloat("Speed", 0.5f);
            }
            else
            {
                moveSpeed = 5f;
                Main_Player.Instance.anim_player.SetFloat("Speed", 1f);
            }
        }
        else Main_Player.Instance.anim_player.SetFloat("Speed", 0f);
    }
}
