using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using System.IO;
using UnityEngine.Rendering;


public enum BattleState{START, PLAYERTURN, ENEMYTURN,WON,LOST}
public class BattleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    int atkBtnClicked;
    public GameObject mainCamera;
    public ScanManager scanManager;

    public GameObject battleHud;
    public GameObject scanHud;
    public GameObject scanHudMelee;

    public BattleState state;
    public GameObject playerPrefab;
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;

    public GameObject WinHud;
    public GameObject LostHud;
    public GameObject ScanShieldHud;
    public Animator StarAnimator1;
    public Animator StarAnimator2;
    public Animator StarAnimator3;
    public Animator BannerAnimator;
    
    public Text TurnText;
    public Transform playerPosition;
    public Transform enemyPosition;
    public Transform enemyPosition1;
    public Transform enemyPosition2;

    public InputField xInput;
    public InputField yInput;

    public InputField mInput;
    public InputField bInput;

    public InputField shieldX;
    public InputField shieldY;

    public StageEndScreen endScreen;
    



    public Text dialogueText;
    public UnityEngine.SceneManagement.Scene Scanning;

    bool isCoroutineRunning=false;
    bool isInCycle=true;
    bool isActionCompleted=false;
    
    public BattleHud playerHud;
    private int cycle=0;
    

    public UltimateButton UltBtn;
    bool playerActionCompleted=false;
    public EnemyHud enemyHud;
     Dictionary<string, Unit> enemyDictionary = new Dictionary<string, Unit>();

    Unit playerUnit;
    Unit enemyUnit;
    Unit enemyUnit1;
    Unit enemyUnit2;
    Unit[] activeUnits = new Unit[4];

    Unit selectedEnemy;
    GameObject PlayerGO;
    bool isPlayerTurn=false;
    bool isEnemyTurn=false;
    bool isMeele=false;
    bool isEquationCorrect=false;
    bool isShieldBreak=false;
    private int UltButtonClicked=0;

    

    void Start()
    {
        state=BattleState.START;
        battleHud.GetComponent<Animator>().SetBool("isActive",true);
        StartCoroutine(SetupBattle());
        
    }
    void Update(){
        // if(state==BattleState.ENEMYTURN){

        // }else{
        //     PlayerTurn();
        // }
        

    }
    IEnumerator  SetupBattle(){
        Debug.Log("Starting setup Coroutine");

        GameObject playerGO = Instantiate(playerPrefab,playerPosition);
        this.PlayerGO=playerGO;
        GameObject enemyGO = Instantiate(enemyPrefab,enemyPosition);
        GameObject enemyGO1 = Instantiate(enemyPrefab1,enemyPosition1);
        GameObject enemyGO2= Instantiate(enemyPrefab2,enemyPosition2);

        playerUnit = playerGO.GetComponent<Unit>();
        enemyUnit=enemyGO.GetComponent<Unit>();
        enemyUnit1=enemyGO1.GetComponent<Unit>();
        enemyUnit2=enemyGO2.GetComponent<Unit>();

        activeUnits[0]=playerUnit;
        activeUnits[1]=enemyUnit;
        activeUnits[2]=enemyUnit1;
        activeUnits[3]=enemyUnit2;

        

        activeUnits[1].name="Enemy 1";
        activeUnits[2].name="Enemy 2";
        activeUnits[3].name="Enemy 3";
        
        enemyHud.SetHUD(activeUnits[1]);


        foreach(Unit unit in activeUnits){
            if(unit.isPlayer){
                unit.isUlting=false;
            }
            unit.currentHp=unit.maxHp;
            unit.currentSpeed=unit.speed;
            unit.isWalking=false;
            unit.wasFallen=false;
            unit.isIddle=true;
            unit.isAttacking=false;
            unit.isAttacked=false;
            

        }

        playerHud.SetHUD(playerUnit);
        enemyDictionary.Add("Enemy1", activeUnits[1]);
        enemyDictionary.Add("Enemy2", activeUnits[2]);
        enemyDictionary.Add("Enemy3", activeUnits[3]);

        SelectEnemy("Enemy1");
        

        // yield return new WaitForSeconds(2f);

        state=BattleState.START;
        
        bool enemyWipe = activeUnits[1].currentHp==0&&activeUnits[2].currentHp==0&&activeUnits[3].currentHp==0;
        
        while((!enemyWipe)&&activeUnits[0].currentHp>0){
            Debug.Log("enemy wipe = "+ enemyWipe);
            if(cycle==0){
                yield return StartCoroutine(BattleCycle2());
                
            }else{
                yield return new WaitUntil(()=>!isInCycle);
                Debug.Log("Calling StartCoroutine(StartCycle)");
                yield return StartCoroutine(BattleCycle2());
            }
            
            
            
        }
        if(enemyWipe){
            StopAllCoroutines();
            Debug.Log("YOU WIN");
        }
        
        
        

        


    }
    void PlayerWin(){
        StopAllCoroutines();
        PlayerGO.GetComponent<Animator>().SetBool("isAttacking",false);

        battleHud.SetActive(false);    
        endScreen.Win();

    }
    void PlayerLost(){
        StopAllCoroutines();
        battleHud.SetActive(false);
        endScreen.Lost();
    }
    
   
    

    IEnumerator PlayerAttack(int multiplier=1,bool melee=false){
        Debug.Log("Player Attack is Called");

        Boolean isDead = selectedEnemy.TakeDamage(playerUnit.damage*multiplier);
        

        if(melee){
            playerUnit.isMeleeAttacking=true;

        }else{
            playerUnit.isAttacking=true;

        }
        selectedEnemy.isIddle=false;

        yield return new WaitForSeconds(0.2f);
        selectedEnemy.isAttacked=true;
        // selectedEnemy.isIddle=false;
        yield return new WaitForSeconds(1.5f);
        playerUnit.isAttacking=false;
        playerUnit.isMeleeAttacking=false;
        selectedEnemy.isAttacked=false;
        playerUnit.GainSp(1);


        

        enemyHud.SetHP(selectedEnemy.currentHp);
        
        
        playerHud.setSp(playerUnit.currentSp);
        if(isDead){
            selectedEnemy.wasFallen=true;
            yield return new WaitForSeconds(1.5f);

            selectedEnemy.wasFallen=false;
            selectedEnemy.gameObject.SetActive(false);
            foreach(Unit unit in activeUnits){
                if(!unit.isDead&&(!unit.isPlayer)){
                    selectedEnemy=unit;
                    enemyHud.SetHUD(selectedEnemy);

                }
            }
            // Destroy(selectedEnemy,1f);
        }

        if(isDead){
            state=BattleState.WON;
            // EndBattle();
            playerActionCompleted=true;
            isActionCompleted=true;

        }else{
            // state=BattleState.ENEMYTURN;
            isPlayerTurn=true;
            playerActionCompleted=true;
            Debug.Log("Action Completed");
            isActionCompleted=true;
        }
        //playerActionCompleted=true;
    }

    IEnumerator PlayerTurn(){

        while(!playerActionCompleted){
            yield return null;

        }
        playerActionCompleted=false;


    }
    Unit GetNextFastestUnit(int currentIndex){
    Unit nextUnit = null;
    float minSpeed = float.MaxValue;

    for (int j = 0; j < activeUnits.Length; j++){
        if (j != currentIndex && activeUnits[j].currentSpeed < minSpeed){
            minSpeed = activeUnits[j].currentSpeed;
            nextUnit = activeUnits[j];
        }
    }

    return nextUnit;
}

   

    IEnumerator BattleCycle2(){
        
        bool enemyWipe = activeUnits[1].currentHp<=0&&activeUnits[2].currentHp<=0&&activeUnits[3].currentHp<=0;
        if(enemyWipe&&activeUnits[0].currentHp>0){
            playerUnit.isWinning=true;
            yield return new WaitForSeconds(3f);

            PlayerWin();

            Debug.Log("WIN");

            
            
        }
        if(activeUnits[0].currentHp<=0){
                PlayerLost();
                Debug.Log("Lost");
                
        }
        isInCycle=true;
        if(playerUnit.currentSp==playerUnit.maxSp){
            UltBtn.isReady=true;
            UltBtn.gameObject.SetActive(true);
        }
        for(int i=0;i<activeUnits.Length;i++){
            Unit fastest=activeUnits[i];
            Unit nextUnit=GetNextFastestUnit(i);
            for(int j=0;j<activeUnits.Length;j++){
                if(activeUnits[j].speed>fastest.speed){
                    fastest=activeUnits[j];
                }
            }

            if(fastest.isDead){
                
            }

            if(fastest.isPlayer){
                atkBtnClicked=0;
                UltButtonClicked=0;
                

                Debug.Log("PLayer turns");
                TurnText.text="Player";
                state=BattleState.PLAYERTURN;
                isActionCompleted=false;
                yield return new WaitUntil(()=>isActionCompleted);
                fastest.speed-=100;
                state=BattleState.ENEMYTURN;
            }else{
                
                TurnText.text=fastest.name;
                if(state!=BattleState.PLAYERTURN){
                    if(nextUnit.isPlayer){
                    yield return StartCoroutine(EnemyTurn(fastest));
                    state=BattleState.PLAYERTURN;
                    isActionCompleted=false;
                }else{
                    Debug.Log("Starting Enemy turn = "+fastest.name);
                    yield return StartCoroutine(EnemyTurn(fastest));
                    state=BattleState.ENEMYTURN;
                    }
                }
                Debug.Log("Fastest = "+fastest.name+" Speed = "+fastest.speed);
                fastest.speed-=100; 
                
            }
            
        }
        for(int o=0;o<activeUnits.Length;o++){
            activeUnits[o].speed+=100;
        }

        
        cycle++;
        isInCycle=false;
        
    }

    IEnumerator ExecuteEnemyTurn(Unit enemyUnit){
        // if(!isEnemyTurn){
        //     yield return null;
        // }else{
        //     isEnemyTurn=false;

        //     yield return StartCoroutine(EnemyTurn(enemyUnit));

        //     isEnemyTurn=true;
        // }

        // yield return new WaitUntil(()=>isEnemyTurn);
        if(isEnemyTurn){
            yield return StartCoroutine(EnemyTurn(enemyUnit));
        }
    //    isEnemyTurn=false;
    }

    IEnumerator EnemyTurn(Unit enemyUnit){
        yield return new WaitForSeconds(1f);
        if(!enemyUnit.isDead){
            enemyUnit.isIddle=false;
            if(enemyUnit.currentSp==enemyUnit.maxSp){
                enemyUnit.shieldActive=true;
                enemyUnit.currentSp=0;
                
            }
            if(enemyUnit.currentSp<enemyUnit.maxSp){
                enemyUnit.currentSp++;
            }
            
            enemyUnit.isAttacking=true;
            yield return new WaitForSeconds(1f);
            Debug.Log("Wait for 1s is Finished");

            // yield return StartCoroutine(WaitForAnimationToEnd(enemyUnit.enemyAnimator,"isAttacking"));


            playerUnit.isAttacked=true;
            // yield return new WaitForSeconds(1.5f);
            Debug.Log("Wait for 1.5s is Finished");

            
            // Debug.Log("Enemy Turn from the coroutine");
            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
            playerHud.SetHP(playerUnit.currentHp);
            Debug.Log("EnemyTurn : "+enemyUnit.name);
            playerUnit.isAttacked=false;
            enemyUnit.isAttacking=false;
            if(isDead){
                Debug.Log("Player lost"); 
                playerUnit.wasFallen=true;
                yield return new WaitForSeconds(3f);
                PlayerLost();

                
            }
        }else{
             enemyUnit.isAttacking=true;
            yield return new WaitForSeconds(0f);
            Debug.Log(enemyUnit.name +" is Dead");
            enemyUnit.isAttacking=false;

        }



      
   
        
    }


    

    void EndBattle(){

    }

    public void OnScanBackButton(){
        mainCamera.transform.position=new Vector3(2437,124,-10);
        if(ScanShieldHud!=null){
            ScanShieldHud.SetActive(false);

        }
        if(scanHud!=null){
            scanHud.SetActive(false);

        }
        if(scanHudMelee!=null){
            scanHudMelee.SetActive(false);

        }
        battleHud.GetComponent<Animator>().SetBool("isActive",true);
    }

    public void OnAttackButton(){
        if(state != BattleState.PLAYERTURN&&!playerActionCompleted){
            return;

        }
        if(atkBtnClicked<1){
            
            Debug.Log("OnAttackButton pressed");
            StartCoroutine(PlayerAttack());
            state=BattleState.ENEMYTURN;
            atkBtnClicked++;
        }

        
    }
    public void OnMeleeAttackButton(){
        if(state != BattleState.PLAYERTURN&&!playerActionCompleted){
            return;

        }
        if(atkBtnClicked<1){
            
            Debug.Log("OnAttackButton pressed");
            StartCoroutine(PlayerAttack(1,true));
            state=BattleState.ENEMYTURN;
            atkBtnClicked++;
        }
    }

    public void OnSwitchAttackButton(){
        isMeele=!isMeele;
        battleHud.GetComponent<Animator>().SetBool("isMeele",isMeele);
    }

    public void OnShieldScanButton(){
        if(state != BattleState.PLAYERTURN){
            return;
        }else{
            if(selectedEnemy.shieldActive)
            scanManager.SetUpShieldScan(selectedEnemy);
            Debug.Log("Scan pressed");
        }

    }


    public void OnBreakShiledButton(){
        mainCamera.transform.position=new Vector3(2437,124,-10);
        ScanShieldHud.SetActive(false);
        battleHud.GetComponent<Animator>().SetBool("isActive",true);
        bool coordCorrect=CheckLineInterceptions(StringToFloat(shieldX.input),StringToFloat(shieldY.input));
        
        if(coordCorrect){
            if(atkBtnClicked<1){
                selectedEnemy.shieldActive=false;
                Debug.Log("Shield Broke");
                StartCoroutine(PlayerAttack(3,true));
                state=BattleState.ENEMYTURN;
                atkBtnClicked++;
            }
        }else{
            if(atkBtnClicked<1){
            
                Debug.Log("OnAttackButton pressed");
                StartCoroutine(PlayerAttack(1,true));
                state=BattleState.ENEMYTURN;
                atkBtnClicked++;
            }
        }
        

        if(state != BattleState.PLAYERTURN&&!playerActionCompleted){
            return;

        }

    }

    public void OnTargetedAttackButton(){

        mainCamera.transform.position=new Vector3(2437,124,-10);
        scanHud.SetActive(false);
        battleHud.GetComponent<Animator>().SetBool("isActive",true);

        if(isCrit((float)int.Parse(xInput.input),(float)int.Parse(yInput.input))){
            if(atkBtnClicked<1){
            
                Debug.Log("OnAttackButton pressed");
                StartCoroutine(PlayerAttack(2));
                state=BattleState.ENEMYTURN;
                atkBtnClicked++;
            }
        }else{
            if(atkBtnClicked<1){
            
                Debug.Log("OnAttackButton pressed");
                StartCoroutine(PlayerAttack());
                state=BattleState.ENEMYTURN;
                atkBtnClicked++;
            }
        }
        

        if(state != BattleState.PLAYERTURN&&!playerActionCompleted){
            return;

        }
        
        

        
    }

    public void OnTargetedMeeleAttack(){
        mainCamera.transform.position=new Vector3(2437,124,-10);
        scanHudMelee.SetActive(false);
        battleHud.GetComponent<Animator>().SetBool("isActive",true);
        bool EqCorrect=CheckLinearEquationAnswer(StringToFloat(mInput.input),StringToFloat(bInput.input));
        
        if(EqCorrect){
            if(atkBtnClicked<1){
            
                Debug.Log("triple damage");
                StartCoroutine(PlayerAttack(3,true));
                state=BattleState.ENEMYTURN;
                atkBtnClicked++;
            }
        }else{
            if(atkBtnClicked<1){
            
                Debug.Log("OnAttackButton pressed");
                StartCoroutine(PlayerAttack(1,true));
                state=BattleState.ENEMYTURN;
                atkBtnClicked++;
            }
        }
        

        if(state != BattleState.PLAYERTURN&&!playerActionCompleted){
            return;

        }
    }

    bool isCrit(float xPoint,float yPoint){

        // if(xPoint==selectedEnemy.weakX||xPoint==selectedEnemy.weakX1||xPoint==selectedEnemy.weakX2||xPoint==selectedEnemy.weakX3){
        //     if(yPoint==selectedEnemy.weakY||yPoint==selectedEnemy.weakY1||yPoint==selectedEnemy.weakY2||yPoint==selectedEnemy.weakY3){
        //         return true;
        //     }
        // }
        // foreach(Coordinate coordinate in selectedEnemy.WeakPoints){
        //     if(coordinate.X==xPoint&&coordinate.Y==xPoint){
                
        //         return true;
        //     }else{
        //         return false;
        //     }
        // }

        for(int i=0;i<selectedEnemy.WeakPoints.Length;i++){
            if(selectedEnemy.WeakPoints[i]!=null){
                if(selectedEnemy.WeakPoints[i].X==xPoint&&selectedEnemy.WeakPoints[i].Y==yPoint){
                    selectedEnemy.WeakPoints[i]=null;
                    return true;
                }else{
                                
            }
            }
            
        }
        

        return false;
    }

    public void OnScanButton(){
        if(state != BattleState.PLAYERTURN){
            return;
        }else{
            scanManager.setupScan(selectedEnemy,false);
            Debug.Log("Scan pressed");
        }
        
    }

    public void OnMeleeScanButton(){
        if(state != BattleState.PLAYERTURN){
                    return;

        }
        scanManager.setupScan(selectedEnemy,true);
        Debug.Log("Scan pressed");
    }

    public void SelectEnemy(String enemyId){
        if(enemyDictionary.ContainsKey(enemyId)){
            selectedEnemy=enemyDictionary[enemyId];
            enemyHud.SetHUD(selectedEnemy);
            PlayerPrefs.SetInt("weakX",selectedEnemy.weakX);
            PlayerPrefs.SetInt("weakX1",selectedEnemy.weakX1);
            PlayerPrefs.SetInt("weakX2",selectedEnemy.weakX2);
            PlayerPrefs.SetInt("weakX3",selectedEnemy.weakX3);
            PlayerPrefs.SetInt("weakY",selectedEnemy.weakY);
            PlayerPrefs.SetInt("weakY1",selectedEnemy.weakY1);
            PlayerPrefs.SetInt("weakY2",selectedEnemy.weakY2);
            PlayerPrefs.SetInt("weakY3",selectedEnemy.weakY3);

            

        }else{
            Debug.LogError("Enemy with ID "+enemyId+" can't be found!");
        }

        
    }


    IEnumerator PlayerUlt(){
        playerUnit.isUlting=true;
        yield return new WaitForSeconds(2.5f);
        
        foreach(Unit unit in activeUnits){
            if(!unit.isPlayer){
                unit.isAttacked=true;
                Boolean isEnemyDead = unit.TakeDamage(playerUnit.damage*3);
                unit.isAttacked=true;
                if(isEnemyDead){
                    unit.wasFallen=true;
                    yield return new WaitForSeconds(0.5f);
                    unit.gameObject.SetActive(false);
                }
                unit.isAttacked=false;
            }
        }
        isPlayerTurn=true;
            playerActionCompleted=true;
            Debug.Log("Action Completed");
            isActionCompleted=true;
        playerUnit.isUlting=false;

        
    }

    public void OnUltButton(){

        if(state != BattleState.PLAYERTURN&&!playerActionCompleted){
            return;

        }
        UltBtn.isReady=false;

        if(playerUnit.currentSp==playerUnit.maxSp&&UltButtonClicked==0){
            UltButtonClicked++;
            Debug.Log("OnAttackButton pressed");
            StartCoroutine(PlayerUlt());
            state=BattleState.ENEMYTURN;
            playerUnit.currentSp=0;
            playerHud.setSp(playerUnit.currentSp);
        }

        
    }

    private float CalculateSlope(Coordinate coord1,Coordinate coord2){

        
        return(coord2.Y-coord1.Y)/(coord2.X-coord1.X);

    }

    private float CalculateIntercept(Coordinate coord,float slope){
        return coord.Y-coord.X*slope;
    }

    private bool CheckLinearEquation(Coordinate coord1, Coordinate coord2,float m,float b){

        float slope= CalculateSlope(coord1,coord2);
        float intercept=CalculateIntercept(coord1,slope);
        if(slope==m&&intercept==b){
            return true;
        }

        return false;
    }

    private bool CheckLinearEquationAnswer(float m, float b){
        bool correct=false;
        
        for(int i=0;i<selectedEnemy.WeakPoints.Length;i++){
            
            if(selectedEnemy.WeakPoints[i]!=null){
                for(int j=i+1;j<selectedEnemy.WeakPoints.Length;j++){
                    if(selectedEnemy.WeakPoints[j]!=null){
                        isEquationCorrect=CheckLinearEquation(selectedEnemy.WeakPoints[i],selectedEnemy.WeakPoints[j],m,b);
                        if(isEquationCorrect){
                            Debug.Log("Equation is COrrect");
                            Debug.Log("Is the Eq Correct : "+isEquationCorrect);
                            correct=true;
                            selectedEnemy.WeakPoints[i]=null;
                            selectedEnemy.WeakPoints[j]=null;


                        }
                    }
                }                
            }

        }
        return correct;
    }
    private bool CheckLineInterceptions(float x, float y){
        return  selectedEnemy.shieldEq1.coefficientY*y==selectedEnemy.shieldEq1.slope*x+selectedEnemy.shieldEq1.intercept && selectedEnemy.shieldEq1.coefficientY*y==selectedEnemy.shieldEq1.slope*x+selectedEnemy.shieldEq1.intercept;
        
        

         
    }
    private float StringToFloat(string input)
    {
        if (input.Contains("/"))
        {
            
            string[] parts = input.Split('/');
            if (parts.Length == 2 && int.TryParse(parts[0], out int numerator) && int.TryParse(parts[1], out int denominator))
            {
                if (denominator != 0)
                {
                    return (float)numerator / denominator;
                }
                else
                {
                    throw new ArgumentException("Denominator cannot be zero.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid fraction format.");
            }
        }
        else
        {
            
            if (float.TryParse(input, out float floatValue))
            {
                return floatValue;
            }
            else
            {
                throw new ArgumentException("Invalid number format.");
            }
        }
    }


    

    // Update is called once per frame
    
}
