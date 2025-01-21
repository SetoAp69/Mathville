using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro.Examples;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.UIElements;


public enum GameState {Unfinished, Revealed, Finished,InDialogue,Puzzle}
public class PuzzleSystem : MonoBehaviour
{
    // Start is called before the first frame update


    public PuzzlePiece [] pieces=new PuzzlePiece[16];

    public GameObject[]spots=new GameObject[4];

    private int totalActivated=0;

    public GameObject grid;
    public GameObject ruler;
    public GameObject Fog;
    public GameObject PuzzleHud;
    public DialogueManager dialogueManager;
    public GameState state;
    public StageEndScreen EndScreen;
    public GameObject[] TriggerAreas=new GameObject[16];

    private PlayableDirector director ;
    public GameObject pointer;
    public GameObject Map;

    public InputField[]InputAnswer;
    // public Text[] inputCoordinate;
    public Dialogue dialogue0;
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;
    public Dialogue dialogue5;
    public Dialogue dialogue6;
    public Dialogue dialogue7;
    public Dialogue dialogue8;
    public Dialogue dialogue9;
   
    private Animator MapAnimator;
    private int totalCalled=0;
    private int totalCalled1=0;
    private int totalCalled2=0;
    private int totalCalled3=0;
    private int totalCalled4=0;
    private bool puzzleCalled=false;
    private bool dialogue6IsCalled=false;
    private bool dialogue5IsCalled=false;
    private bool isAnswerCorrect=false;
    private bool isAnswerSubmited=false;
    private bool dialogue7IsCalled=false;
    private bool dialogue8IsCalled=false;
    private bool isEndScreenCalled=false;
    // private int[]coordinateAnswer={-4,2,-1,6,-5,7,-6,8};
    private int[]coordinateAnswer={1,1,1,1,1,1,1,1};



    bool isDirectorCalled=false;


    public void CheckAnswer(){

        if(ValidateInput(InputAnswer)){
            isAnswerCorrect=true;
            for(int i=0;i<8;i++){
                if (int.Parse(InputAnswer[i].input )!= coordinateAnswer[i])
                {  
                    isAnswerCorrect=false;
                }

            
            }
            isAnswerSubmited=true;
        }else{
            Debug.Log("input invalid");
        }

        // if(isAnswerCorrect){
        //     AnswerCorrect();
        // }else{
        //     AnswerWrong();
        // }
        
    }

