using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterManager : MonoBehaviour
{
    public void ChapterOnClick(){
       SceneManager.LoadScene("Chapter1");

    }
    public void OnBackButtonClick(){
        SceneManager.LoadScene("MainMenu");
    }
}
