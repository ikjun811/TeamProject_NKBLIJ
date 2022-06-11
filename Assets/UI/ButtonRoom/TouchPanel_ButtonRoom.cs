using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchPanel_ButtonRoom : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public GameObject ip; // itemPanel 에 붙은 스크립트 가져옴
    public ItemPanel itempanel;
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;
    public GameObject Canvas;
    private GameObject clikedObj;

    public GameObject DoorLockPanel;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");
        NowLocate.GetComponent<Text>().text = "현재 위치 : 버튼의 방";
        clikedObj = null;

        itempanel = ip.GetComponent<ItemPanel>();
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
                if (clikedObj.name == "Light")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false) // 라이트 조사
                    {
                        // 대사 출력 = 낡은 건물 분위기와 어울리지 않는 새 것 같은 형광등이다...
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonRoomDoor") // 버튼룸 문 조사
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 자동문인 것 같다. 문 옆에 어떤 장치가 있는 것 같은데..
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "FingerPrintReader") // 지문리더기
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 지문 인식기이다. 우리 모두 손가락을 올려 보았지만 작동하지 않았다...
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "voodooDoll")
                        {
                            // 대사 출력 : 인형의 지문을 입력하자 문이 열림
                            SceneManager.LoadScene("5F_ButtonRoom");
                        }
                        else
                        {
                            um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hammer") // 망치 조사
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 망치네요. 챙겨두면 쓸모가 있을 것 같아요....
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("아이템 획득 : 망치");
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "LockedDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 아래층으로 내려갈 수 있는 것 같은 문이다. 지금은 잠겨있어 열 수 없다.
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        Debug.Log(tempItemName);
                        if (tempItemName == "5F_Key")
                        {
                            //  대사 출력 : 기분 나쁜 찢어지는 소리와 함께 문이 열렸다...
                            SceneManager.LoadScene("4F");
                        }
                        else
                        {
                            um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                        }
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

