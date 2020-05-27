using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelingRateManager : MonoBehaviour{
    //Input
    [SerializeField] Button reelRight;
    [SerializeField] Button reelLeft;

    //Variables
    int tapRightCount;
    int tapLeftCount;
    float startTime;
    ReelDirection lastDirection;

    //Output
    public float reelingRate;  

    //Start
    private void Start() {
        tapLeftCount = 0;
        tapRightCount = 0;
        InvokeRepeating("CalculateRate", 0f, 1f);
        startTime = Time.time;
    }
    
    private void Update() {

    }

    public void ReelRight(){
        tapRightCount++;
        lastDirection = ReelDirection.Right;
        tapLeftCount = 0;
    }

    public void ReelLeft(){
        tapLeftCount++;
        lastDirection = ReelDirection.Left;
        tapRightCount = 0;
    }

    void CalculateRate(){
        if(lastDirection == ReelDirection.Right){
            reelingRate = tapRightCount / (Time.time - startTime);
        }
        else{
            reelingRate = -tapLeftCount / (Time.time - startTime);
        }
        startTime = Time.time;
        tapRightCount = 0;
        tapLeftCount = 0;
    }
    
}

enum ReelDirection{
    Right, Left
}



