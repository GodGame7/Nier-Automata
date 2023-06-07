using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeControl : MonoBehaviour
{
    [SerializeField] float scrollingSpeed;

    void Update()
    {
        transform.Translate(Vector3.back * scrollingSpeed * Time.deltaTime);
    }
}
