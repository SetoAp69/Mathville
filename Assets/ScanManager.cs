using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spotPrefab;
    public GameObject cameraMain;
    public GameObject battleHud;
    public GameObject scanningHud;
    public GameObject scanningHudMelee;
    public GameObject rangedTargetedAttackButton;
    public GameObject meleeTargetedAttackButton;
    public GameObject scanShieldHud;
    public Text intercept1;
    public Text slope1;
    public Text intercept2;
    public Text slope2;
    public Text coefficientY1;
    public Text coefficientY2;


    private List<GameObject> spotInstances = new List<GameObject>();
    public void setupScan(Unit enemy,bool isMelee){
        
        if(isMelee){
            
            scanningHudMelee.SetActive(true);
        }else{
            
            scanningHud.SetActive(true);

        }

        foreach (Coordinate coordinate in enemy.WeakPoints){
            if(coordinate !=null){
              GameObject spotInstance = Instantiate(spotPrefab, new Vector3(coordinate.X * 30, coordinate.Y * 30, 0), Quaternion.identity);
                spotInstances.Add(spotInstance);

            }
        }

        cameraMain.transform.position=new Vector3(0,0,-10);

        
        battleHud.GetComponent<Animator>().SetBool("isActive",false);

    }
    public void DestroyInstances(){
        foreach (GameObject spotInstance in spotInstances)
                {
                    Destroy(spotInstance);
                }
                spotInstances.Clear();
    }
    public void OnBackButton(){
        cameraMain.transform.position=new Vector3(2437,124,-10);
        scanningHud.SetActive(false);
        scanningHudMelee.SetActive(false);
    }

    public void SetUpShieldScan(Unit enemyUnit){
        battleHud.GetComponent<Animator>().SetBool("isActive",false);
        cameraMain.transform.position=new Vector3(-2437*2,124,-10);
        scanShieldHud.SetActive(true);
        intercept1.text=enemyUnit.shieldEq1.intercept.ToString();
        intercept2.text=enemyUnit.shieldEq2.intercept.ToString();
        slope1.text=enemyUnit.shieldEq1.slope.ToString();
        slope2.text=enemyUnit.shieldEq2.slope.ToString();
        coefficientY1.text=enemyUnit.shieldEq1.coefficientY==1?"":enemyUnit.shieldEq1.coefficientY.ToString();
        coefficientY2.text=enemyUnit.shieldEq2.coefficientY==1?"":enemyUnit.shieldEq2.coefficientY.ToString();

        
    }

    
}
