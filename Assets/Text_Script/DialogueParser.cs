using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); //csv을 ','단위로 분할

            Dialogue dialogue = new Dialogue();

            dialogue.name = row[1]; //캐릭터 이름

            List<string> contextList = new List<string>(); //대사 
            List<string> spriteList = new List<string>(); //캐릭터 이미지
            List<string> slideList = new List<string>(); //아이템 이미지슬라이드

            do
            {
                //분할한 내용을 각각 배열형태로 저장
                contextList.Add(row[2]);
                spriteList.Add(row[3]);
                slideList.Add(row[4]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            dialogue.spriteName = spriteList.ToArray();
            dialogue.slideName = slideList.ToArray();
            dialogueList.Add(dialogue);

     

        }
        return dialogueList.ToArray();
    }

}
