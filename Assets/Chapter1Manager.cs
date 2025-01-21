using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnStageButtonClick(string name){

        SceneManager.LoadScene(name);
    }
    public void OnBackButtonClick(){
        SceneManager.LoadScene("PortalScene");
    }
}
