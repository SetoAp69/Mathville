using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateButton : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isReady=false;
    public GameObject Button;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake(){
        Button=this.gameObject;
    }
    void FixedUpdate(){
        // if(isReady){
        //     Button.SetActive(true);
        // }else{
        //     Button.SetActive(false);
        // }
    }


}
