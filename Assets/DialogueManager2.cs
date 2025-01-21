using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager2 : MonoBehaviour
{
    private Text nameText;
    private Text dialogueText;
    public Text playerNameText;
    public Text doctorNameText;
    public Text criminalNameText;
    public Text mayorNameText;
    public Text mayorBlindFoldedNameText;

    public Text playerDialogueText;
    public Text doctorDialogueText;
    public Text criminalDialogueText;
    public Text mayorDialogueText;
    public Text mayorBlindFoldedDialogueText;

    public Transform IconPosition;

    public GameObject drIcon;
    public GameObject playerIcon;
    public GameObject MayorIcon;
    public GameObject criminalIcon;

    public Text sideNameText;
    public Text sideDialogueText;

    private GameObject DialogueBox;
    public GameObject DialogueBoxDoctor;
    public GameObject DialogueBoxPlayer;
    public GameObject DialogueBoxCriminal;
    public GameObject DialogueBoxMayor;
    public GameObject DialogueBoxMayorBlindFolded;
    private Animator DialogueBoxAnimator;
    private GameObject DialogueBoxActive;

    public GameObject SideDialogueBox;
    private Animator SideDialogueBoxAnimator;
    private Queue<string> sentences;
    public GameObject IconGO;
    private Vector3 nonActivePosition;
    public Dialogue currentDialogue;
    public GameObject RepeatButton;
    public Sprite[] iconSprites;
    private UnityEngine.UI.Image icon;

    public DialogueState state;
    private Dictionary<int, GameObject> dialogueBoxMappings;
    private Dictionary<int, Text> nameTextMappings;
    private Dictionary<int, Text> dialogueTextMappings;


    void Start()
    {   
        nonActivePosition = playerIcon.transform.position;
        dialogueBoxMappings = new Dictionary<int, GameObject>
        {
            { 0, DialogueBoxPlayer },
            { 1, DialogueBoxDoctor },
            { 2, DialogueBoxCriminal },
            { 3, DialogueBoxMayor },
            { 4, DialogueBoxMayorBlindFolded }
        };

        nameTextMappings = new Dictionary<int, Text>
        {
            { 0, playerNameText },
            { 1, doctorNameText },
            { 2, criminalNameText },
            { 3, mayorNameText },
            { 4, mayorBlindFoldedNameText }
        };

        dialogueTextMappings = new Dictionary<int, Text>
        {
            { 0, playerDialogueText },
            { 1, doctorDialogueText },
            { 2, criminalDialogueText },
            { 3, mayorDialogueText },
            { 4, mayorBlindFoldedDialogueText }
        };
        
        
       
    }

    public void StartDialogue(Dialogue dialogue)
    {
        StopAllCoroutines();
        state = DialogueState.Start;
        currentDialogue = dialogue;
        sentences = new Queue<string>();
        Debug.Log("Dialogue Manager is Started");
        Debug.Log("Starting dialogue with " + dialogue.name);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (dialogue.isSideDialogue)
        {
            SideDialogueBoxAnimator = SideDialogueBox.GetComponent<Animator>();
            SideDialogueBoxAnimator.SetBool("isActive", true);
            sideNameText.text = dialogue.name;
            DisplayNextSentenceSide();
        }
        else
        {   
           if (dialogueBoxMappings.TryGetValue(dialogue.iconId, out GameObject DialogueBoxActive) &&
            nameTextMappings.TryGetValue(dialogue.iconId, out Text nameText) &&
            dialogueTextMappings.TryGetValue(dialogue.iconId, out Text dialogueText)){
            DialogueBox = DialogueBoxActive;
            DialogueBoxAnimator = DialogueBox.GetComponent<Animator>();
            this.nameText = nameText;
            this.dialogueText = dialogueText;

            DialogueBoxAnimator.SetBool("isActive", true);
            this.nameText.text = dialogue.name;
            DisplayNextSentence();
            }else{
                Debug.Log("Icon ID Not Found");
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        this.dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    IEnumerator TypeSentenceSide(string sentence)
    {
        sideDialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            sideDialogueText.text += letter;
            yield return null;
        }
    }

    
    public void ReplayDialogue()
    {
        StartDialogue(currentDialogue);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentenceSide()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentenceSide(sentence));
    }

    

    void EndDialogue()
    {
        Debug.Log("Dialogue Ended");
        state=DialogueState.End;
        DialogueBoxAnimator.SetBool("isActive",false);
        if(currentDialogue.isSideDialogue){
            SideDialogueBoxAnimator.SetBool("isActive",false);

        }
    }
}
