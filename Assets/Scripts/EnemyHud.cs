using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHud : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Text maxHpText;
    public Text hpText;
    public Text nameText;

    public Slider hpSlider;

    public void SetHUD(Unit unit){
        nameText.text=unit.unitName;
        //levelText.text="lvl "+unit.unitLevel;

        maxHpText.text="/"+unit.maxHp;
        hpText.text=" "+unit.currentHp;
       
        hpSlider.maxValue=unit.maxHp;
        hpSlider.value=unit.currentHp;
    }
    public void SetHP(int hp){
        hpText.text=" "+hp;
    }


}
