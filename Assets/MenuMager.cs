using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuMager : MonoBehaviour
{
    // Start is called before the first frame update\
    public Scene portalScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPortalClick(){
       SceneManager.LoadScene("PortalScene");
    }
    public void OnLibraryClick(){
       SceneManager.LoadScene("LibraryScene");

    }
    public void OnArenaCLick(){
       SceneManager.LoadScene("ArenaScene");
        
    }
}
