using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHp;
    public int currentHp;

    public int maxSp;
    public int currentSp;
    public int[,] weakPoints= new int [4,2];
    public int weakX;
    public int weakY;
    public int weakX1;
    public int weakY1;
    public int weakX2;
    public int weakY2;
    public int weakX3;
    public int weakY3;

    public Coordinate [] WeakPoints=new Coordinate[4];

    private Animator playerAnimator;

    public Animator enemyAnimator;
    public int speed;
    public int currentSpeed;
    public bool isPlayer;
    public enum UnitState{IDDLE, ATTACK, WALK,FALLEN,ATTACKED, ULT};

    public bool isIddle;
    public bool isAttacking;
    public bool isWalking;

    public bool isMeleeAttacking;
    public bool wasFallen;
    public bool isAttacked;
    public bool isUlting;
    public bool isDead=false;
    public bool isWinning=false;
    public bool hasShield=false;
    public bool shieldActive;
    public Line shieldEq1;
    public Line shieldEq2;
    public HashSet<(float,float)> coordinate= new HashSet<(float, float)>();
    public bool TakeDamage(int dmg){

        

        if(dmg>=currentHp){
            currentHp=0;
            isDead=true;
        }else{
            if(shieldActive){
                currentHp--;
            }else{
             currentHp-=dmg;

            }

        }
        if(currentHp<=0){
            return true;
        }
        else 
            return false;

    }

    public void GainSp(int sp){
        currentSp+=sp;

        if(currentSp>=maxSp){
            currentSp=maxSp;
            
        }
        
    }

     void Awake() {
        if(isPlayer){
            playerAnimator= GetComponent<Animator>();
        }else{
            enemyAnimator= GetComponent<Animator>();    
            

        }

    }

    public void resetAnimation(){
        isAttacking=false;
        isAttacked=false;
        wasFallen=false;
        isIddle=true;
        isMeleeAttacking=false;

    }
    void FixedUpdate() {

    if(isPlayer){
        playerAnimator.SetBool("isAttacking", isAttacking);
        playerAnimator.SetBool("isAttacked",isAttacked);
        playerAnimator.SetBool("isUlting",isUlting);
        // playerAnimator.SetBool("isIddle",isIddle);
        playerAnimator.SetBool("wasFallen",wasFallen);
        playerAnimator.SetBool("isWalking",isWalking);
        playerAnimator.SetBool("isWinning",isWinning);
        playerAnimator.SetBool("isMeleeAttacking",isMeleeAttacking);
        
    }else{
        enemyAnimator.SetBool("isAttacking", isAttacking);
        enemyAnimator.SetBool("isAttacked", isAttacked);
        // enemyAnimator.SetBool("isIddle", isIddle);
        enemyAnimator.SetBool("isWalking", isWalking);
        enemyAnimator.SetBool("wasFallen", wasFallen);
        enemyAnimator.SetBool("isShieldActive",shieldActive);
    }
    }
}
