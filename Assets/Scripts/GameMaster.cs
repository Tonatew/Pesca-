using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMaster{
    public static Player player;
    public static Region selectedRegion;
    public static FishingRod selectedRod;

    public static void LoadUser(){
        player = new Player("Farengar");
    }
}
