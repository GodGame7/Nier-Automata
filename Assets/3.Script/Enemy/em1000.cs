using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em1000 : MonoBehaviour
{

    [Header("톱 관련")]
    [SerializeField] GameObject saw;
    [Range(1f, 500f)]
    [SerializeField] float maxsawrotateSpeed = 500f;
    [Range(1f, 500f)]
    [SerializeField] float minsawrotateSpeed = 150f;
    [SerializeField] float speed_down_time = 5f;

    [SerializeField] bool isattack = false;

    private float currentSawRotateSpeed;

    void Start()
    {
        currentSawRotateSpeed = maxsawrotateSpeed;
    }

    void Update()
    {
        saw.transform.Rotate(-currentSawRotateSpeed * Time.deltaTime, 0, 0);

        SawAttack();
    }


    //공격
    void SawAttack()
    {
        if (currentSawRotateSpeed == maxsawrotateSpeed && isattack)

        {
            StartCoroutine(DecreaseRotationSpeed());
        }
        else if (!isattack)
        {
            currentSawRotateSpeed = maxsawrotateSpeed;
        }
    }

    //회전 떨구기
    IEnumerator DecreaseRotationSpeed()
    {
        float Timer = 0f;
        float startSpeed = currentSawRotateSpeed;

        //2초 정도 속도 줄어듬
        while (Timer < speed_down_time)
        {
            Timer += Time.deltaTime;

            currentSawRotateSpeed = Mathf.Lerp(startSpeed, minsawrotateSpeed, Timer / speed_down_time);
            yield return null;
        }

        currentSawRotateSpeed = minsawrotateSpeed;
    }

}
