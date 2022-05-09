using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Panel : MonoBehaviour
{
    UIManager um;

    private GameObject clikedObj;
    public Inventory inventory;
    private void Start()
    {
        clikedObj = null;
        um = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !um.IsUIOn)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                clikedObj = hit.transform.gameObject;
                Debug.Log(clikedObj.name);
                if (clikedObj.name == "Lighter")
                {
                    inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    Destroy(clikedObj);
                }
                else if (clikedObj.name == "Gas")
                {
                    inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    Destroy(clikedObj);
                }
                else if (clikedObj.name == "DoorLock")
                {
                    // UI 팝업 출력 + 입력 받기 + 터치 제어
                }
                else if (clikedObj.name == "Book")
                {
                    // 대사 출력
                }
                else if (clikedObj.name == "Window")
                {
                    // 대사 출력
                }
            }

        }
    }

}
