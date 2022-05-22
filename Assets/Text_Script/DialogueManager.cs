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


    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;

    Dialogue[] dialogues;

    bool isDialogue = false;
    bool isNext = false;

    [Header("텍스트 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;
    int contextCount = 0;
    int End_text_num = 0;

    SpriteManager theSpriteManager;

    void Start()
    {
        theSpriteManager = FindObjectOfType<SpriteManager>();
    }

    void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isNext = false;
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
        End_text_num = end_num;

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
}
