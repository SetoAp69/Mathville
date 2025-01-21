using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Stage_1_4_dialogue_manager : MonoBehaviour

{
    // Start is called before the first frame update
    public Dialogue[]dialogue;
    public DialogueManager dialogueManager;
    public GameObject PointerGO;
    bool pointerActive=false;
    public Animator battleHudAnimator;
    public GameObject battleSystem;

    void Start()
    
    {
        battleHudAnimator.SetBool("isActive",true);    
    }

    // Update is called once per frame
    void Update()

    {
        if(!dialogue[0].isFinish&&dialogueManager.state==DialogueState.End){
            dialogueManager.StartDialogue(dialogue[0]);
            dialogue[0].isFinish=true;

        }if(dialogue[0].isFinish&&dialogueManager.state==DialogueState.End&&!pointerActive){
            PointerGO.SetActive(true);
            pointerActive=true;
        }
        if(dialogue[0].isFinish&&dialogueManager.state==DialogueState.End&&!pointerActive&&PointerGO.activeSelf){
            battleSystem.SetActive(true);
            Debug.Log("Launching Battle System");
        }

        
    }

    public void OnSwitchButtonClick (){
        if(PointerGO.activeSelf){
            PointerGO.SetActive(false);
            battleSystem.SetActive(true);
            Debug.Log("Launching Battle System from Button");
        }
    }
}
