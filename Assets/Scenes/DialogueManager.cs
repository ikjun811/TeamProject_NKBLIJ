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

    int lineCount = 0;
    int contextCount = 0;

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
                    ++contextCount;
                    StartCoroutine(TypeWriter());
                }
            }
        }
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

    IEnumerator TypeWriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");

        txt_Dialogue.text = t_ReplaceText;

        isNext = true;
        yield return null;
    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }
}
