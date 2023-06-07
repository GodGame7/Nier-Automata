using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    [Header("Ä®µé")]
    SwordPos handSword;
    BigSwordPos handBSword;
    Sword throwingSword;
    BigSword throwingBSword;
    GameObject idleSword;
    GameObject idleBSword;


    private void Awake()
    {
        handSword = FindObjectOfType<SwordPos>();
        handBSword = FindObjectOfType<BigSwordPos>();
        throwingSword = FindObjectOfType<Sword>();
        throwingBSword = FindObjectOfType<BigSword>();
    }
    private void Start()
    {
        idleSword = Main_Player.Instance.idleSword;
        idleBSword = Main_Player.Instance.idleBigSword;
        NoSword();
    }





    Vector3 trashPos = new Vector3(0, 100, 0);
    public void NoSword()
    {
        handSword.Reset();
        handBSword.Reset();
        ThrowingSwordOff();
        if (!idleSword.activeSelf)
        {
            idleSword.SetActive(true);
        }
        if (!idleBSword.activeSelf)
        {
            idleBSword.SetActive(true);
        }
    }
    public void HandSword()
    {
        handSword.Load();
        idleSword.SetActive(false);
    }
    public void HandBSword()
    {
        handBSword.Load();
        idleBSword.SetActive(false);
    }
    public void ThrowingSword()
    {
        handSword.Reset();
        Main_Player.Instance.throwingsword.transform.position = Main_Player.Instance.transform.position;
        Main_Player.Instance.throwingsword.transform.rotation = Main_Player.Instance.transform.rotation;
    }
    public void ThrowingSwordOff()
    {
        Main_Player.Instance.throwingsword.transform.position = trashPos;
    }
}
