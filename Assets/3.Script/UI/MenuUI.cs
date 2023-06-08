using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int ItemMenuCount = 0;

    // 인풋값 확인하기위한 불값
    public bool OpenMenu = false;
    public bool EnterMenuItem = false;

    //탑메뉴 색상
    private Color Select_Color = new Color(65f / 255f, 61f / 255f, 53f / 255f);
    private Color Default_Color = new Color(167f / 255f, 160f / 255f, 134f / 255f);
    private Color Select_Text_Color = new Color(157f / 255f, 153f / 255f, 139 / 255f);
    private Color Default_Text_Color = new Color(87f / 255f, 82f / 255f, 65 / 255f);

    //마우스 커서 위치용 변수 
    private Vector3 Defalut_TopMenu_Cursor = new Vector3(-860, 390, 0);
    private Vector3 Move_TopMenu_Cursor = new Vector3(250, 0, 0);
    private Vector3 Defalut_Selected_Cursor = new Vector3(960, 535, 0);
    private Vector3 Move_Selected_Cursor = new Vector3(0, 75f, 0);

    //인벤토리용 변수
    public int InvenLength;
    private int MaxInvenLength = 3;
    private Color Selected_Text_Color = new Color(164f / 255f, 162f / 255f, 147f / 255f);
    private Color Selected_Image_Color = new Color(180f / 255f, 178f / 255f, 163f / 255f);

    //시스템 용 변수
    private bool OnSystem = false;
    private int SystemCount = 0;
    private int MaxSystemCount = 6;
    private Color Default_System_BackGround = new Color(182f / 255f, 175f / 255f, 149f / 255f);
    private Color Selected_System_BackGround = new Color(98f / 255f, 94f / 255f, 85f / 255f);
    private Color Default_System_Box = new Color(68f / 255f, 62f / 255f, 52f / 255f);
    private Color Selected_System_Box = new Color(191f / 255f, 183f / 255f, 159f / 255f);
    private Color Default_System_Txt = new Color(99f / 255f, 94f / 255f, 71f / 255f);
    private Color Selected_System_Txt = new Color(171f / 255f, 165f / 255f, 154f / 255f);
    private Vector3 Selected_System_Zone = new Vector3(0,85,0);
    //온오프할 메뉴들
    [Header("오브젝트를 넣어주세용")]
    [SerializeField] private GameObject MenuUI_ob;
    [Tooltip("미구현 Obj")]
    [SerializeField] private GameObject CanNot_ob;
    [Tooltip("하단 메뉴들")]
    [SerializeField] private GameObject[] BottomMenu;
    [SerializeField] private GameObject Selected_Menu; //배열로 만들어줘야함 나중에 확장성
    [Tooltip("상단 메뉴 이미지")]
    [SerializeField] private Image[] TopImage;
    [Tooltip("상단 메뉴 텍스트")]
    [SerializeField] private Text[] TopText;
    [Tooltip("상단 메뉴 마우스 커서")]
    [SerializeField] private Image Cursor;

    [Header("멈추기 위한 변수")]
    [Tooltip("카메라를 넣어주세용")]
    [SerializeField] Camera MainCam;

    [Header("아이템 리스트 텍스트")]
    [Header("아이템 사용을 위한 변수들")]
    [Space(50)]
    [SerializeField] Text[] ItemList_Txt;

    [Header("아이템 리스트 갯수")]
    [SerializeField] Text[] ItemList_Amount;

    [Header("아이템 리스트 이미지")]
    [SerializeField] Image[] ItemList_Image;

    [Header("아이템 설명")]
    [SerializeField] Text Item_Header;
    [SerializeField] Text Item_Explain;
    [SerializeField] Text Item_Amount;

    [Header("선택 영역")]
    [SerializeField] GameObject Selected_Cursor;

    public delegate void Item(int num);
    public static event Item UseItem;

    [Header("시스템 영역")]
    [SerializeField] GameObject System_Selected_Zone;
    [SerializeField] Image[] System_BackGround;
    [SerializeField] Image[] System_Box;
    [SerializeField] Text[] System_Text;
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //메뉴창 열기
        {
            if (OpenMenu && !EnterMenuItem)
            {
                MenuClose();
            }
            else if (!OpenMenu)
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
                    AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);

                }
            }
            if (MenuCount >= 1) //좌측 화살표 클릭시 왼쪽 넘어가게
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    LeftArrow();
                    AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Return))
            {


                if (MenuCount == 2)
                {
                    if (InvenLength != 0)
                    {
                        EnterItem();
                        AudioManager.Instance.PlaySfx(Define.SFX.UI_Space);
                    }
                    return;
                }
                else if (MenuCount == 6)
                {
                    EnterSystem();
                    AudioManager.Instance.PlaySfx(Define.SFX.UI_Space);
                    return;
                }

                else//아이템 이외엔 미구현
                {
                    StartCoroutine(CantPlay());
                }

            }
        }
        if (EnterMenuItem) //아이템 메뉴창에서 조작
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitItem();
                AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);
                return;
            }
            if (ItemMenuCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ItemUpArrow();
                    AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);
                }
            }
            if (ItemMenuCount < InvenLength - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ItemDownArrow();
                    AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);

                }
            }


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                ItemEnter();
                AudioManager.Instance.PlaySfx(Define.SFX.UI_Space);
                return;
            }


        }
        if (OnSystem)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (SystemCount > 0)
                {
                    SystemUp();
                    AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (SystemCount < MaxSystemCount-1)
                {
                    SystemDown();
                    AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (SystemCount == 4)
                {
                    SystemGoTitle();
                    return;
                }
                else if (SystemCount == 5)
                {
                    SystemGameEnd();
                    return;
                }
                else
                {
                    StartCoroutine(CantPlay());
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SystemExit();
                AudioManager.Instance.PlaySfx(Define.SFX.UI_Move);
            }
        }

    }
    #region 메뉴창
    public void MenuOpen() //메뉴창 열기
    {
        if (TryGetComponent(out CameraMovement MainCam1))
        {
            MainCam1.enabled = false;
        }
        MenuCount = 0;
        Time.timeScale = 0;
        OpenMenu = true;
        MenuUI_ob.SetActive(true);
        UpdateMenuUI();
        Cursor.gameObject.transform.localPosition = Defalut_TopMenu_Cursor;
        InvenLength = PlayerData.Instance.inven.Items.Count;
    }
    public void MenuClose() //메뉴창 닫기
    {
        if (TryGetComponent(out CameraMovement MainCam1))
        {
            MainCam1.enabled = true;
        }

        //MainCam.GetComponent<CameraMovement>().enabled = true;
        MenuCount = 0;
        Time.timeScale = 1;
        OpenMenu = false;
        MenuUI_ob.SetActive(false);
        for (int i = 0; i < BottomMenu.Length; i++)
        {
            BottomMenu[i].SetActive(false);
        }

    }
    #endregion
    #region 탑 메뉴창
    public void LeftArrow() //탑메뉴창 왼쪽 넘기기
    {
        MenuCount--;
        UpdateMenuUI();
        Cursor.transform.position -= Move_TopMenu_Cursor;
    }
    public void RightArrow() //탑메뉴창 오른쪽넘기기
    {
        MenuCount++;
        UpdateMenuUI();
        Cursor.transform.position += Move_TopMenu_Cursor;
    }
    public void EnterItem() // 탑메뉴창에서 아이템으로 넘어가기
    {
        Selected_Cursor.transform.position = Defalut_Selected_Cursor;
        ItemMenuCount = 0;
        EnterMenuItem = true;
        Selected_Menu.SetActive(true);
        UpdateMenuBottom();

    }
    public void EnterSystem()
    {
        OnSystem = true;
        OpenMenu = false;

    }
    public void ExitItem() // 탑메뉴창으로 돌아가기
    {
        ItemMenuCount = 0;
        EnterMenuItem = false;
        Selected_Menu.SetActive(false);
    }

    
    private void UpdateMenuUI() // 메뉴들 UI 업데이트 
    {
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
    private void UpdateMenuBottom() //하단 메뉴 UI 업데이트
    {
        InvenLength = PlayerData.Instance.inven.Items.Count;
        ClearSlot();
        SetSlot();
    }
    #endregion
    #region 아이템 메뉴
    public void ItemUpArrow() //아이템 메뉴 위화살표 클릭시
    {
        Selected_Cursor.transform.position += Move_Selected_Cursor;
        ItemMenuCount--;
        UpdateMenuBottom();
    }

    public void ItemDownArrow() //아이템 메뉴 아래화살표 클릭시
    {
        Selected_Cursor.transform.position -= Move_Selected_Cursor;
        ItemMenuCount++;
        UpdateMenuBottom();
    }
    public void ItemEnter() //아이템 메뉴 엔터클릭시 아이템 사용
    {
        UseItem?.Invoke(ItemMenuCount);
        UpdateMenuBottom();
    }

    private void ClearSlot()
    {
        for (int i = 0; i < MaxInvenLength; i++)
        {
            ItemList_Txt[i].text = "";
            ItemList_Amount[i].text = "";
            ItemList_Image[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < InvenLength; i++)
        {
            ItemList_Txt[i].text = string.Format("{0}", PlayerData.Instance.inven.Items[i].ItemName);
            ItemList_Amount[i].text = string.Format("{0}", PlayerData.Instance.inven.Items[i].Quantity);
            ItemList_Txt[i].color = Color.black;
            ItemList_Image[i].gameObject.SetActive(true);
            ItemList_Image[i].color = Color.black;
            ItemList_Amount[i].color = Color.black;
        }
    }
    private void SetSlot()
    {
        if (PlayerData.Instance.inven.Items.Count == 0)
        {
            return;
        }
        Item_Header.text = string.Format("{0}", PlayerData.Instance.inven.Items[ItemMenuCount].ItemName);
        Item_Explain.text = string.Format("{0}", PlayerData.Instance.inven.Items[ItemMenuCount].Tooltip);
        Item_Amount.text = string.Format("소지수 : {0} / 99", PlayerData.Instance.inven.Items[ItemMenuCount].Quantity);
        ItemList_Txt[ItemMenuCount].color = Selected_Text_Color;
        ItemList_Amount[ItemMenuCount].color = Selected_Text_Color;
        ItemList_Image[ItemMenuCount].color = Selected_Image_Color;

    }
    #endregion
    #region 시스템 설정
    private void SystemUp()
    {
        SystemCount--;
        UpdateSystemUI();
        System_Selected_Zone.transform.position += Selected_System_Zone;
    }
    private void SystemDown()
    {
        SystemCount++;
        UpdateSystemUI();
        System_Selected_Zone.transform.position -= Selected_System_Zone;
    }
    private void SystemExit()
    {
        OnSystem = false;
        OpenMenu = true;

    }
    private void UpdateSystemUI()
    {
        for (int i = 0; i < MaxSystemCount; i++)
        {
            System_BackGround[i].color = Default_System_BackGround;
            System_Box[i].color = Default_System_Box;
            System_Text[i].color = Default_System_Txt;
        }
        System_BackGround[SystemCount].color = Selected_System_BackGround;
        System_Box[SystemCount].color = Selected_System_Box;
        System_Text[SystemCount].color = Selected_System_Txt;

    }
    private void SystemGoTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
    }
    private void SystemGameEnd()
    {
        Application.Quit();
    }
    #endregion
    private IEnumerator CantPlay() //아이템 이외엔 미구현입니다 뜨는거
    {
        CanNot_ob.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        CanNot_ob.SetActive(false);
    }
}
