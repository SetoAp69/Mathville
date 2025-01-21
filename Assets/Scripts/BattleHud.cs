using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    // Start is called before the first frame update
    public Text nameText;
    public Text levelText;

    public Text spText;
    public Text maxSpText;
    public Text maxHpText;
    public Slider spSlider;
    public Slider hpSlider;
    public Text hpText;

    public void SetHUD(Unit unit){
        nameText.text=unit.unitName;
        //levelText.text="lvl "+unit.unitLevel;

        maxHpText.text="/"+unit.maxHp;
        maxSpText.text="/"+unit.maxSp;
        hpText.text=" "+unit.currentHp;
        spText.text=" "+unit.currentSp;

        

        hpSlider.maxValue=unit.maxHp;
        hpSlider.value=unit.currentHp;

        spSlider.maxValue=unit.maxSp;
        spSlider.value=unit.currentSp;
    }

    public void SetHP(int hp){
       // hpSlider.value=hp;
       hpText.text=" "+hp;
       
    }

    public void setSp(int sp){
        spText.text=" "+sp;
    }

}
