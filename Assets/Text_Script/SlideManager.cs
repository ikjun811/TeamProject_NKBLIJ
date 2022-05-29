using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    [SerializeField] Image img_SlideCG;
    [SerializeField] Animation anim;

    public static bool isFinished = false;
    public static bool isChanged = false;

    public IEnumerator AppearSlide(string p_SlideName)
    {
        Sprite t_Sprite = Resources.Load<Sprite>("Slide/" + p_SlideName);
        if(t_Sprite != null)
        {
            img_SlideCG.gameObject.SetActive(true);
            img_SlideCG.sprite = t_Sprite;
            anim.Play("Appear");
        }

        yield return new WaitForSeconds(0.5f);

        isFinished = true;
       
    }

    public IEnumerator DisappearSlide()
    {
        anim.Play("Appear");
        yield return new WaitForSeconds(0.5f);
        img_SlideCG.gameObject.SetActive(false);

        isFinished = true;

    }

    public IEnumerator ChangeSlide(string p_SlideName)
    {
        isFinished = false;
        StartCoroutine(DisappearSlide());
        yield return new WaitUntil(() => isFinished);

        isFinished = false;
        StartCoroutine(AppearSlide(p_SlideName));
        yield return new WaitUntil(() => isFinished);

        isChanged = true;
    }
}
