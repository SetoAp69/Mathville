using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenTrigger : MonoBehaviour
{
    public StageEndScreen endScreen;
    // Start is called before the first frame update
    void Start()
    {
        endScreen.Win();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
