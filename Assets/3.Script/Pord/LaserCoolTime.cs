using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserCoolTime : MonoBehaviour
{
    [SerializeField] GameObject Pord;
    [SerializeField] Text CoolTime_text;
    [SerializeField] Slider CoolTime_Slider;
    private Vector3 CoolPos = new Vector3(0, -0.9f, 0);
    private float LaserCool = 7f;
    private float CurrentCool = 0f;
    public bool CanLaser = true;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        CurrentCool = LaserCool;
    }
    private void Update()
    {

        if (CurrentCool > 0f)
        {
            CurrentCool -= Time.deltaTime;
            CanLaser = false;
        }
        else
        {
            CanLaser = true;
            gameObject.SetActive(false);
        }
        transform.position = Pord.transform.position + CoolPos;
        CoolTime_text.text = string.Format("{0:##0}", CurrentCool * 100f);
        CoolTime_Slider.value = CurrentCool / LaserCool;

    }

}
