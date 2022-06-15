using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLockUI : MonoBehaviour
{
    private GameObject clikedObj;
    public Touch_Panel tp;
    public Inventory inventory;


    DialogueManager theDM;

    InteractionEvent getdialog;


    int temp1;
    int temp2; 
    int temp3;
    int temp4;
    int input_count;
    bool suc_flag;
    bool isNextScene;
    bool isLastScript;

    void Start()
    {
        input_count = 4;
        temp1 = 99;
        temp2 = 99;
        temp3 = 99;
        temp4 = 99;
        suc_flag = true;
        isNextScene = false;
        isLastScript = false;

        theDM = FindObjectOfType<DialogueManager>();

        getdialog = FindObjectOfType<InteractionEvent>();
    }

    private void OnEnable()
    {
        input_count = 4;
        temp1 = 99;
        temp2 = 99;
        temp3 = 99;
        temp4 = 99;
        suc_flag = true;
        input_count = 4;
    }

    void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                clikedObj = hit.transform.gameObject;

                // 비밀번호 = 0115
                if (clikedObj.name == "btn(1)") // 1
                {
                    input_count -= 1;
                    if (suc_flag == true && temp1 == 0 & temp2 == 99 & temp3 == 99)
                    {
                        temp2 = 1;
                    }
                    else if (suc_flag == true && temp1 == 0 && temp2 == 1 && temp3 == 99)
                    {
                        temp3 = 1;
                    }
                    else suc_flag = false;
                }
                else if (clikedObj.name == "btn(2)") // 2
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btn(3)") // 3
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btn(4)") // 4
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btn(5)") // 5
                {
                    input_count -= 1;
                    if (temp1 == 0 && temp2 == 1 & temp3 == 1 && suc_flag == true)
                    {
                        temp4 = 5;
                    }
                }
                else if (clikedObj.name == "btn(6)") // 6 
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btn(7)") // 7 
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btn(8)") // 8 
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btn(9)") // 9 
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btn(0)") // 0
                {
                    input_count -= 1;
                    if (temp1 == 99 && temp2 == 99 && temp3 == 99 && suc_flag == true)
                    {
                        temp1 = 0;
                    }
                    else suc_flag = false;

                }
                else if (clikedObj.name == "btnstar")
                {
                    input_count -= 1; suc_flag = false;
                }
                else if (clikedObj.name == "btnsharp")
                {
                    input_count -= 1; suc_flag = false;
                }
            }
            if (input_count < 1)
            {
                if(temp1 == 0 && temp2 == 1 && temp3 == 1 && temp4 == 5)
                {
                    //inventory.RemoveItem("Lighter_F"); // 나중에 변경
                    //EndScriptStart(39, 45);
                    tp.DoorLockPanelOff();
                    // 대사 출력
                    SceneManager.LoadScene("5F_CandleRoom");
                    //StartCoroutine(WaitEndScript());


                }
                else
                {
                    tp.DoorLockPanelOff();
                }
            }
        }
        if (isLastScript)
        {
            if (theDM.isDialogue == false)
            {
                SceneManager.LoadScene("5F_CandleRoom");
            }
        }
    }

    void ScriptStart(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

    }

    void EndScriptStart(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        isLastScript = true;
        //StartCoroutine(WaitEndScript());

        //SceneManager.LoadScene("5F_CandleRoom");
        //yield return new WaitWhile(() => theDM.isDialogue);
        //yield return null;
    }



    IEnumerator WaitEndScript()
    {
        yield return new WaitWhile(() => theDM.isDialogue);
        isNextScene = true;

    }

}


