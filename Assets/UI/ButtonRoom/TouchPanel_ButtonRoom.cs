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

    public GameObject redButtonCase;
    public GameObject blueButtonCase;
    public Sprite redButtonSprite;
    public Sprite blueButtonSprite;
    private bool flag_redBtn;
    private bool flag_blueBtn;
    
    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        itempanel = ip.GetComponent<ItemPanel>();
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");
        NowLocate.GetComponent<Text>().text = "현재 위치 : 버튼의 방";
        clikedObj = null;

        redButtonCase.GetComponent<BoxCollider2D>().enabled = false;
        blueButtonCase.GetComponent<BoxCollider2D>().enabled = false;
        flag_redBtn = false;
        flag_blueBtn = false;
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
                if (clikedObj.name == "KeyRoomDoor") // 버튼 플래그 2개 추가
                {
                    if (NowState.activeSelf == false && (!flag_blueBtn || !flag_redBtn))
                    {
                        Debug.Log("다음방으로 가는 문이다. 손잡이도 없고... 트릭이 있는 것 같다.");
                    }
                    else if (NowState.activeSelf == false && flag_redBtn && flag_blueBtn)
                    {
                        Debug.Log("버튼을 동시에 누르고 문에 힘을 가하자 문이 열렸다...");
                        SceneManager.LoadScene("5F_KeyRoom");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorridorDoor") // 버튼룸 문 조사
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("대사 출력 : 돌아갈 이유가 없다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedButton" || clikedObj.name == "BlueButton") // 버튼 습득
                {
                    if (NowState.activeSelf == false) // 템 사용 중 아닐 때.
                    {   // 대사는 필요 없을 듯
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("아이템 획득 : " + clikedObj.name);
                    }
                    else
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "WoodRed" || clikedObj.name == "WoodBlue") // 나무판자
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("대사 출력 : 튼튼해보이는 나무판자이다. 안에 장치같은게 숨어있다... 부숴야 할 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "Hammer")
                        {
                            // 대사 출력 : 망치를 휘두르자 나무 판자가 부서졌다~~~
                            Destroy(clikedObj);
                            if (clikedObj.name == "WoodRed")
                            {
                                redButtonCase.GetComponent<BoxCollider2D>().enabled = true;
                            }
                            else if (clikedObj.name == "WoodBlue")
                            {
                                blueButtonCase.GetComponent<BoxCollider2D>().enabled = true;
                            }
                        }
                        else
                        {
                            um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonCaseRed" || clikedObj.name == "ButtonCaseBlue") // 버튼 슬롯
                {
                    if (NowState.activeSelf == false)
                    {
                        if (clikedObj.name == "ButtonCaseRed" && flag_redBtn == true)
                        {
                            Debug.Log("대사 출력 : 장치에 빨간 버튼이 꽂혀 있다.");
                        }
                        else if(clikedObj.name == "ButtonCaseBlue" && flag_blueBtn == true)
                        {
                            Debug.Log("대사 출력 : 장치에 파란 버튼이 꽂혀 있다.");
                        }
                        else
                        {
                            Debug.Log("대사 출력 : 판자를 부수니 안에 기계 장치가 모습을 드러냈다... 무언가를 꽂아야 할 것 같다...");
                        }
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "RedButton" && clikedObj.name == "ButtonCaseRed")
                        {
                            Debug.Log("대사 출력 : 찰칵 소리를 내며 빨간 버튼이 맞물려 고정됐다.");
                            inventory.RemoveItem("RedButton");
                            redButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            redButtonCase.GetComponent<SpriteRenderer>().sprite = redButtonSprite;
                            flag_redBtn = true;
                        }
                        else if (tempItemName == "BlueButton" && clikedObj.name == "ButtonCaseBlue")
                        {
                            Debug.Log("대사 출력 : 찰칵 소리를 내며 파란 버튼이 맞물려 고정됐다.");
                            inventory.RemoveItem("BlueButton");
                            blueButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            blueButtonCase.GetComponent<SpriteRenderer>().sprite = blueButtonSprite;
                            flag_blueBtn = true;
                        }
                        else
                        {
                            um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedCircle" || clikedObj.name == "BlueCircle") // 색깔 원
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("대사 출력 : 슬롯 위에 색이 칠해진 원이 그려져 있다. -> 버튼 색깔 힌트.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hat") // 안전모
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("대사 출력 : 먼지를 듬뿍 뒤집어 쓴 낡아빠진 안전모이다. 별 도움은 안될 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Trash") // 쓰레기더미
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("대사 출력 : 각종 건설자재들이 쌓여 있다. 죄다 낡고 녹슬어 함부로 만지면 안될 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Newspaper") // 신문지
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("대사 출력 : 꽤 오래 전 날짜의 신문이 놓여져 있다. -> 건설사 노조 관련 사건");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DrumCement") // 드럼통+시멘트포대
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("대사 출력 : 드럼통과 시멘트 포대가 놓여 있다. ... 큰 쓸모는 없는 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
            }
        }
    }
    void NowStateMsgCheck()
    {
        if (NowState.activeSelf == true)
        {
            NowState.SetActive(false);
        }
    }
}

