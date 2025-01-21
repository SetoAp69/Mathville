using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class StageEndScreen : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject playerPrefab;
   public Transform playerPosition;

    public GameObject ScoreBanner;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    Animator ScoreBannerAnimator;
    Animator star1Animator;
    Animator star2Animator;
    Animator star3Animator;
    public GameObject Camera;
    public Animator LoseBannerAnimator;
    public Animator LoseNavAnimator;

    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win(){
        Camera.SetActive(true);


        ScoreBannerAnimator=ScoreBanner.GetComponent<Animator>();
        star1Animator=star1.GetComponent<Animator>();
        star2Animator=star2.GetComponent<Animator>();
        star3Animator=star3.GetComponent<Animator>();

        ScoreBannerAnimator.SetBool("isActive",true);
        star1Animator.SetBool("isActive",true);
        star2Animator.SetBool("isActive",true);
        star3Animator.SetBool("isActive",true);


        GameObject playerGO = Instantiate(playerPrefab, new Vector3(-2422f,623f,0f),Quaternion.identity);
        playerGO.transform.localScale=new Vector3(-8f,8f,8f);
        Camera.transform.position=new Vector3(-2178f,715f,-10f);
    }

    public void Lost(){
        // GameObject playerGO = Instantiate(playerPrefab, new Vector3(-2422f,-210f,0f),Quaternion.identity);
        // playerGO.transform.localScale=new Vector3(-8f,8f,8f);
        Camera.SetActive(true);

        Camera.transform.position=new Vector3(-2178f,-118f,-10f);
        // playerGO.GetComponent<Animator>().SetBool("wasLost",true);
        LoseBannerAnimator.SetBool("isActive",true);
        LoseNavAnimator.SetBool("isActive",true);
        LoseNavAnimator.SetBool("isBlinking",true);

        

    }
}
