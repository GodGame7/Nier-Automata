using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private Inventory inventory;
    private ItemEffect Item;

    private bool isActiveInven = false;
    private int ListNum = 0;

    //인풋 매니저
    [SerializeField] GameObject Inventory_UI;
    [SerializeField] GameObject Selected_Item;
    Vector3 defalutItemPosition;
    Vector3 MoveSelectedPoint;
    private void Awake() //처음 위치 (1번자리) 값 할당
    {
        defalutItemPosition = Selected_Item.transform.position;
        MoveSelectedPoint = new Vector3(0, -10f, 0); //-10f는 임시로 할당,추후 변경예정

    }
    private void OnEnable() // 활성화가 될때마다 그 할당된 위치로 갈수있도록
    {
        Selected_Item.transform.position = defalutItemPosition;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // 왼쪽아래 작은 인벤툴팁 열기
        {
            if (!isActiveInven //&& Player.instance.Inventory.count !=0
                               )
            {
                isActiveInven = true;
                Inventory_UI.SetActive(true);

            }
            else
            {
                isActiveInven = false;
                Inventory_UI.SetActive(false);
            }
        }

        //인벤토리 List에서 n번째 아이템인지 확인해서 사용 , 키누르면 위아래로 I 인벤열기 , K 위 L 아래 J 사용

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isActiveInven)
            {

                //Player.instance.inventory.RemoveItem(ListNum);
                Item.UseItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            if (ListNum >= 0  //&& ListNum <= Player.instance.inventory.Count  
                )
            {
                ListNum++;
                Selected_Item.transform.position += MoveSelectedPoint;
                //

            }


        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (ListNum > 0  //Player.instance.inventory.Count <= ListNum
                )
                ListNum--;
            Selected_Item.transform.position -= MoveSelectedPoint;
        }



    }
}
