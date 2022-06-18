using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    void Awake()
    {
        //전체 대사 내용을 불러와 담아두는 과정
        dialogue.dialogues = DatabaseManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
    }

    public Dialogue[] GetDialogue()
    {
        return dialogue.dialogues;
    }
}
