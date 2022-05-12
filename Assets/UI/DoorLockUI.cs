using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLockUI : MonoBehaviour
{

    public Touch_Panel tp;
    int temp1;
    int temp2; 
    int temp3;
    int temp4;
    int input_count;
    bool suc_flag;
    public bool flag;
    void Start()
    {
        input_count = 4;
        temp1 = 99;
        temp2 = 99;
        temp3 = 99;
        temp4 = 99;
        suc_flag = true;
        flag = false;
    }

    private void OnEnable()
    {
        input_count = 4;
        temp1 = 99;
        temp2 = 99;
        temp3 = 99;
        temp4 = 99;
        suc_flag = true;
        flag = false;
        input_count = 4;
    }

    void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            // 비밀번호 = 0115
            if (Input.mousePosition.x >= 80 && Input.mousePosition.x <= 115 && Input.mousePosition.y >= 340 && Input.mousePosition.y <= 370) // 1
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
            else if (Input.mousePosition.x >= 120 && Input.mousePosition.x <= 155 && Input.mousePosition.y >= 340 && Input.mousePosition.y <= 370) // 2
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 160 && Input.mousePosition.x <= 195 && Input.mousePosition.y >= 340 && Input.mousePosition.y <= 370) // 3
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 80 && Input.mousePosition.x <= 115 && Input.mousePosition.y >= 300 && Input.mousePosition.y <= 330) // 4
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 120 && Input.mousePosition.x <= 155 && Input.mousePosition.y >= 300 && Input.mousePosition.y <= 330) // 5
            {
                input_count -= 1;
                if(temp1 == 0 && temp2 == 1 & temp3 == 1 && suc_flag == true)
                {
                    temp4 = 5;
                }
            }
            else if (Input.mousePosition.x >= 160 && Input.mousePosition.x <= 195 && Input.mousePosition.y >= 300 && Input.mousePosition.y <= 330) // 6 
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 80 && Input.mousePosition.x <= 115 && Input.mousePosition.y >= 260 && Input.mousePosition.y <= 290) // 7 
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 120 && Input.mousePosition.x <= 155 && Input.mousePosition.y >= 260 && Input.mousePosition.y <= 290) // 8 
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 160 && Input.mousePosition.x <= 195 && Input.mousePosition.y >= 260 && Input.mousePosition.y <= 290) // 9 
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 120 && Input.mousePosition.x <= 155 && Input.mousePosition.y >= 220 && Input.mousePosition.y <= 250) // 0
            {
                input_count -= 1;
                if(temp1==99 && temp2 == 99 && temp3 == 99 && suc_flag == true)
                {
                    temp1 = 0;
                }
                else suc_flag = false;

            }
            else if (Input.mousePosition.x >= 80 && Input.mousePosition.x <= 115 && Input.mousePosition.y >= 220 && Input.mousePosition.y <= 250)
            {
                input_count -= 1; suc_flag = false;
            }
            else if (Input.mousePosition.x >= 160 && Input.mousePosition.x <= 195 && Input.mousePosition.y >= 220 && Input.mousePosition.y <= 250)
            {
                input_count -= 1; suc_flag = false;
            }
            if (input_count < 1)
            {
                if(temp1 == 0 && temp2 == 1 && temp3 == 1 && temp4 == 5)
                {
                    flag = true;
                    tp.DoorLockPanelOff();
                    // 대사 출력
                    SceneManager.LoadScene("5F_CandleRoom");
                }
                else
                {
                    tp.DoorLockPanelOff();
                }
            }
        }
    }
}


