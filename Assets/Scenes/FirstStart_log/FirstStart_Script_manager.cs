using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart_Script_manager : MonoBehaviour
{

    DialogueManager theDM;

    InteractionEvent getdialog;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();

        getdialog = FindObjectOfType<InteractionEvent>();

        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
