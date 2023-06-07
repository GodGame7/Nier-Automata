using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideControl : MonoBehaviour
{
    [SerializeField] GameObject[] Insides;
    [SerializeField] GameObject Exit;
    [SerializeField] FlagFightSubTitleManager flagFightSubTitleManager;
    [SerializeField] float insideSpeed = 2;


    // Start is called before the first frame update
    void Start()
    {
        flagFightSubTitleManager = FindObjectOfType<FlagFightSubTitleManager>();
        flagFightSubTitleManager.phase7_02.AddListener(phase7_02);
        flagFightSubTitleManager.phase7_07.AddListener(Phase7_07);
    }

    void phase7_02()
    {
        Exit.SetActive(true);
        for (int i = 0; i < Insides.Length; i++)
        {
            Insides[i].SetActive(true);
        }
    }

    void Phase7_07()
    {
        ObjectScrolling objectScrolling = Exit.GetComponent<ObjectScrolling>();
        objectScrolling.scrollingSpeed = 0;
        for(int i = 0; i < Insides.Length; i++)
        {
            objectScrolling = Insides[i].GetComponent<ObjectScrolling>();
            objectScrolling.scrollingSpeed = insideSpeed;
        }
    
    }

    public void ExitSpeed(float Speed)
    {
        ObjectScrolling objectScrolling = Exit.GetComponent<ObjectScrolling>();
        objectScrolling.scrollingSpeed = Speed;
    }



}
