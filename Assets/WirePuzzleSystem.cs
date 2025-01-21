using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleSystem : MonoBehaviour


{
    public GameObject redWire;
    public GameObject blueWire;
    public GameObject greenWire;

    string[,] answer = {{"3","-8"},{"2","0"},{"-1/2","7/2"}};

    public InputField inputM;
    public InputField inputB;
    public GameObject puzzleFinish;
    public DialogueManager2 dialogueManager;
    public Dialogue wrongDialogue;
    int CorrectAnswer=0;
    public bool isPuzzleFinish=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CorrectAnswer==3){
            isPuzzleFinish=true;
            CorrectAnswer++;
        }
    }

    public void OnConfirmButton(){

        if(inputM.input=="3"&&inputB.input=="-8"&&!blueWire.activeSelf){
            blueWire.SetActive(true);
            CorrectAnswer++;
        }
        else if(inputM.input=="2"&&inputB.input=="0"){
            greenWire.SetActive(true);
            CorrectAnswer++;

        }
        else if(inputM.input=="-1/2"&&inputB.input=="7/2"&&!redWire.activeSelf){
            redWire.SetActive(true);
            CorrectAnswer++;
        }else{
            dialogueManager.StartDialogue(wrongDialogue);
        }

        Debug.Log(CorrectAnswer);
    }
}
