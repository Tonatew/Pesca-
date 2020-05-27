using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    string name;
    float money;
    public LinkedList<Region> unlockedRegions;
    public LinkedList<FishingRod> unlockedRods;

    public Player(string name){
        this.name = name;
        this.money = 0.0f;
        this.unlockedRegions = new LinkedList<Region>();
        this.unlockedRegions.AddLast(World.Mexico);
        this.unlockedRods = new LinkedList<FishingRod>();
        this.unlockedRods.AddLast(FishingRodBank.LaViejaConfiable);
    }
}
