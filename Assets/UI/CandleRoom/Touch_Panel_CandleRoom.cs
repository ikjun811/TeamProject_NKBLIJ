using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private bool flag_doll, flag_candle, flag_Aphoto, flag_Cphoto, flag_CCTV;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.FindObjectOfType<ItemPanel>();
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");

        NowLocate.GetComponent<Text>().text = "현재 위치 : 추모의 방";
        clikedObj = null;

        flag_doll = false;
        flag_Cphoto = false;
        flag_CCTV = false;
        flag_candle = false;
        flag_Aphoto = false;
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
                    if (NowState.activeSelf == false) // 사용중x + 복도다녀오기 전 : 변수 추가
                    {
                        // 대사 출력 = 불길한 기운의 인형...
                        flag_doll = true;
                    }
                    // else if (NowState.active == false ) // 사용중 x + 복도 다녀온 후 : 아이템 획득
                    // inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    // Destroy(clikedObj);
                    // um.NewItemAddPanelOn("아이템 획득 : 부두 인형");
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
                        flag_candle = true;
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
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 어린 아이와 알 수 없는 인물의 사진이다~~~
                        flag_Cphoto = true;
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoAdult") // 제단 사진
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 영정 사진?
                        flag_Aphoto = true;
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CCTV") // CCTV 
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : CCTV....
                        flag_CCTV = true;
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorriderDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        if (flag_Aphoto && flag_candle && flag_CCTV && flag_Cphoto && flag_doll)
                        { // 조건 충족 -> 다음 방 넘어갈 수 있음
                            // 대사 출력
                            SceneManager.LoadScene("5F_Corridor");
                        }
                        else
                        {
                            // 대사 출력 : 문이 잠겨서 열리지 않는다.... 일다 다른 것들 먼저 조사하자.
                            Debug.Log("333");
                        }
                    }
                    else // 문에 아이템 사용 시.
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    // 대사 출력
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "StartRoomDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 돌아갈 이유가 없다...
                    }
                    else // 문에 아이템 사용 시.
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
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

