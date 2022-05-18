using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;


    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;

    Dialogue[] dialogues;

    bool isDialogue = false;
    bool isNext = false;

    [Header("�ؽ�Ʈ ������")]
    [SerializeField] float textDelay;

    int lineCount = 0;
    int contextCount = 0;

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
                if (Input.GetKeyDown(KeyCode.Space))
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
                        if(++lineCount< dialogues.Length)
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

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;

        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;

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

        if (p_flag)
        {
            if(dialogues[lineCount].name == "")
            {
                go_DialogueNameBar.SetActive(false);
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }
        }
        else
        {
            go_DialogueNameBar.SetActive(false);
        }
    }
}
