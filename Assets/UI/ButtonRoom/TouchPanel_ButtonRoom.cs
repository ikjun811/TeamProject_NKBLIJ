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
    private int count_wood;

    DialogueManager theDM;
    InteractionEvent getdialog;

    static bool flag_firstdialogue;

    bool isLastScript;

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
        count_wood = 0;

        theDM = FindObjectOfType<DialogueManager>();

        getdialog = FindObjectOfType<InteractionEvent>();

        flag_firstdialogue = true; //최초 실행 대사에 쓸 플래그

        isLastScript = false;
    }
    void Update()
    {
        if (flag_firstdialogue)
        {
            flag_firstdialogue = false;
            ScriptStart(3, 19); //대사 : 제단방 진입
        }

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
                        ScriptStart(42, 46); // 문 첫 관찰 대사
                        Debug.Log("다음방으로 가는 문이다. 손잡이도 없고... 트릭이 있는 것 같다.");
                    }
                    else if (NowState.activeSelf == false && flag_redBtn && flag_blueBtn)
                    {
                        EndScriptStart(47, 63); //버튼을 동시에 누르며 문이 열림
                        Debug.Log("버튼을 동시에 누르고 문에 힘을 가하자 문이 열렸다...");
                        //SceneManager.LoadScene("5F_KeyRoom");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorridorDoor") // 복도 문 조사
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(20, 22); // 대사 출력 돌아갈 이유가 없다.
                        Debug.Log("대사 출력 : 돌아갈 이유가 없다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedButton" || clikedObj.name == "BlueButton") // 버튼 습득
                {
                    if (NowState.activeSelf == false) // 템 사용 중 아닐 때.
                    {   // 대사는 필요 없을 듯
                        if (clikedObj.name == "RedButton")
                        {
                            um.NewItemAddPanelOn("아이템 획득 : 빨간색 버튼");
                        }
                        else if (clikedObj.name == "BlueButton")
                        {
                            um.NewItemAddPanelOn("아이템 획득 : 파란색 버튼");
                        }
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("아이템 획득 : " + clikedObj.name);
                    }
                    else
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "WoodRed" || clikedObj.name == "WoodBlue") // 나무판자
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(23, 28); //나무판자 첫 조사
                        Debug.Log("대사 출력 : 튼튼해보이는 나무판자이다. 안에 장치같은게 숨어있다... 부숴야 할 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "Hammer")
                        {
                            if (count_wood == 0) // 첫째나무
                            {
                                ScriptStart(29, 35); //첫 나무판자 파괴
                                Debug.Log("망치 휘두름 -> 판자 부서지고 안에 장치 발견 -> 망치 휘청거림 -> 빨리 남은 하나도 마저 부수자");
                            }
                            else
                            {// 두번째나무
                                ScriptStart(36, 41); //두번째 나무판자 파괴
                                Debug.Log("남은 거 부숨 -> 망치 부서짐-> 버리자");
                            }
                            count_wood++;
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
                            ScriptStart(1, 1); // 사용불가
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonCaseRed" || clikedObj.name == "ButtonCaseBlue") // 버튼 슬롯
                {
                    if (NowState.activeSelf == false)
                    {
                        if(flag_blueBtn && flag_redBtn)
                        {
                            ScriptStart(73, 78); // 버튼 두개 모두 들어간 대사
                            Debug.Log("대사 출력 : 두개의 장치에 모두 버튼을 꽂아 넣었다. 문을 보자");
                        }
                        else if (clikedObj.name == "ButtonCaseRed" && flag_redBtn == true)
                        {
                            ScriptStart(69, 69); //빨간버튼 장착 확인
                            Debug.Log("대사 출력 : 장치에 빨간 버튼이 꽂혀 있다.");
                        }
                        else if(clikedObj.name == "ButtonCaseBlue" && flag_blueBtn == true)
                        {
                            ScriptStart(71, 71); //파란버튼 장착 확인
                            Debug.Log("대사 출력 : 장치에 파란 버튼이 꽂혀 있다.");
                        }
                        else
                        {
                            ScriptStart(64, 68); //장치에 무언가 끼울 수 있을것같다.
                            Debug.Log("대사 출력 : 판자를 부수니 안에 기계 장치가 모습을 드러냈다... 무언가를 꽂아야 할 것 같다...");
                        }
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "RedButton" && clikedObj.name == "ButtonCaseRed")
                        {
                            ScriptStart(79, 83); //레드버튼 장착
                            Debug.Log("대사 출력 : 찰칵 소리를 내며 빨간 버튼이 맞물려 고정됐다.");
                            inventory.RemoveItem("RedButton");
                            redButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            redButtonCase.GetComponent<SpriteRenderer>().sprite = redButtonSprite;
                            flag_redBtn = true;
                        }
                        else if (tempItemName == "BlueButton" && clikedObj.name == "ButtonCaseBlue")
                        {
                            ScriptStart(84, 88); //블루버튼 장착
                            Debug.Log("대사 출력 : 찰칵 소리를 내며 파란 버튼이 맞물려 고정됐다.");
                            inventory.RemoveItem("BlueButton");
                            blueButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            blueButtonCase.GetComponent<SpriteRenderer>().sprite = blueButtonSprite;
                            flag_blueBtn = true;
                        }
                        else
                        {
                            ScriptStart(1, 1); // 사용불가
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedCircle" || clikedObj.name == "BlueCircle") // 색깔 원
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(89, 95); //벽면 원 확인
                        Debug.Log("대사 출력 : 슬롯 위에 색이 칠해진 원이 그려져 있다. -> 버튼 색깔 힌트.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hat") // 안전모
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(96, 101); //안전모 확인 대사
                        Debug.Log("대사 출력 : 먼지를 듬뿍 뒤집어 쓴 낡아빠진 안전모이다. 별 도움은 안될 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Trash") // 쓰레기더미
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(102, 109); // 쓰레기더미 대사

                        Debug.Log("대사 출력 : 각종 건설자재들이 쌓여 있다. 죄다 낡고 녹슬어 함부로 만지면 안될 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Newspaper") // 신문지
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(108, 123); // 사용불가
                        Debug.Log("대사 출력 : 꽤 오래 전 날짜의 신문이 놓여져 있다. -> 건설사 노조 관련 사건");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DrumCement") // 드럼통+시멘트포대
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(124, 126); // 사용불가
                        Debug.Log("대사 출력 : 드럼통과 시멘트 포대가 놓여 있다. ... 큰 쓸모는 없는 것 같다.");
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
            }
        }
        if (isLastScript)
        {
            if (theDM.isDialogue == false)
            {
                SceneManager.LoadScene("5F_KeyRoom");
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

    void ScriptStart(int Start_num, int End_num) //대사 호출
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

    }

    void EndScriptStart(int Start_num, int End_num) // 씬 이동 이전 대사 호출
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        isLastScript = true;
    }
}

