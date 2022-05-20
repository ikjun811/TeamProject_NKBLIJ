using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_Panel_CandleRoom : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public ItemPanel ip; // itemPanel 에 붙은 스크립트 가져옴
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;
    public GameObject Canvas;
    private GameObject clikedObj;

    public GameObject DoorLockPanel;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.FindObjectOfType<ItemPanel>();
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");

        NowLocate.GetComponent<Text>().text = "현재 위치 : 추모의 방";
        clikedObj = null;

        //um = GameObject.Find("UIManager").GetComponent<UIManager>();
        //inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

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
                if (clikedObj.name == "voodooDoll")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false) // 조건 추가 : 복도의 지문 인식기 다녀오면 습득
                    {
                        // 대사 출력 = 아무래도 이 인형이 단서다. 인형에 지문이 있다...
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("아이템 획득 : 부두 인형");
                        NowStateMsgCheck();
                    }
                    // else if (NowState.active == false ) 복도 다녀오기 전 -> 대사 출력 = 불길한 느낌의 인형이다. 집어들기조차 싫다...
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Candle1" || clikedObj.name == "Candle2") // 촛불 눌렀을 때
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 = 촛불이네요... 방금 전에 켠듯한 -> 범인은 가까이 있었다
                        // 단서 아이템 추가 -> 추후 구현 예정?
                        NowStateMsgCheck();
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoChild") // 여자 아이 사진
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoAdult")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    // 대사 출력
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CCTV")
                {
                    Debug.Log(clikedObj.name);

                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    // 대사 출력
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorriderDoor")
                {
                    Debug.Log(clikedObj.name);

                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    // 대사 출력
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "StartRoomDoor")
                {
                    Debug.Log(clikedObj.name);

                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    else
                    {
                        // 대사 출력
                    }
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

