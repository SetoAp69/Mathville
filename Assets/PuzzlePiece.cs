using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject piece;
    public bool isActive=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePiece() {
        if(!isActive&&!piece.activeSelf){
            piece.SetActive(true);
            isActive=true;
        }

        Debug.Log("Clicked Through ActivatePiece");        
    }
    public void OnMouseDown() {
        if(!isActive&&!piece.activeSelf){
            piece.SetActive(true);
            isActive=true;
        }
        Debug.Log("Clicked Through OnMouseDown");        

    }
}
