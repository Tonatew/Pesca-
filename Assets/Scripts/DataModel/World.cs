using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class World{
    public static Region Mexico = new Region("Mexico",
        //Description
        @"Mexico has privileged climatic and territorial conditions that allow it to have a great variety of fish.
        The most representative species for the amount of income they generate in Mexico are tuna, mojarra and shrimp. 
        Despite the great fishing potential, in Mexico families allocate only 2.8% of their total spending to the purchase of fish food.",
        //probabilities
        new Dictionary<string, float>{
            ["Pacific Sardine"] =   0.3f,
            ["Mojarra"] =           0.3f,
            ["Octopus Maya"] =      0.2f,
            ["Yellow Tuna"] =       0.1f,
            ["Jaiba"] =             0.1f,
        },
        //Length[cm] Distributions
        new Dictionary<string, NormalDistribution>{
            ["Pacific Sardine"] =   new NormalDistribution(16.5f, 2.5f),
            ["Mojarra"] =           new NormalDistribution(25.9f, 3.9f),
            ["Octopus Maya"] =      new NormalDistribution(10.457f, 2.63f),
            ["Yellow Tuna"] =       new NormalDistribution(140.0f, 15.0f),
            ["Jaiba"] =             new NormalDistribution(9.57f, 1.5f),
        },
        //ConditionFactor = mass[kg]/length[cm] -> CF*length[cm] = mass [kg]
        new Dictionary<string, float>{
            ["Pacific Sardine"] =   0.0130f,
            ["Mojarra"] =           0.0125f,
            ["Octopus Maya"] =      0.0614f,
            ["Yellow Tuna"] =       0.2500f,
            ["Jaiba"] =             0.0078f,
        });
}

public class Region{
    public string name;
    public string description;
    public Dictionary<string, float> probabilities;
    public Dictionary<string, NormalDistribution> lengthsDistributions;
    public Dictionary<string, float> lengthMassVariation;

    public Region(  
        string name, string description, 
        Dictionary<string, float> probabilities, 
        Dictionary<string, NormalDistribution> massDistributions, 
        Dictionary<string, float> lengthMassVariation){
            this.name = name;
            this.description = description;
            this.probabilities = probabilities;
            this.lengthsDistributions = massDistributions;
            this.lengthMassVariation = lengthMassVariation;
    }

    public Fish GenerateRandomFish(){
        string selectedFishName = WeigthRandom.Choose(this.probabilities);
        float length = lengthsDistributions[selectedFishName].GetRandomFloat();
        float mass = length * lengthMassVariation[selectedFishName];
        float stamina = Fish.staminas[selectedFishName];
        float strength = Fish.strengths[selectedFishName];

        return new Fish(selectedFishName, length, mass, stamina, strength);
    }
}

public static class WeigthRandom{
    //Return a random value with weigths
    public static string Choose(Dictionary<string, float> dictionary){
        float prob = Random.value;
        
        foreach(KeyValuePair<string, float> entry in dictionary){
            prob -= entry.Value;
            if(prob <= 0) return entry.Key;
        }

        throw new System.Exception("Porbabilities do not add up to 1.0");
    }

    public static bool ChooseBool(Dictionary<bool, float> dictionary){
        float prob = Random.value;
        
        foreach(KeyValuePair<bool, float> entry in dictionary){
            prob -= entry.Value;
            if(prob <= 0) return entry.Key;
        }

        throw new System.Exception("Porbabilities do not add up to 1.0");
    }
}


