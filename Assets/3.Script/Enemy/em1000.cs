using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em1000 : MonoBehaviour
{

    [Header ("�� ����")]
    [SerializeField] GameObject saw;
    [Range(1f,500f)]
    [SerializeField] float maxsawrotateSpeed = 500f;
    [Range(1f,500f)]
    [SerializeField] float minsawrotateSpeed = 100f;
    [SerializeField] float speed_down_time = 3f;

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


    //����
    void SawAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentSawRotateSpeed == maxsawrotateSpeed)
            {
                StartCoroutine(DecreaseRotationSpeed());
            }
            else
            {
                currentSawRotateSpeed = maxsawrotateSpeed;
            }
        }
    }

    //ȸ�� ������
    IEnumerator DecreaseRotationSpeed()
    {
        float Timer = 0f;
        float startSpeed = currentSawRotateSpeed;

        //2�� ���� �ӵ� �پ��
        while (Timer < speed_down_time)
        {
            Timer += Time.deltaTime;

            currentSawRotateSpeed = Mathf.Lerp(startSpeed, minsawrotateSpeed, Timer / speed_down_time);
            yield return null;
        }

        currentSawRotateSpeed = minsawrotateSpeed;
    }

}
