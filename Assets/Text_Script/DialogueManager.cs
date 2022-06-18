using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;
    [SerializeField] GameObject Char_sprite;
    [SerializeField] GameObject BG_light;

    private UIManager um;
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;

    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;

    Dialogue[] dialogues;

    public bool isDialogue = false;
    bool isNext = false;
    bool isSkipActive = false;



    [Header("텍스트 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;
    int contextCount = 0;
    int End_text_num = 0;

    SpriteManager theSpriteManager;
    SlideManager theSlideManager;

    void Start()
    {
        um = GameObject.Find("UIManager").GetComponent<UIManager>();


        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");

        //NowLocate.GetComponent<Text>().text = "현재 위치 : 시작의 방";

        theSpriteManager = FindObjectOfType<SpriteManager>();
        theSlideManager = FindObjectOfType<SlideManager>();


    }

    void Update()
    {
        if (isDialogue) //대사출력 진행중
        {
            if (isNext) // 다음대사 진행 확인
            {
                if (Input.GetMouseButtonDown(0) || isSkipActive) //클릭 또는 스킵
                {
                    isNext = false;
                    isSkipActive = false;

                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length) //문단 내 줄 대사
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount< End_text_num) //줄이 모두 끝나면 다음 문단으로 넘어감
                        {
                            StartCoroutine(TypeWriter());
                        }
                        else
                        {
                            Announce(); //시스템 알림창
                            EndDialogue(); //대사 출력 종료
                        }
                    }
                }
            }
        }
    }

    void EndDialogue()
    {
        

        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isSkipActive = false;
        isNext = false;
        um.IsUIOn = false;
        SettingUI(false);
    }

    public void ShowDialogue(Dialogue[] p_dialogues, int start_num, int end_num)
    {
        isDialogue = true;

        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;

        lineCount = start_num -1; //대사 시작 번호
        End_text_num = end_num -1; //대사 종료 번호

        SettingUI(true);
        um.IsUIOn = true;
        StartCoroutine(TypeWriter());
    }

    void ChangeSprite() 
    {
        //캐릭터 이미지 변경
        if(dialogues[lineCount].spriteName[contextCount] != "")
        {
            StartCoroutine(theSpriteManager.SpriteChangeCoroutine(this.gameObject.transform, dialogues[lineCount].spriteName[contextCount]));
        }
    }

    void SlideOnOff()
    {
        //아이템 이미지 슬라이드 효과 변경
        if (dialogues[lineCount].slideName[contextCount] == "" || isSkipActive)
        {
            return;
        }
        else if (dialogues[lineCount].slideName[contextCount] == "0")
        {
            StartCoroutine(theSlideManager.DisappearSlide());
        }
        else if (dialogues[lineCount].slideName[contextCount] != "")
        {
            StartCoroutine(theSlideManager.AppearSlide(dialogues[lineCount].slideName[contextCount]));
        }
    }

    IEnumerator TypeWriter()
    {
        //각 대사 타이핑 효과
        SettingUI(true);
        ChangeSprite();
        SlideOnOff();

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];

        t_ReplaceText = t_ReplaceText.Replace("'", ","); //csv파일은 ','단위로 쪼개져 작성시 ','대신 사용한 '''를 ','로 변경해주는 과정
        t_ReplaceText = t_ReplaceText.Replace(" ", "\u00A0"); //줄바꿈 변경

        txt_Dialogue.text = ""; //이전에 들어있던 내용 초기화


        for (int i = 0; i< t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
            //한글자씩 딜레이타임을 가지고 넣어 타이핑 효과
        }

        isNext = true;
        //문장이 전부 출력되었다면 다음 대사로 진행 준비되었음이 확인
    }

    void Announce()
    {
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];

        if (t_ReplaceText.Equals("")) //대사의 마지막 줄이 공백일경우 알림창 띄우지 않음
        {
            return;
        }
        else
        {
            t_ReplaceText = t_ReplaceText.Replace("'", ",");

            um.NewItemAddPanelOn(t_ReplaceText);
            NowStateMsgCheck();
        }
       
    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        BG_light.SetActive(p_flag);

        if (p_flag)
        {
            if(dialogues[lineCount].name == "") //캐릭터가 대사중일때만 이름, 캐릭터 표시
            {
                go_DialogueNameBar.SetActive(false);
                Char_sprite.SetActive(false);
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
                Char_sprite.SetActive(true);
            }
        }
        else
        {
            go_DialogueNameBar.SetActive(false);
            Char_sprite.SetActive(false);
        }
    }

    void NowStateMsgCheck()
    {
        if (NowState.activeSelf == true)
        {
            NowState.SetActive(false);
        }
    }

    public void OnClick()
    {
        //스킵 기능
        //버튼 입력시 진행중인 대사를 마지막 대사 번호로 변경
        //이후 대사 진행

        StartCoroutine(theSlideManager.DisappearSlide());
        lineCount = End_text_num - 1 ;
        //contextCount = 0;


        //string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        // t_ReplaceText = t_ReplaceText.Replace("'", ",");

        isSkipActive = true;
        //isNext = true;
    }
}
