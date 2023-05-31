using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player : MonoBehaviour
{
    private static Main_Player instance;
    public static Main_Player Instance { get { return instance; } }

    [Header("컴포넌트")]
    //public Rigidbody rb;
    public Transform transForm;
    public Rigidbody rb;
    public Animator anim_player;
    public Animator anim_sword;
    public Animator anim_bigsword;
    public CapsuleCollider collider_body;
    public BoxCollider collider_sword;
    public BoxCollider collider_bigsword;

    [Header("컨트롤용 불변수")]
    public bool isGrounded;
    public bool isDash;
    public bool isAtk;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        isGrounded = true;
        isDash = false;
        isAtk = false;
        collider_sword.enabled = false;
        collider_bigsword.enabled = false;
    }

    public void Rotation2(Vector3 inputVec)
    {
        StartCoroutine(Rotatewhile(inputVec));
    }
    public IEnumerator Rotatewhile(Vector3 inputVec)
    {
        Quaternion targetRotation = Quaternion.LookRotation(inputVec, Vector3.up);
        while (transform.rotation != targetRotation)
        {
            transform.rotation = 
                Quaternion.RotateTowards(transform.rotation, targetRotation, 1f * Time.deltaTime);
            yield return null;
        }
    }
    public void Rotate()
    {
        Vector3 rotation = new Vector3(0f, Input.GetAxis("Horizontal"), 0f) * 1f;
        transform.Rotate(rotation);
    }

    public void OnDamage(float damage)
    {
        Main_Player.Instance.anim_player.SetTrigger("Hitted");
        PlayerData.instance.OnDamage(damage);
    }
}