    void AnswerCorrect(){
        Debug.Log("Answer is Corect");
        if(true){
            Debug.Log("Playing the Dialog");

            MapAnimator.SetBool("isBig",true);
            PuzzleHud.SetActive(false);
            TriggerDialogue(dialogue8);
            
            if(dialogueManager.state==DialogueState.End){
                EndScreen.Win();
            }

        }
    }
    void AnswerWrong(){
        Debug.Log("Answer is Corect wrong");

        if(true){
            Debug.Log("Playing the Dialog");

            PuzzleHud.SetActive(false);
            TriggerDialogue(dialogue9);


        }

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


    void Start()
    {
        MapAnimator=Map.GetComponent<Animator>();
        MapAnimator.SetBool("isBig",true);
        director=GetComponent<PlayableDirector>();
        // state=GameState.InDialogue;
        if(dialogue0.sentences[1]!=null){
            Debug.Log("Dialog is assigned");
        }
        TriggerDialogue(dialogue0);
        

    }
    // IEnumerator StartConvo(){
    //     if(state==GameState.InDialogue){
    //         TriggerDialogue(this.dialogue);
    //     }
    //     yield return null;
    // }

    void StartPuzzle(){
        if(state==GameState.Puzzle){

        }
    }
     void TriggerDialogue(Dialogue dialogue){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Debug.Log("Dialog Started");
    }

    // Update is called once per frame
    void Update()
    {       totalActivated=0;
            foreach(PuzzlePiece piece in pieces){
                if(piece.isActive){
                    totalActivated++;
                }
                
            }

            if(totalActivated==16){
                            Debug.Log("Map fully unlocked");
                            grid.SetActive(true);
                            ruler.SetActive(true);
                            // PuzzleHud.SetActive(true);
                            Fog.SetActive(false);
                            if(totalCalled4==1){
                                // dialogueManager.state=DialogueState.Start;
                                TriggerDialogue(dialogue5);
                                dialogue5IsCalled=true;

                            }
                            if(dialogue5IsCalled){
                                if(!dialogue6IsCalled&&dialogueManager.state==DialogueState.End){
                                    dialogue6IsCalled=true;
                                    MapAnimator.SetBool("isBig",false);
                                    Debug.Log("Map not big");
                                    TriggerDialogue(dialogue6);
                                    if(dialogueManager.state==DialogueState.End){
                                        isDirectorCalled=true;
                                        

                                    }
                                

                                }
                            }
                            if(dialogue6IsCalled){
                                if(dialogueManager.state==DialogueState.End&&!dialogue7IsCalled){
                                    
                                    TriggerDialogue(dialogue7);
                                    dialogue7IsCalled=true;
                                }
                            }
                            if(dialogue7IsCalled&dialogueManager.state==DialogueState.End&&!puzzleCalled){
                                 puzzleCalled=true;

                                PuzzleHud.SetActive(true);
                            }

                            
                            totalCalled4++;
                
            }

            if(state==GameState.Puzzle){
                if(!puzzleCalled){
                    PuzzleHud.SetActive(true);
                    puzzleCalled=true;
                }

            }
            

            if(pieces[6].isActive){
                totalCalled++;
                spots[1].SetActive(true);
                pointer.SetActive(false);
                // foreach(GameObject triggerArea in TriggerAreas){
                //     triggerArea.SetActive(true);
                // }
                if(totalCalled==1){
                    TriggerDialogue(dialogue1);

                }

            

        }  
        if(dialogueManager.state==DialogueState.Start){
            foreach(GameObject triggerArea in TriggerAreas){
                triggerArea.GetComponent<BoxCollider2D>().enabled=false;
            }
            TriggerAreas[6].GetComponent<BoxCollider2D>().enabled=true;
        }
        if(dialogueManager.state==DialogueState.End){
            foreach(GameObject triggerArea in TriggerAreas){
                triggerArea.GetComponent<BoxCollider2D>().enabled=true;
            } 
        }


        if(isAnswerSubmited){
            if(isAnswerCorrect&&dialogueManager.state==DialogueState.End){
                AnswerCorrect();
                dialogue8IsCalled=true;

            }else if(!isAnswerCorrect&&dialogueManager.state==DialogueState.End){
                AnswerWrong();
            }
            isAnswerSubmited=false;
        }
        if(!isEndScreenCalled&&dialogue8IsCalled&&dialogueManager.state==DialogueState.End){
            isEndScreenCalled=true;
            Map.SetActive(false);
            EndScreen.Win();
        }

            
    }
    void FixedUpdate() {
        
        if(dialogueManager.state==DialogueState.End&!pieces[6].isActive){
            pointer.SetActive(true);
        }
        
        if(pieces[1].isActive){
            spots[3].SetActive(true);
            totalCalled1++;
            if(totalCalled1==1){
                TriggerDialogue(dialogue4);
            }

        } 
        if(pieces[5].isActive){
            spots[2].SetActive(true);
             totalCalled2++;
            if(totalCalled2==1){
                TriggerDialogue(dialogue2);
            }
        }  
        if(pieces[6].isActive){
            spots[1].SetActive(true);
            pointer.SetActive(false);
            
            // foreach(GameObject triggerArea in TriggerAreas){
            //     triggerArea.SetActive(true);
            // }
            // TriggerDialogue(dialogue1);

            

        }  

        if(pieces[9].isActive){
            spots[0].SetActive(true);
             totalCalled3++;
            if(totalCalled3==1){
                TriggerDialogue(dialogue3);
            }
        }     

        

        
    }



}
