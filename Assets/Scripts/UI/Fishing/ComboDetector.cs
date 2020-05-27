using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDetector{
    public JoystickMovement joystick;
    
    public JtkDirection[] joystickCombination;
    int index;

    public ComboDetector(JoystickMovement joystick, JtkDirection[] combination){
        this.joystick = joystick;
        this.joystickCombination = combination;
    }
    
    public bool CheckCombo(){
        if(joystick.GetDirection().Equals(joystickCombination[index])){
            index ++;
            if(index >= joystickCombination.Length){
                index = 0;
                return true;
            }
            else{
                return false;
            }
        }
        else{
            index = 0;
            return false;
        }
    }
}


