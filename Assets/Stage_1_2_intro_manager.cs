using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Stage_1_2_intro_manager : MonoBehaviour
{
        public Dialogue[] dialogue;
        public Dialogue dialogueWrongAnswer;
        public DialogueManager2 dialogueManager;
        public GameObject DialogueBox;
        public GameObject Icon;
        public GameObject multiplyLayer;
        public GameObject ScanButton;
        public GameObject PuzzleHUD;
        public GameObject Spot1;
        public GameObject Spot2;
        public GameObject CutSceneEnd;
        public InputField YInput;
        public InputField XInput;
        

        public InputField[] InputAnswer;
        bool isAnswerCorrect=false;
        bool isAnswerSubmited=false;
        int [] coordinateAnswer1={-8,5};
        int [] coordinateAnswer2={8,5};
        
        int CorrectAnswer=0;

    // Start is called before the first frame update
    void Start()
    {

        dialogueManager.state=DialogueState.End;
        // for(int x=0;x<dialogue.Length;x++){
        //     if(x==0){
        //         if(!dialogue[x].isFinish){
        //             dialogueManager.StartDialogue(dialogue[x]);
        //             dialogue[x].isFinish=true;
        //         }
                
        //     }
        //     else{
        //             if(dialogue[x-1].isFinish&&!dialogue[x].isFinish){
        //                 dialogueManager.StartDialogue(dialogue[x]);
        //                 dialogue[x].isFinish=true;

        //             }
        //         }
        //         Debug.Log("X = "+x);

        //  }

        
        
    }

    // Update is called once per frame
    void Update()
    {
         Animator DialogueAnimator=DialogueBox.GetComponent<Animator>();
         Animator IconAnimator=Icon.GetComponent<Animator>();
        //  dialogueManager.StartDialogue(dialogue[0]);
        if(!dialogue[0].isFinish&&dialogueManager.state==DialogueState.End){
            dialogueManager.StartDialogue(dialogue[0]);
            dialogue[0].isFinish=true;
        }
        if(dialogue[0].isFinish&!dialogue[1].isFinish&&dialogueManager.state==DialogueState.End){
            dialogueManager.StartDialogue(dialogue[1]);
            dialogue[1].isFinish=true;
        }if(dialogue[1].isFinish&!dialogue[2].isFinish&&dialogueManager.state==DialogueState.End){
            dialogueManager.StartDialogue(dialogue[2]);
            dialogue[2].isFinish=true;
        }if(dialogue[2].isFinish&&dialogueManager.state==DialogueState.End){
            ScanButton.SetActive(true);
            ScanButton.GetComponent<Animator>().SetBool("isActive",true);
            multiplyLayer.SetActive(true);
            
        }if(CorrectAnswer==2){
            PuzzleClear();
            ScanButton.SetActive(false);
            multiplyLayer.SetActive(false);
        }
         
    }
    public void OnScanButton(){
        StartPuzzle();
        ScanButton.SetActive(false);
    }

    public void StartPuzzle(){
        PuzzleHUD.SetActive(true);
        Spot1.SetActive(true);
        Spot2.SetActive(true);
        multiplyLayer.SetActive(false);
        ScanButton.SetActive(false);
    }
    private bool ValidateInput(InputField[]input){
        bool valid=true;
        foreach(InputField i in input){
            foreach(char c in i.input){
                if(c!='-'){
                    valid=char.IsDigit(c);

                }
            }
        }
        Debug.Log("Is Input Valid? : "+valid);
        return valid;
        
    }
    void PuzzleClear(){
        PuzzleHUD.SetActive(false);
        CutSceneEnd.SetActive(true);
    }
    public void CheckAnswer(){

        if(ValidateInput(InputAnswer)){
            isAnswerCorrect=true;
            if(int.Parse(InputAnswer[0].input)==coordinateAnswer1[0]&&int.Parse(InputAnswer[1].input)==coordinateAnswer1[1]){
                
                Spot1.SetActive(false);
                CorrectAnswer++;

            }else if(int.Parse(InputAnswer[0].input)==coordinateAnswer2[0]&&int.Parse(InputAnswer[1].input)==coordinateAnswer2[1]){
                Spot2.SetActive(false);
                CorrectAnswer++;
            }else{
                
                dialogueManager.StartDialogue(dialogueWrongAnswer);
                Debug.Log("wrong answer");
            }
            
        }else{
            Debug.Log("input invalid");
        }

        // if(isAnswerCorrect){
        //     AnswerCorrect();
        // }else{
        //     AnswerWrong();
        // }
        
    }

}



