using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanHUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Text x;
    public Text y;
    public Text x1;
    public Text y1;
    public Text x2;
    public Text y2;
    public Text x3;
    public Text y3;


    public void SetHud(){
        x.text=PlayerPrefs.GetInt("weakX")+"";
        y.text=PlayerPrefs.GetInt("weakY")+"";
        x1.text=PlayerPrefs.GetInt("weakX1")+"";
        y1.text=PlayerPrefs.GetInt("weakY1")+"";
        x2.text=PlayerPrefs.GetInt("weakX2")+"";
        y2.text=PlayerPrefs.GetInt("weakY2")+"";
        x3.text=PlayerPrefs.GetInt("weakX3")+"";
        y3.text=PlayerPrefs.GetInt("weakY3")+"";

    }
}
