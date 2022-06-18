using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    void Awake()
    {
        //��ü ��� ������ �ҷ��� ��Ƶδ� ����
        dialogue.dialogues = DatabaseManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
    }

    public Dialogue[] GetDialogue()
    {
        return dialogue.dialogues;
    }
}
