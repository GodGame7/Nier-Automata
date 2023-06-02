using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] em0000;
    [SerializeField] GameObject[] em0000_2;
    [SerializeField] GameObject em0010;
    [SerializeField] GameObject em1000;

    public UnityEvent enemyspawner;

    bool first = true;
    bool second = false;
    bool third = false;
    bool four = false;

    bool allInactive = true;

    private void Update()
    {
        if (first)
        {
            firstEnemy();
        }
        else if (second)
        {
            SecondEnemy();
        }
        else if (third)
        {
            //활성화 되어있지 않으면,
            if (em0010.activeSelf)
            {
                em0010.SetActive(true);
            }
        }
    }

    void firstEnemy()
    {
        bool allInactive = true;

        for (int i = 0; i < em0000.Length; i++)
        {
            if (em0000[i].activeSelf)
            {
                allInactive = false;
                Debug.Log("와와~");
                break;
            }
        }

        if (allInactive)
        {
            for (int i = 0; i < em0000_2.Length; i++)
            {
                em0000_2[i].SetActive(true);
            }
            second = true;
            first = false;
            Debug.Log("나감");
        }

    }
    void SecondEnemy()
    {
        bool allInactive = true;

        for (int i = 0; i < em0000_2.Length; i++)
        {
            if (em0000_2[i].activeSelf)
            {
                allInactive = false;
                Debug.Log("와와~");
                break;
            }
        }

        if (allInactive)
        {
            for (int i = 0; i < em0000_2.Length; i++)
            {
                em0000_2[i].SetActive(true);
            }
            second = false;
            third = true;
            Debug.Log("나감");
        }
    }


}
