using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    //
    [Header("타이틀")]
    [SerializeField] GameObject Title;

    [SerializeField] GameObject Title_Presskey;

    [SerializeField] GameObject Title_Menu;
    [SerializeField] GameObject Title_SelectLine;
    [SerializeField] GameObject Title_Warning;
    //
    [Header("메뉴")]
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject[] Menu_obj;

    [Header("뉴게임 오브젝트")]
    [SerializeField] GameObject NewGame_obj;
    [SerializeField] GameObject StartMenu_obj;
    [SerializeField] GameObject Setting_obj;
    [Header("1.New Game")]
    [SerializeField] Image[] NewGame_BackGround;
    [SerializeField] Image[] NewGame_Box;
    [SerializeField] Text[] NewGame_Text;
    [SerializeField] Text[] NewGame_Date_Text;
    [Header("시스템 메뉴 관련")]
    [SerializeField] GameObject SystemMenu;
    [SerializeField] GameObject System_Selected_Cursor;
    [SerializeField] Image[] System_BackGround;
    [SerializeField] Image[] System_Box;
    [SerializeField] Text[] System_Text;
    [Header("2.Start Menu")]
    [SerializeField] GameObject StartMenu_Selected_Zone;
    [SerializeField] Image[] StartMenu_BackGround;
    [SerializeField] Image[] StartMenu_Box;
    [SerializeField] Text[] StartMenu_Text;
    [Header("셋팅 오브젝트")]
    [SerializeField] GameObject Setting_Selected_Cursor;
    [SerializeField] Image[] SettingMenu_BackGround;
    [SerializeField] Image[] SettingMenu_Box;
    [SerializeField] Text[] SettingMenu_Text;

    [Header("5.Sound")]
    [SerializeField] GameObject Sound_Selected_Zone;
    [SerializeField] Image[] Sound_BackGround;
    [SerializeField] Image[] Sound_Box;
    [SerializeField] Text[] Sound_Text;
    [Header("사운드 셋팅")]
    [SerializeField] GameObject SoundSetting_obj;
    [SerializeField] Image[] SoundSetting_BackGround;
    [SerializeField] Image[] SoundSetting_Box;
    [SerializeField] Text[] SoundSetting_Text;
    [SerializeField] GameObject[] BGM_Obj;
    [SerializeField] GameObject BGM_Selected_Zone;
    [SerializeField] GameObject[] SFX_Obj;
    [SerializeField] GameObject SFX_Selected_Zone;
    [SerializeField] GameObject[] TalkSound_Obj;
    [SerializeField] GameObject TalkSound_Selected_Zone;


    [Header("CanNot Obj")]
    [SerializeField] GameObject CanNot_ob;

    //타이틀 변수
    private bool OnTitle = true;
    private bool OnTitleMenu = false;
    private bool OnTitleWarning = false;
    private int TitleCount = 0;
    private int MaxTitleCount = 5;
    private Vector3 MoveSelect_Position = new Vector3(0, -60f, 0);
    private Vector3 Defalut_TitleSelct_Position = new Vector3(960, 540, 0);

    //뉴게임 관련 변수
    private bool OnGameMenu = false;
    private int NewGameCount = 0;
    private int MaxNewGameCount = 3;
    private Color Default_NewGame_BackGround = new Color(172f / 255f, 166f / 255f, 138f / 255f);
    private Color Selected_NewGame_BackGronud = new Color(73f / 255f, 70f / 255f, 59f / 255f);
    private Color Default_NewGame_Txt = new Color(73f / 255f, 70f / 255f, 59f / 255f);
    private Color Selected_NewGame_Txt = new Color(189f / 255f, 179f / 255f, 166f / 255f);
    private Color Default_NewGame_Box = new Color(73f / 255f, 70f / 255f, 59f / 255f);
    private Color Selected_NewGame_Box = new Color(203f / 255f, 197f / 255f, 168f / 255f);

    //환경 설정 변수
    private bool OnSetting = false;
    private bool TitleToSetting = false;
    private bool MenuToSetting = false;
    private int SettingCount = 0;
    private int MaxSettingCount = 6;
    private Color Default_Setting_BackGround = new Color(182f / 255f, 175f / 255f, 149f / 255f);
    private Color Selected_Setting_BackGround = new Color(97f / 255f, 96f / 255f, 83f / 255f);
    private Color Default_Setting_Box = new Color(68f / 255f, 62f / 255f, 52f / 255f);
    private Color Selected_Setting_Box = new Color(182f / 255f, 175f / 255f, 149f / 255f);
    private Color Default_Setting_Txt = new Color(93f / 255f, 88f / 255f, 66f / 255f);
    private Color Selected_Setting_Txt = new Color(182f / 255f, 175f / 255f, 149f / 255f);

    //사운드 변수
    private bool OnSound = false;
    private int SoundCount = 0;
    private int MaxSoundCount = 4;
    private Color Default_Sound_BackGround = new Color(182f / 255f, 175f / 255f, 149f / 255f);
    private Color Selected_Sound_BackGround = new Color(97f / 255f, 96f / 255f, 83f / 255f);
    private Color Default_Sound_Box = new Color(68f / 255f, 62f / 255f, 52f / 255f);
    private Color Selected_Sound_Box = new Color(182f / 255f, 175f / 255f, 149f / 255f);
    private Color Default_Sound_Txt = new Color(93f / 255f, 88f / 255f, 66f / 255f);
    private Color Selected_Sound_Txt = new Color(182f / 255f, 175f / 255f, 149f / 255f);
    private Vector3 Move_Setting_SelectedZone = new Vector3(0, 85, 0);

    //사운드 셋팅 변수 
    private bool OnSoundSetting = false;
    private bool OnBGM = false;
    private bool OnSFX = false;
    private bool OnTalkSound = false;
    private int SoundSettingCount = 0;
    private int MaxSoundSettingCount = 4;
    private int BgmCount = 10;
    private int MaxBgmCount = 10;
    private int SFXCount = 10;
    private int MaxSFXCount = 10;
    private int TalkSoundCount = 10;
    private int MaxTalkSoundCount = 10;
    private Color Default_SoundSetting_BackGround = new Color(182, 175, 149);
    private Color Selected_SoundSetting_BackGround = new Color(72, 73, 62);
    private Color Default_SoundSetting_Box = new Color(68, 62, 52);
    private Color Selected_SoundSetting_Box = new Color(211, 206, 188);
    private Color Default_SoundSetting_Txt = new Color(93, 88, 66);
    private Color Selected_SoundSetting_Txt = new Color(211, 206, 188);

    //시스템 메뉴 관련 변수
    private bool OnSystemMenu = false;
    private int SystemMenuCount = 0;
    private int MaxSystemMenuCount = 2;
    private Color Defalut_System_BackGround = new Color(174f / 255f, 165f / 255f, 138f / 255f);
    private Color Selected_System_BackGround = new Color(101f / 255f, 95f / 255f, 81f / 255f);
    private Color Defalut_System_Txt = new Color(78f / 255f, 72f / 255f, 60f / 255f);
    private Color Selected_System_Txt = new Color(199f / 255f, 194f / 255f, 159f / 255f);
    private Vector3 Selected_Curosr_Position = new Vector3(250, 0, 0);
    //스타트메뉴 관련 변수
    private bool OnStartMenu = false;
    private int StartMenuCount = 0;
    private int MaxStartMenuCount = 3;
    private Color Defalut_StartMenu_BackGround = new Color(182f / 255f, 175f / 255f, 149f / 255f);
    private Color Selected_StartMenu_BackGround = new Color(96f / 255f, 93f / 255f, 81f / 255f);
    private Color Defalut_StartMenu_Txt = new Color(93f / 255f, 88f / 255f, 66f / 255f);
    private Color Selected_StartMenu_Txt = new Color(173f / 255f, 163f / 255f, 155f / 255f);
    private Color Defalut_StartMenu_Box = new Color(68f / 255f, 62f / 255f, 52f / 255f);
    private Color Selected_StartMenu_Box = new Color(191f / 255f, 183f / 255f, 153f / 255f);
    private Vector3 Selected_StartMenu_SelectZone = new Vector3(0, 85f, 0);
    #region 타이틀 관련 메서드
    private void TitlePressKey()
    {
        Title_Presskey.SetActive(false);
        Title_Menu.SetActive(true);
        OnTitle = false;
        OnTitleMenu = true;
    }
    private void TitleMenuUp()
    {
        TitleCount++;
        Title_SelectLine.transform.position += MoveSelect_Position;
    }
    private void TitleMenuDown()
    {
        TitleCount--;
        Title_SelectLine.transform.position -= MoveSelect_Position;
    }
    private void TitleMenuEnter()
    {
        Title_Warning.SetActive(false);
        Title.SetActive(false);
        Menu.SetActive(true);
        Menu_obj[TitleCount].SetActive(true);
        OnTitleWarning = false;
        OnGameMenu = true;
    }
    private void TitleMenuExit()
    {
        Title_Menu.SetActive(false);
        Title_Presskey.SetActive(true);
        OnTitle = true;
        OnTitleMenu = false;
    }
    private void TitleWarningEnter()
    {
        OnTitleMenu = false;
        OnTitleWarning = true;
        Title_Warning.SetActive(true);
    }
    private void TitleWarningExit()
    {
        OnTitleMenu = true;
        OnTitleWarning = false;
        Title_Warning.SetActive(false);
    }
    private void TitleSettingEnter()
    {
        OnTitleMenu = false;
        OnSetting = true;
        TitleToSetting = true;
        Title_Menu.SetActive(false);
        Menu.SetActive(true);
        Setting_obj.SetActive(true);
    }
    #endregion
    #region 메뉴 관련 메서드
    private void MenuExit()
    {
        NewGame_obj.SetActive(false);
        Menu.SetActive(false);
        Title.SetActive(true);
        Title_Presskey.SetActive(true);
        Title_Menu.SetActive(false);
        OnGameMenu = false;
        OnTitle = true;
        TitleCount = 0;
        Title_SelectLine.transform.position = Defalut_TitleSelct_Position;
    }
    #region 뉴게임 관련 메서드
    private void NewGameUp()
    {
        NewGameCount--;
        UpdateNewGameUI();

    }
    private void NewGameDown()
    {
        NewGameCount++;
        UpdateNewGameUI();
    }
    private void NewGameEnter()
    {
        SystemMenu.SetActive(true);
        OnSystemMenu = true;
        OnGameMenu = false;
    }


    private void UpdateNewGameUI()
    {
        for (int i = 0; i < MaxNewGameCount; i++)
        {
            NewGame_BackGround[i].color = Default_NewGame_BackGround;
            NewGame_Text[i].color = Default_NewGame_Txt;
            NewGame_Date_Text[i].color = Default_NewGame_Txt;
            NewGame_Box[i].color = Default_NewGame_Box;
        }
        NewGame_BackGround[NewGameCount].color = Selected_NewGame_BackGronud;
        NewGame_Text[NewGameCount].color = Selected_NewGame_Txt;
        NewGame_Date_Text[NewGameCount].color = Selected_NewGame_Txt;
        NewGame_Box[NewGameCount].color = Selected_NewGame_Box;

    }
    #endregion
    #region 환경설정 관련 메서드
    private void SettingUp()
    {
        SettingCount--;
        UpdateSettingUI();
        Setting_Selected_Cursor.transform.position += Move_Setting_SelectedZone;
    }
    private void SettingDown()
    {
        SettingCount++;
        UpdateSettingUI();
        Setting_Selected_Cursor.transform.position -= Move_Setting_SelectedZone;
    }
    private void UpdateSettingUI()
    {
        for (int i = 0; i < MaxSettingCount; i++)
        {
            SettingMenu_BackGround[i].color = Default_Setting_BackGround;
            SettingMenu_Box[i].color = Default_Setting_Box;
            SettingMenu_Text[i].color = Default_Setting_Txt;
        }
        SettingMenu_BackGround[SettingCount].color = Selected_Setting_BackGround;
        SettingMenu_Box[SettingCount].color = Selected_Setting_Box;
        SettingMenu_Text[SettingCount].color = Selected_Setting_Txt;

    }
    private void SettingEnter()
    {
        OnSetting = false;
        OnSound = true;
        SoundSetting_obj.SetActive(true);

    }
    private void SettingExit()
    {
        if (TitleToSetting)
        {
            OnTitleMenu = true;
            OnSetting = false;
            TitleToSetting = false;
            Title_Menu.SetActive(true);
            Title.SetActive(true);
            Menu.SetActive(false);
            Setting_obj.SetActive(false);

        }
        if (MenuToSetting)
        {
            OnStartMenu = true;
            OnSetting = false;
            MenuToSetting = false;
            StartMenu_obj.SetActive(true);
            Setting_obj.SetActive(false);
            
        }
    }

    #endregion
    #region 사운드 관련 메서드
    private void SoundUpArrow()
    {
        SoundCount--;
        UpdateSoundSetting();
        Sound_Selected_Zone.transform.position += Move_Setting_SelectedZone;
    }
    private void SoundDownArrow()
    {
        SoundCount++;
        UpdateSoundSetting();
        Sound_Selected_Zone.transform.position -= Move_Setting_SelectedZone;
    }
    private void UpdateSoundSetting()
    {
        for (int i = 0; i < MaxSoundCount; i++)
        {
            Sound_BackGround[i].color = Default_Sound_BackGround;
            Sound_Box[i].color = Default_Sound_Box;
            Sound_Text[i].color = Default_Sound_Txt;
        }
        Sound_BackGround[SoundCount].color = Selected_Sound_BackGround;
        Sound_Box[SoundCount].color = Selected_Sound_Box;
        Sound_Text[SoundCount].color = Selected_Sound_Txt;
    }
    private void EnterBGMSetting()
    {
        OnSound = false;
        OnSoundSetting = true;
        OnBGM = true;
    }
    private void EnterSFXSetting()
    {
        OnSound = false;
        OnSoundSetting = true;
        OnSFX = true;
    }
    private void EnterTalkSoundSetting()
    {
        OnSound = false;
        OnSoundSetting = true;
        OnTalkSound = true;
    }
    private void SoundExit()
    {
        OnSound = false;
        OnSetting = true;
        SoundSetting_obj.SetActive(false);
    }
    #endregion
    #region 사운드셋팅 관련 메서드
    private void BGMLeft()
    {
        BgmCount--;
        BGMUpdateUI();
        //브금 소리 줄이기
        AudioManager.Instance.SetBgmVolume(BgmCount);
    }
    private void BGMRight()
    {
        BgmCount++;
        BGMUpdateUI();
        AudioManager.Instance.SetBgmVolume(BgmCount);
        //브금 소리늘리기
    }
    private void BGMUpdateUI()
    {
        for (int i = 0; i < MaxBgmCount; i++)
        {
            BGM_Obj[i].SetActive(false);
        }
        for (int i = 0; i < BgmCount; i++)
        {
            BGM_Obj[i].SetActive(true);
        }
    }
    private void SFXLeft()
    {
        SFXCount--;
        SFXUpdateUI();
        AudioManager.Instance.SetSfxVolume(SFXCount);
        //SFX 소리줄이기
    }
    private void SFXRight()
    {
        SFXCount++;
        SFXUpdateUI();
        AudioManager.Instance.SetSfxVolume(SFXCount);
        //SFX 소리 늘리기
    }
    private void SFXUpdateUI()
    {
        for (int i = 0; i < MaxSFXCount; i++)
        {
            SFX_Obj[i].SetActive(false);
        }
        for (int i = 0; i < SFXCount; i++)
        {
            SFX_Obj[i].SetActive(true);
        }
    }
    private void SoundTalkLeft()
    {
        TalkSoundCount--;
        UpdateSoundTalkUI();
        //대사 소리 줄이기
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.float_talkSound = TalkSoundCount;



    }
    private void SoundTalkRight()
    {
        TalkSoundCount++;
        UpdateSoundTalkUI();
        //대사 소리 늘리기
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.float_talkSound = TalkSoundCount;
    }
    private void UpdateSoundTalkUI()
    {
        for (int i = 0; i < MaxTalkSoundCount; i++)
        {
            TalkSound_Obj[i].SetActive(false);
        }
        for (int i = 0; i < TalkSoundCount; i++)
        {
            TalkSound_Obj[i].SetActive(true);
        }
    }
    private void SoundSettingExit()
    {
        OnSoundSetting = false;
        OnBGM = false;
        OnSFX = false;
        OnTalkSound = false;
        OnSound = true;
    }
    #endregion
    #region 시스템 메뉴 관련 메서드
    private void SystemMenuLeft()
    {
        SystemMenuCount--;
        System_Selected_Cursor.transform.position -= Selected_Curosr_Position;
        UpdateSystemMenuUI();
    }
    private void SystemMenuRight()
    {
        SystemMenuCount++;
        System_Selected_Cursor.transform.position += Selected_Curosr_Position;
        UpdateSystemMenuUI();
    }
    private void SystemMenuEnter()
    {
        SystemMenu.SetActive(false);
        OnSystemMenu = false;
        OnStartMenu = true;
        NewGame_obj.SetActive(false);
        StartMenu_obj.SetActive(true);


    }
    private void UpdateSystemMenuUI()
    {
        for (int i = 0; i < MaxSystemMenuCount; i++)
        {
            System_BackGround[i].color = Defalut_System_BackGround;
            System_Box[i].color = Defalut_System_Txt;
            System_Text[i].color = Defalut_System_Txt;
        }
        System_BackGround[SystemMenuCount].color = Selected_System_BackGround;
        System_Box[SystemMenuCount].color = Selected_System_Txt;
        System_Text[SystemMenuCount].color = Selected_System_Txt;
    }
    private void SystemMenuExit()
    {
        SystemMenu.SetActive(false);
        OnSystemMenu = false;
        OnGameMenu = true;
    }
    #endregion
    #region 스타트메뉴 관련 메서드
    private void StartMenuUp()
    {
        StartMenuCount--;
        StartMenu_Selected_Zone.transform.position += Selected_StartMenu_SelectZone;
        UpdateStartMenuUI();
    }
    private void StartMenuDown()
    {
        StartMenuCount++;
        StartMenu_Selected_Zone.transform.position -= Selected_StartMenu_SelectZone;
        UpdateStartMenuUI();
    }
    private void StartMenuEnter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FlagFight");
    }
    private void StartMenuEnterSetting()
    {
        OnStartMenu = false;
        OnSetting = true;
        MenuToSetting = true;
        StartMenu_obj.SetActive(false);
        Setting_obj.SetActive(true);

    }
    private void StartMenuExit()
    {
        OnStartMenu = false;
        OnGameMenu = true;
        StartMenu_obj.SetActive(false);
        NewGame_obj.SetActive(true);
    }
    private void UpdateStartMenuUI()
    {
        for (int i = 0; i < MaxStartMenuCount; i++)
        {
            StartMenu_BackGround[i].color = Defalut_StartMenu_BackGround;
            StartMenu_Text[i].color = Defalut_StartMenu_Txt;
            StartMenu_Box[i].color = Defalut_StartMenu_Box;
        }
        StartMenu_BackGround[StartMenuCount].color = Selected_StartMenu_BackGround;
        StartMenu_Text[StartMenuCount].color = Selected_StartMenu_Txt;
        StartMenu_Box[StartMenuCount].color = Selected_StartMenu_Box;
    }
    #endregion

    #endregion
    private void Update()
    {

        if (OnTitle)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                    return;
                }
                else
                {
                    TitlePressKey();
                    return;
                }
            }
        }
        if (OnTitleMenu)
        {
            if (TitleCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TitleMenuDown();
                }
            }
            if (TitleCount < MaxTitleCount - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    TitleMenuUp();
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {

                if (TitleCount == 1)
                {
                    TitleWarningEnter();
                    return;
                }
                if (TitleCount == 2)
                {
                    TitleSettingEnter();
                    return;
                }
                if (TitleCount == 4)
                {
                    Application.Quit();
                }
                else
                {
                    CantPlay();
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                TitleMenuExit();
                return;
            }
        }
        if (OnTitleWarning)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                TitleMenuEnter();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TitleWarningExit();
                return;
            }
        }
        if (OnGameMenu)
        {
            if (NewGameCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    NewGameUp();
                }
            }
            if (NewGameCount < MaxNewGameCount - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    NewGameDown();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (NewGameCount != 2)
                {
                    NewGameEnter();
                    return;
                }
                else
                {
                    NewGameEnter();
                    SystemMenuEnter();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuExit();
                return;
            }
        }
        if (OnSetting)
        {
            if (SettingCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SettingUp();
                }
            }
            if (SettingCount < MaxSettingCount - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SettingDown();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (SettingCount == 4)
                {
                    SettingEnter();
                    return;
                }
                else
                {
                    CantPlay();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SettingExit();
                return;
            }
        }
        if (OnSound)
        {
            if (SoundCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SoundUpArrow();
                }
            }
            if (SoundCount < MaxSoundCount - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SoundDownArrow();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (SoundCount == 0)
                {
                    EnterBGMSetting();
                }
                if (SoundCount == 1)
                {
                    EnterSFXSetting();
                }
                if (SoundCount == 2)
                {
                    EnterTalkSoundSetting();
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SoundExit();
            }
        }
        if (OnSoundSetting)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (OnBGM)
                {
                    BGMLeft();
                }
                else if (OnSFX)
                {
                    SFXLeft();
                }
                else if (OnTalkSound)
                {
                    SoundTalkLeft();
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (OnBGM)
                {
                    BGMRight();
                }
                else if (OnSFX)
                {
                    SFXRight();
                }
                else if (OnTalkSound)
                {
                    SoundTalkRight();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SoundSettingExit();
            }
        }
        if (OnSystemMenu)
        {
            if (SystemMenuCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SystemMenuLeft();
                }
            }
            if (SystemMenuCount < MaxSystemMenuCount - 1)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SystemMenuRight();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (SystemMenuCount == 0)
                {
                    SystemMenuEnter();
                    return;
                }
                else
                {
                    SystemMenuExit();
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SystemMenuExit();
                return;
            }
        }
        if (OnStartMenu)
        {
            if (StartMenuCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    StartMenuUp();
                }
            }
            if (StartMenuCount < MaxStartMenuCount - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartMenuDown();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (StartMenuCount == 0)
                {
                    StartMenuEnter();
                    return;
                }
                if (StartMenuCount == 1)
                {
                    StartMenuEnterSetting();
                }
                else
                {
                    CantPlay();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartMenuExit();
                return;
            }
        }
    }
    private void CantPlay() //미구현입니다 뜨는거
    {
        CanNot_ob.SetActive(true);

        Invoke("CannotFalse", 1f);
    }
    private void CannotFalse()
    {
        CanNot_ob.SetActive(false);
    }
}
