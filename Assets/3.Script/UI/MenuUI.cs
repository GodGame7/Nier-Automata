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

    // ����޴����� Ȯ���ϱ����� 
    private int MenuCount = 0;
    private int ItemMenuCount = 0;

    // ��ǲ�� Ȯ���ϱ����� �Ұ�
    private bool OpenMenu = false;
    private bool EnterMenuItem = false;

    //ž�޴� ����
    private Color Select_Color = new Color(65f / 255f, 61f / 255f, 53f / 255f);
    private Color Default_Color = new Color(167f / 255f, 160f / 255f, 134f / 255f);
    private Color Select_Text_Color = new Color(157f / 255f, 153f / 255f, 139 / 255f);
    private Color Default_Text_Color = new Color(87f / 255f, 82f / 255f, 65 / 255f);

    //���콺 Ŀ�� ��ġ�� ���� 
    private Vector3 Defalut_TopMenu_Cursor = new Vector3(-860, 390, 0);
    private Vector3 Move_TopMenu_Cursor = new Vector3(250, 0, 0);
    private Vector3 Defalut_Selected_Cursor = new Vector3(960, 535, 0);
    private Vector3 Move_Selected_Cursor = new Vector3(0, 75f, 0);

    //�κ��丮�� ����
    private int InvenLength;
    private int MaxInvenLength = 3;
    private Color Selected_Text_Color = new Color(164f / 255f, 162f / 255f, 147f / 255f);
    private Color Selected_Image_Color = new Color(180f / 255f, 178f / 255f, 163f / 255f);
    //�¿����� �޴���
    [Header("������Ʈ�� �־��ּ���")]
    [SerializeField] private GameObject MenuUI_ob;
    [Tooltip("�̱��� Obj")]
    [SerializeField] private GameObject CanNot_ob;
    [Tooltip("�ϴ� �޴���")]
    [SerializeField] private GameObject[] BottomMenu;
    [SerializeField] private GameObject Selected_Menu; //�迭�� ���������� ���߿� Ȯ�强
    [Tooltip("��� �޴� �̹���")]
    [SerializeField] private Image[] TopImage;
    [Tooltip("��� �޴� �ؽ�Ʈ")]
    [SerializeField] private Text[] TopText;
    [Tooltip("��� �޴� ���콺 Ŀ��")]
    [SerializeField] private Image Cursor;

    [Header("���߱� ���� ����")]
    [Tooltip("ī�޶� �־��ּ���")]
    [SerializeField] Camera MainCam;

    [Header("������ ����Ʈ �ؽ�Ʈ")]
    [Header("������ ����� ���� ������")]
    [Space(50)]
    [SerializeField] Text[] ItemList_Txt;

    [Header("������ ����Ʈ ����")]
    [SerializeField] Text[] ItemList_Amount;

    [Header("������ ����Ʈ �̹���")]
    [SerializeField] Image[] ItemList_Image;

    [Header("������ ����")]
    [SerializeField] Text Item_Header;
    [SerializeField] Text Item_Explain;
    [SerializeField] Text Item_Amount;

    [Header("���� ����")]
    [SerializeField] GameObject Selected_Cursor;

    public delegate void Item(int num);
    public static event Item UseItem;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //�޴�â ����
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
        if (OpenMenu && !EnterMenuItem) // �κ��丮�� ������ �� ž�޴�â ����
        {

            if (MenuCount < 6) //����ȭ��ǥ Ŭ���� ������ �Ѿ��
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    RightArrow();
                }
            }
            if (MenuCount >= 1) //���� ȭ��ǥ Ŭ���� ���� �Ѿ��
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    LeftArrow();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (MenuCount != 2) //������ �̿ܿ� �̱���
                {
                    StartCoroutine(CantPlay());
                }
                else  //�����ۿ��� ����Ŭ���� ���� 
                {
                    EnterItem();
                }
            }
        }
        if (EnterMenuItem) //������ �޴�â���� ����
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitItem();
            }
            if (ItemMenuCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ItemUpArrow();
                }
            }
            if (ItemMenuCount < InvenLength - 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ItemDownArrow();

                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ItemEnter();
            }
        }
    }
    public void MenuOpen() //�޴�â ����
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
    }
    public void MenuClose() //�޴�â �ݱ�
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

    public void LeftArrow() //ž�޴�â ���� �ѱ��
    {
        MenuCount--;
        UpdateMenuUI();
        Cursor.transform.position -= Move_TopMenu_Cursor;
    }
    public void RightArrow() //ž�޴�â �����ʳѱ��
    {
        MenuCount++;
        UpdateMenuUI();
        Cursor.transform.position += Move_TopMenu_Cursor;
    }
    public void EnterItem() // ž�޴�â���� ���������� �Ѿ��
    {
        Selected_Cursor.transform.position = Defalut_Selected_Cursor;
        ItemMenuCount = 0;
        EnterMenuItem = true;
        Selected_Menu.SetActive(true);
        UpdateMenuBottom();

    }
    public void ExitItem() // ž�޴�â���� ���ư���
    {
        ItemMenuCount = 0;
        EnterMenuItem = false;
        Selected_Menu.SetActive(false);
    }

    private void UpdateMenuUI() // �޴��� UI ������Ʈ 
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
    private void UpdateMenuBottom() //�ϴ� �޴� UI ������Ʈ
    {
        InvenLength = PlayerData.Instance.inven.Items.Count;
        ClearSlot();
        SetSlot();
    }
    public void ItemUpArrow() //������ �޴� ��ȭ��ǥ Ŭ����
    {
        Selected_Cursor.transform.position += Move_Selected_Cursor;
        ItemMenuCount--;
        UpdateMenuBottom();
    }
    public void ItemDownArrow() //������ �޴� �Ʒ�ȭ��ǥ Ŭ����
    {
        Selected_Cursor.transform.position -= Move_Selected_Cursor;
        ItemMenuCount++;
        UpdateMenuBottom();
    }
    public void ItemEnter() //������ �޴� ����Ŭ���� ������ ���
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
        Item_Header.text = string.Format("{0}", PlayerData.Instance.inven.Items[ItemMenuCount].ItemName);
        Item_Explain.text = string.Format("{0}", PlayerData.Instance.inven.Items[ItemMenuCount].Tooltip);
        Item_Amount.text = string.Format("������ : {0} / 99", PlayerData.Instance.inven.Items[ItemMenuCount].Quantity);
        ItemList_Txt[ItemMenuCount].color = Selected_Text_Color;
        ItemList_Amount[ItemMenuCount].color = Selected_Text_Color;
        ItemList_Image[ItemMenuCount].color = Selected_Image_Color;

    }
    private IEnumerator CantPlay() //������ �̿ܿ� �̱����Դϴ� �ߴ°�
    {
        CanNot_ob.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        CanNot_ob.SetActive(false);
    }
}
