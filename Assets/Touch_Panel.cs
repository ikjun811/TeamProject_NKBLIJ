using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Panel : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.position.x >= 30 && eventData.position.x <= 50 && 
            eventData.position.y >= 150 && eventData.position.y <= 170)
        {
            Debug.Log("Box"); // 아이템 습득 - 박스 -> 이후 해체해서 기름 습득
        }
        else if (eventData.position.x >= 80 && eventData.position.x <= 115 &&
            eventData.position.y >= 230 && eventData.position.y <= 260)
        {
            Debug.Log("Book"); // 대화 스크립트 - 성경구절 Hint
        }
        else if (eventData.position.x >= 170 && eventData.position.x <= 180 &&
            eventData.position.y >= 252 && eventData.position.y <= 262)
        {
            Debug.Log("Lighter"); // 아이템 습득 - 라이터
        }
        else if (eventData.position.x >= 140 && eventData.position.x <= 150 &&
            eventData.position.y >= 315 && eventData.position.y <= 330)
        {
            Debug.Log("DoorLock"); // 대화 스크립트 - 도어락 + 비밀번호 입력창
        }
        else Debug.Log(eventData.position);
    }

}