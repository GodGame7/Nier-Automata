using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool cursor;
    private void Start()
    {
        CursorOff();
    }
    private void Update()
    {
        CursorManager();
    }

    private void CursorManager()
    {
        if (cursor) { 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        }
        else { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void CursorOn()
    {
        cursor = true;
    }
    public void CursorOff()
    {
        cursor = false;
    }
}
