using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Panel : MonoBehaviour
{
    public Inventory inventory;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                if(click_obj.name == "Book")
                {
                    inventory.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
                    Destroy(hit.transform.gameObject);
                }
                else if (click_obj.name == "Lighter")
                {
                    // 대사 출력
                    Destroy(click_obj);
                    // 아이템 획득 UI 출력
                    // 인벤토리에 아이템 추가
                }
                else if (click_obj.name == "Gas")
                {
                    // 대사 출력
                    Destroy(click_obj);
                    // 아이템 획득 UI 출력
                    // 인벤토리에 아이템 추가
                }
                else if (click_obj.name == "DoorLock")
                {
                    Debug.Log(click_obj.name);
                    // 대사 출력 (1번만)
                    // UI 팝업 출력 (비밀번호 입력 창)
                }
                else if (click_obj.name == "Window")
                {
                    Debug.Log(click_obj.name);
                    // 대사 출력
                }
            }
        }
    }

}