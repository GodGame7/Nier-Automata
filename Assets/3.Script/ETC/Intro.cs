using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    //
    [Header("타이틀")]
    [SerializeField] GameObject Title;
 
    [SerializeField] GameObject Title_Presskey;

    [SerializeField] GameObject Title_Menu;
    [SerializeField] GameObject Title_SelectLine;
    [SerializeField] GameObject Warning;
    //
    [Header("메뉴")]
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject[] Menu_obj;

    [Header("뉴게임 오브젝트")]
    [SerializeField] GameObject[] NewGame_obj;
    [Header("1.New Game")]
    [SerializeField] GameObject SystemMenu;
    [SerializeField] Image[] NewGame_BackGround;
    [SerializeField] Image[] NewGame_Box;
    [SerializeField] Text[] NewGame_Text;
    [SerializeField] Text[] NewGame_Date_Text;

    [Header("2.Start Menu")]
    [SerializeField] GameObject Selected_Cursor;
    [SerializeField] Image[] StartMenu_BackGround;
    [SerializeField] Image[] StartMenu_Box;
    [SerializeField] Text[] StartMenu_Text;

    //타이틀 변수
    private int TitleCount = 0;
    private Vector3 MoveSelect_Position = new Vector3(0, -60f, 0);
    private Vector3 Defalut_TitleSelct_Position = new Vector3(960, -540, 0);

    //뉴게임 관련 변수
    private bool OnSystemMenu = false;
    private int NewGameCount = 0;
    private int MaxNewGameCount = 3;
    //스타트메뉴 관련 변수

    #region 타이틀 관련 메서드
    private void PressKey()
    {
        Title_Presskey.SetActive(false);
        Title_Menu.SetActive(true);
    }
    private void TitleUp()
    {
        TitleCount++;
        Title_SelectLine.transform.position += MoveSelect_Position;
    }
    private void TitleDown()
    {
        TitleCount--;
        Title_SelectLine.transform.position -= MoveSelect_Position;
    }
    private void TitleEnter()
    {
        Title.SetActive(false);
        Menu.SetActive(true);
        Menu_obj[TitleCount].SetActive(true);
    }
    private void TitleExit()
    {
        Title_Menu.SetActive(false);
        Title_Presskey.SetActive(false);
    }
    #endregion
    #region 메뉴 관련 메서드
    private void MenuExit()
    {
        Menu.SetActive(false);
        Title.SetActive(true);
        TitleCount = 0;
        Title_SelectLine.transform.position = Defalut_TitleSelct_Position;
    }
    #region 뉴게임 관련 메서드
    private void NewGameUp()
    {
        NewGameCount++;
        UpdateNewGameUI();
        
    }
    private void NewGameDown()
    {
        NewGameCount--;
        UpdateNewGameUI();
    }
    private void NewGameEnter()
    {
        SystemMenu.SetActive(true);
        OnSystemMenu = true;
    }
    private void NewGameExit()
    {
        SystemMenu.SetActive(false);
        OnSystemMenu = false;
    }
    
    private void UpdateNewGameUI()
    {
        for (int i=0; i< MaxNewGameCount; i++)
        {

        }
    }
    #endregion
    #region 스타트메뉴 관련 메서드
    private void StartMenuUp()
    {

    }
    private void StartMenuDown()
    {

    }
    private void StartMenuEnter()
    {

    }
    private void UpdateStartMenuUI()
    {

    }
    #endregion

    #endregion
}
