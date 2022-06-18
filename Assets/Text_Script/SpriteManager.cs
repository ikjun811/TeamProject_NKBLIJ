using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    bool CheckSameSprite(SpriteRenderer p_SpriteRenderer, Sprite p_Sprite)
    {
        //캐릭터 이미지 호출 확인
        if(p_SpriteRenderer.sprite == p_Sprite)
            return true;
        else
            return false;
    }

    public IEnumerator SpriteChangeCoroutine(Transform p_Target, string p_SpriteName)
    {
        //필요한 스프라이트를 찾아와 변경
        SpriteRenderer t_SpriteRenderer = p_Target.GetComponentInChildren<SpriteRenderer>();
        Sprite t_Sprite = Resources.Load("Char/" + p_SpriteName, typeof(Sprite)) as Sprite;

        if(!CheckSameSprite(t_SpriteRenderer, t_Sprite))
        {
            Color t_color = t_SpriteRenderer.color;
            t_color.a = 0;
            t_SpriteRenderer.color = t_color;

            t_SpriteRenderer.sprite = t_Sprite;

            while(t_color.a < 1)
            {
                t_color.a += fadeSpeed;
                t_SpriteRenderer.color = t_color;
                yield return null;
            }
        }
    }
}
