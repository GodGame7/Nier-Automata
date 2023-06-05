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

    // ��ǲ�� Ȯ���ϱ����� �Ұ�
    private bool OpenMenu = false;
    private bool EnterMenuItem = false;

    //ž�޴� ����
    private Color Select_Color = new Color(65f / 255f, 61f / 255f, 53f / 255f);
    private Color Default_Color = new Color(167f / 255f, 160f / 255f, 134f / 255f);
    private Color Select_Text_Color = new Color(157f / 255f, 153f / 255f, 139 / 255f);
    private Color Default_Text_Color = new Color(87f / 255f, 82f / 255f, 65 / 255f);
    //�¿����� �޴���
    [Header("������Ʈ�� �־��ּ���")]
    [SerializeField] private GameObject MenuUI_ob;
    [SerializeField] private GameObject CanNot_ob;
    [SerializeField] private GameObject[] BottomMenu;
    [SerializeField] private Image[] TopImage;
    [SerializeField] private Text[] TopText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //�޴�â ����
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
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
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
    public void MenuOpen() //�޴�â ����
    {
        MenuCount = 0;
        Time.timeScale = 0;
        OpenMenu = true;
        MenuUI_ob.SetActive(true);
        UpdateMenuUI();
    }
    public void MenuClose() //�޴�â �ݱ�
    {
        MenuCount = 0;
        Time.timeScale = 1;
        OpenMenu = false;
        MenuUI_ob.SetActive(false);
    }
    public void LeftArrow() //ž�޴�â ���� �ѱ��
    {
        MenuCount--;
        UpdateMenuUI();
    }
    public void RightArrow() //ž�޴�â �����ʳѱ��
    {
        MenuCount++;
        UpdateMenuUI();
    }
    public void EnterItem() // ž�޴�â���� ���������� �Ѿ��
    {
        EnterMenuItem = true;

    }
    public void ExitItem() // ž�޴�â���� ���ư���
    {
        EnterMenuItem = false;
        
    }
   
    private void UpdateMenuUI() // �޴��� UI ������Ʈ 
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
    public void ItemUpArrow() //������ �޴� ��ȭ��ǥ Ŭ����
    {

    }
    public void ItemDownArrow() //������ �޴� �Ʒ�ȭ��ǥ Ŭ����
    {

    }
    public void ItemEnter() //������ �޴� ����Ŭ���� ������ ���
    {

    }
    private IEnumerator CantPlay() //������ �̿ܿ� �̱����Դϴ� �ߴ°�
    {
        CanNot_ob.SetActive(true);
        yield return new WaitForSeconds(2f);
        CanNot_ob.SetActive(false);
    }
}
