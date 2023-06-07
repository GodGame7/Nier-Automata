using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] GameObject[] pipes;
    
    public void OnPipe()
    {
        for(int i = 0; i < pipes.Length; i++)
        {
            pipes[i].SetActive(true);
        }
    }

    public void OffPipe()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].SetActive(false);
        }
    }

    public void SetSpeed(float speed)
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            PipeControl pipeControl = pipes[i].GetComponent<PipeControl>();
            pipeControl.scrollingSpeed = speed;
        }
    }
}
