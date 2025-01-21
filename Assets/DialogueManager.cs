using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public enum DialogueState {Start, End}
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Text sideNameText;
    public Text sideDialogueText;

    public GameObject DialogueBox;
    private Animator DialogueBoxAnimator;

    public GameObject SideDialogueBox;
    private Animator SideDialogueBoxAnimator;
    private Queue<string> sentences;

    public Dialogue currentDialogue;
    public GameObject RepeatButton;
    bool hasSideDialog=false;

    public DialogueState state;
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    public void StartDialogue(Dialogue dialogue){
        state=DialogueState.Start;
        currentDialogue=dialogue;
        sentences = new Queue<string>();
        Debug.Log("Dialogue Manager is Started");
        Debug.Log("Starting dialogue with"+dialogue.name);
        sentences.Clear();
        
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        

        if(dialogue.isSideDialogue){
            SideDialogueBoxAnimator = SideDialogueBox.GetComponent<Animator>();

            SideDialogueBoxAnimator.SetBool("isActive",true);
            sideNameText.text=dialogue.name;
            hasSideDialog=true;

            DisplayNextSentenceSide();
        }else if(!dialogue.isSideDialogue){
            DialogueBoxAnimator = DialogueBox.GetComponent<Animator>();

            DialogueBoxAnimator.SetBool("isActive",true);
            nameText.text=dialogue.name;
            DisplayNextSentence();
            

        }
        
    }
    
    IEnumerator TypeSentence (string sentence){
        dialogueText.text="";
        
        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text+=letter;
            yield return null;
        }
    }
    IEnumerator TypeSentenceSide (string sentence){
        sideDialogueText.text="";
        
        foreach (char letter in sentence.ToCharArray()){
            sideDialogueText.text+=letter;
            yield return null;
        }
    }
    public void ReplayDialogue(){
        StartDialogue(currentDialogue);
    }

    public void DisplayNextSentence(){
        if(sentences.Count==0){
            EndDialogue();
            return;
        
        
        }
        

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }
    public void DisplayNextSentenceSide(){
        if(sentences.Count==0){
            EndDialogue();
            return;
        
        
        }
        

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentenceSide(sentence));

    }
    
    void EndDialogue(){
        Debug.Log("Dialogue Ended");
        state=DialogueState.End;
        DialogueBoxAnimator.SetBool("isActive",false);
        if(hasSideDialog){
            SideDialogueBoxAnimator.SetBool("isActive",false);

        }

        // if(currentDialogue.isRepeateable){
        //     // RepeatButton.SetActive(true);
        // }
    }
        
    
}
