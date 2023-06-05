using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private enum Menu
    {
        Map,
        Quest,
        Item,
        Weapon,
        Skill,
        Data,
        System
    }

    // 몇번메뉴인지 확인하기위함 
    private int MenuCount = 0;

    // 인풋값 확인하기위한 불값
    private bool OpenMenu = false;
    private bool EnterMenuItem = false;

    //탑메뉴 색상
    private Color Select_Color = new Color(65f / 255f, 61f / 255f, 53f / 255f);
    private Color Default_Color = new Color(167f / 255f, 160f / 255f, 134f / 255f);
    private Color Select_Text_Color = new Color(157f / 255f, 153f / 255f, 139 / 255f);
    private Color Default_Text_Color = new Color(87f / 255f, 82f / 255f, 65 / 255f);
    //온오프할 메뉴들
    [Header("오브젝트를 넣어주세용")]
    [SerializeField] private GameObject MenuUI_ob;
    [SerializeField] private GameObject CanNot_ob;
    [SerializeField] private GameObject[] BottomMenu;
    [SerializeField] private Image[] TopImage;
    [SerializeField] private Text[] TopText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //메뉴창 열기
        {
            if (OpenMenu)
            {
                MenuClose();
            }
            else
            {
                MenuOpen();
            }

        }
        if (OpenMenu && !EnterMenuItem) // 인벤토리가 열렸을 때 탑메뉴창 조작
        {

            if (MenuCount < 6) //우측화살표 클릭시 오른쪽 넘어가게
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    RightArrow();
                }
            }
            if (MenuCount >= 1) //좌측 화살표 클릭시 왼쪽 넘어가게
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    LeftArrow();
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (MenuCount != 2) //아이템 이외엔 미구현
                {
                    StartCoroutine(CantPlay());
                }
                else  //아이템에서 엔터클릭시 진행 
                {
                    EnterItem();
                }
            }
        }
        if (EnterMenuItem) //아이템 메뉴창에서 조작
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitItem();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ItemUpArrow();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ItemDownArrow();

            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ItemEnter();
            }
        }
    }
    public void MenuOpen() //메뉴창 열기
    {
        MenuCount = 0;
        Time.timeScale = 0;
        OpenMenu = true;
        MenuUI_ob.SetActive(true);
        UpdateMenuUI();
    }
    public void MenuClose() //메뉴창 닫기
    {
        MenuCount = 0;
        Time.timeScale = 1;
        OpenMenu = false;
        MenuUI_ob.SetActive(false);
    }
    public void LeftArrow() //탑메뉴창 왼쪽 넘기기
    {
        MenuCount--;
        UpdateMenuUI();
    }
    public void RightArrow() //탑메뉴창 오른쪽넘기기
    {
        MenuCount++;
        UpdateMenuUI();
    }
    public void EnterItem() // 탑메뉴창에서 아이템으로 넘어가기
    {
        EnterMenuItem = true;

    }
    public void ExitItem() // 탑메뉴창으로 돌아가기
    {
        EnterMenuItem = false;
        
    }
   
    private void UpdateMenuUI() // 메뉴들 UI 업데이트 
    {
        Debug.Log(MenuCount);
        for (int i = 0; i < BottomMenu.Length; i++)
        {
            BottomMenu[i].SetActive(false);
            TopImage[i].color = Default_Color;
            TopText[i].color = Default_Text_Color;
        }
        BottomMenu[MenuCount].SetActive(true);
        TopImage[MenuCount].color = Select_Color;
        TopText[MenuCount].color = Select_Text_Color;

    }
    public void ItemUpArrow() //아이템 메뉴 위화살표 클릭시
    {

    }
    public void ItemDownArrow() //아이템 메뉴 아래화살표 클릭시
    {

    }
    public void ItemEnter() //아이템 메뉴 엔터클릭시 아이템 사용
    {

    }
    private IEnumerator CantPlay() //아이템 이외엔 미구현입니다 뜨는거
    {
        CanNot_ob.SetActive(true);
        yield return new WaitForSeconds(2f);
        CanNot_ob.SetActive(false);
    }
}
