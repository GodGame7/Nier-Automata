using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideControl : MonoBehaviour
{
    [SerializeField] GameObject[] Insides;
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
        for(int i = 0; i < Insides.Length; i++)
        {
            Insides[i].SetActive(true);
        }
    }

    void Phase7_07()
    {
        for(int i = 0; i < Insides.Length; i++)
        {
            ObjectScrolling objectScrolling = Insides[i].GetComponent<ObjectScrolling>();
            objectScrolling.scrollingSpeed = insideSpeed;
        }
    
    }

}
