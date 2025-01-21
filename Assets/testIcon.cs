using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testIcon : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject Icon;
    bool isChanged=false;
    public Dialogue[] dialogue;
    bool Started=false;
    public GameObject CutScene;
    public DialogueManager2 dialogueManager;
    // Start is called before the first frame update
    void Start()
    {   
        if(!CutScene){
         dialogueManager.StartDialogue(dialogue[0]);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
        Animator DialogueAnimator=DialogueBox.GetComponent<Animator>();
         Animator IconAnimator=Icon.GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            if(!isChanged){
            IconAnimator.SetInteger("id",1);
                // isChanged=true;


            }
            DialogueAnimator.SetInteger("id",10);
            isChanged=false;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(!isChanged){
                IconAnimator.SetInteger("id",3);
                // isChanged=true;


            }
            DialogueAnimator.SetInteger("id",10);
            isChanged=false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(!isChanged){
                IconAnimator.SetInteger("id",2);
                // isChanged=true;


            }
            DialogueAnimator.SetInteger("id",10);
            // isChanged=false;

        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DialogueAnimator.SetBool("isActive",true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            DialogueAnimator.SetBool("isActive",false);
        }


        if(!Started){
            dialogueManager.StartDialogue(dialogue[1]);
            Started=true;
        }

    }
    
}
