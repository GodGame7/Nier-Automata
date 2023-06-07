using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSubTitleTextManager : MonoBehaviour
{

    [SerializeField] FlagFightSubTitleAudio flagFightSubTitleAudio;

    [Space(0.5f)]
    [Header("확인용")]
    [SerializeField] Text text_Subtitle;
    [SerializeField] int subTitleCounter;
    [SerializeField] FlagFightSubTitleAudio SubTitleAudio;

    string[] flagSubTitles = new string[]
    {
        "이것이 목표 대형 병기!?",
        "부정 : 해당 적은 목표가 아님",
        "신속한 제거를 권장",
        "쉽게 말하는군⋯⋯!"
    };

    // Start is called before the first frame update
    void Start()
    {
        subTitleCounter = 0;
        text_Subtitle = GetComponentInChildren<Text>();
    }

    public void NextSubText()
    {
        text_Subtitle.text = flagSubTitles[subTitleCounter];
        text_Subtitle.gameObject.SetActive(true);
        subTitleCounter++;
    }

    public void OffSubText()
    {
        text_Subtitle.gameObject.SetActive(false);
    }

    public void PlayClip(int subTitleCounter)
    {
        flagFightSubTitleAudio.PlayClip(subTitleCounter);
    }
}
