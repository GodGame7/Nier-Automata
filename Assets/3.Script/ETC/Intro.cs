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
    [Header("1.New Game")]
    [SerializeField] GameObject SystemMenu;
    [SerializeField] Image[] NewGame_BackGround;
    [SerializeField] Image[] NewGame_Box;
    [SerializeField] Text[] NewGame_Text;
    [SerializeField] Text[] NewGame_Date_Text;
    [Header("시스템 메뉴 관련")]
    [SerializeField] GameObject System_Selected_Cursor;
    [SerializeField] Image[] System_BackGround;
    [SerializeField] Image[] System_Box;
    [SerializeField] Text[] System_Text;
    [Header("2.Start Menu")]
    [SerializeField] GameObject StartMenu_Selected_Zone;
    [SerializeField] Image[] StartMenu_BackGround;
    [SerializeField] Image[] StartMenu_Box;
    [SerializeField] Text[] StartMenu_Text;

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
    private Vector3 Selected_StartMenu_SelectZone = new Vector3(0,85f,0);
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
    #endregion
    #region 메뉴 관련 메서드
    private void MenuExit()
    {
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
                    Debug.Log("들어옴");
                    TitleWarningEnter();
                    return;
                }

                else
                {
                    StartCoroutine(CantPlay());
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
                Debug.Log("이거들어옴?");
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
            if (NewGameCount != 2)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    NewGameEnter();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuExit();
                return;
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
            if (SystemMenuCount == 0)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    SystemMenuEnter();
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
            if (StartMenuCount == 0)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    StartMenuEnter();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartMenuExit();
                return;
            }
        }
    }
    private IEnumerator CantPlay() //미구현입니다 뜨는거
    {
        CanNot_ob.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        CanNot_ob.SetActive(false);
    }
}
