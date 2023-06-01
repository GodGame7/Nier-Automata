using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] em0000;
    [SerializeField] GameObject[] em0000_2;
    [SerializeField] GameObject em0010;
    [SerializeField] GameObject em1000;

    bool first = true;
    bool second = false;
    bool third = false;
    bool four = false;
    bool allInactive = true;

    private void Update()
    {
        if (first)
        {
            for (int i = 0; i < em0000.Length; i++)
            {
                if (em0000[i].activeSelf)
                {
                    Debug.Log("³¡?");
                    allInactive = false;

                    first = false;
                    second = true;
                    break;
                }
            }
        }

        if (second)
        {
            for (int i = 0; i < em0000_2.Length; i++)
            {
                if (em0000_2[i].activeSelf)
                {
                    allInactive = false;

                    //first = false;
                    second = false;
                    third = true;
                    break;
                }
            }
        }

        if (third)
        {
            em0010.SetActive(true);
            if (em0010.activeSelf)
            {
                third = false;
            }
        }
    }
}
