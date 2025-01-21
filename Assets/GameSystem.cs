using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour

{

    public Animator BattleHudAnimator;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnContinueButton(){
        pauseMenu.SetActive(false);
    }
    public void onPauseButton(){
        pauseMenu.SetActive(true);
    }
    public void OnQuitButton(){
        SceneManager.LoadScene("MainMenu");

    }
    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }

    
}
