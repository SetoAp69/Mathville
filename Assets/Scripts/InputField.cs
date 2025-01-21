using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class InputField : MonoBehaviour
{
    // Start is called before the first frame update

    // [Header("The value we got from the input field")]


    
    void Start()
    {
        
    }
    public string input;
    // [SerializeField] private GameObject reactionGroup;
    // [SerializeField]private TMP_Text reactionTextBox;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadStringInput(string s){
        input=s;
        Debug.Log(input);
    //     reactionTextBox.text=input;
    //     reactionGroup.SetActive(true);
    // }


}
}