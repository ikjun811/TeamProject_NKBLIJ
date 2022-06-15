using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class First_manager : MonoBehaviour
{
    First_start_DialogueManager theDM;
    InteractionEvent getdialog;

    private bool flag_firstdialogue;
    bool isNextScene;
    bool isLastScript;

    // Start is called before the first frame update
    void Start()
    {
        getdialog = FindObjectOfType<InteractionEvent>();

        theDM = FindObjectOfType<First_start_DialogueManager>();

        flag_firstdialogue = true; //���� ���� ��翡 �� �÷���

        isNextScene = false;
        isLastScript = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag_firstdialogue)
        {
            flag_firstdialogue = false;
            EndScriptStart(1, 3); //���� ���� ���
            //DialogueManager�� ��� ��� �κ��� Update�̹Ƿ� Start���� ȣ���ϸ� �����߻�, �÷��� �̿��Ͽ� 1ȸ�� ����
        }
        if (isLastScript)
        {
            if (theDM.isDialogue == false)
            {
                SceneManager.LoadScene("5F_StartRoom");
            }
        }
    }

    void EndScriptStart(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        isLastScript = true;
    }
}
