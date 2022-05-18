using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Touch_Panel : MonoBehaviour
{
    private UIManager um;
    private GameObject clikedObj;
    public Inventory inventory;
    public ItemPanelOnOff ipoo; // itemPanel 에 붙은 스크립트 가져옴
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;

    private bool flag_Lighter;
    private bool flag_Gas;
    public GameObject DoorLockPanel;
    private void Start()
    {
        NowLocate.GetComponent<Text>().text = "현재 위치 : 시작의 방";
        clikedObj = null;
        um = GameObject.Find("UIManager").GetComponent<UIManager>();
        flag_Lighter = false;
        flag_Gas = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !um.IsUIOn)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                clikedObj = hit.transform.gameObject;
                if (clikedObj.name == "라이터")
                {
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("아이템 획득 : 가스 없는 라이터");
                        flag_Lighter = true;
                        NowStateMsgCheck();
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "상자")
                {
                    // 대사 출력
                    if (NowState.activeSelf == false)
                    {
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("아이템 획득 : 상자");
                        flag_Gas = true;
                        NowStateMsgCheck();
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DoorLock")
                {
                    // bool temp = inventory.FindItem("Gas");  조합 기능 이후 완성 예정
                    string tempItemName = ipoo.getItem();
                    if (flag_Gas == true && flag_Lighter == true && tempItemName == "Lighter" && NowState.activeSelf == true) // 분해 구현 시, Box & Gas 구분 + 조합된 아이템 구분
                    {  // 조건 충족 시, 실행
                        DoorLockPanelOn();
                    }
                    else if (flag_Gas == true && flag_Lighter == true && tempItemName != "Lighter" && NowState.activeSelf == true) // 분해 구현 시, Box & Gas 구분 + 조합된 아이템 구분
                    {  // 조건은 다 만족시켰지만 도어락에 다른 아이템을 사용했을 때 : 발생 가능성은 없음
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    else // 조건 미충족
                    {
                        um.NewItemAddPanelOn("조건 불충분"); // UI 대신, 대사 처리 필요
                        // 대사 출력
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Book")
                {
                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    // 대사 출력
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Window")
                {
                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    // 대사 출력
                    NowStateMsgCheck();
                }
            }
        }
    }
        public void DoorLockPanelOn()
    {
        um.IsUIOn = true;
        DoorLockPanel.SetActive(true);
    }
    public void DoorLockPanelOff()
    {
        DoorLockPanel.SetActive(false);
        um.IsUIOn = false;
    }
    void NowStateMsgCheck()
    {
        if (NowState.activeSelf == true)
        {
            NowState.SetActive(false);
        }
    }
}
