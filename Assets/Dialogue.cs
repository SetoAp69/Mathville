using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    // Start is called before the first frame update
   public string name;
   public bool isSideDialogue;
   public bool isRepeateable;
   public int iconId;
   public bool isFinish;
   
   
[TextArea(3,10)]
   public string[]sentences;

   

}
