using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasManager : MonoBehaviour
{
    [SerializeField] GameObject[] UIs;

    public void ActiveUI(int num)
    {
        UIs[num].SetActive(true);
    }

}
