using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPosition : MonoBehaviour
{

    public BattleSystem battleSystem;
    public String enemyId;
    // Start is called before the first frame update
    
   private void OnMouseDown() {
    Debug.Log("Clicked");

    if(battleSystem!=null){
        battleSystem.SelectEnemy(enemyId);
    }else{
        Debug.LogError("Battle System not found ");
    }

   }
}
