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

    bool isDialogue = false;
    bool isNext = false;
    bool isSkipActive = false;

    [Header("텍스트 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;
    int contextCount = 0;
    int End_text_num = 0;

    SpriteManager theSpriteManager;

    void Start()
    {
        um = GameObject.Find("UIManager").GetComponent<UIManager>();

        NowLocate.GetComponent<Text>().text = "현재 위치 : 시작의 방";

        theSpriteManager = FindObjectOfType<SpriteManager>();
    }

    void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetMouseButtonDown(0) || isSkipActive)
                {
                    isNext = false;
                    isSkipActive = false;

                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount< End_text_num)
                        {
                            StartCoroutine(TypeWriter());
                        }
                        else
                        {
                            Announce();

                            EndDialogue();
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
        SettingUI(false);
    }

    public void ShowDialogue(Dialogue[] p_dialogues, int start_num, int end_num)
    {
        isDialogue = true;

        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;

        lineCount = start_num -1;
        End_text_num = end_num -1;

        SettingUI(true);
        StartCoroutine(TypeWriter());
    }

    void ChangeSprite()
    {
        if(dialogues[lineCount].spriteName[contextCount] != "")
        {
            StartCoroutine(theSpriteManager.SpriteChangeCoroutine(this.gameObject.transform, dialogues[lineCount].spriteName[contextCount]));
        }
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);
        ChangeSprite();

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];

        t_ReplaceText = t_ReplaceText.Replace("'", ",");


        
        for(int i = 0; i< t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;

    }

    void Announce()
    {
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];

        if (t_ReplaceText.Equals(""))
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
            if(dialogues[lineCount].name == "")
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
        lineCount = End_text_num - 1 ;
        //contextCount = 0;


        //string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
       // t_ReplaceText = t_ReplaceText.Replace("'", ",");

        isSkipActive = true;
        //isNext = true;
    }
}
