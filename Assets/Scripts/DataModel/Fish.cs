using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish{
    public string name;    
    float length;
    public float mass;
    public float stamina;
    public float strength;
    public string description;

    public static Dictionary<string, float> staminas = new Dictionary<string, float>{
        ["Pacific Sardine"] =   0.07f,
        ["Mojarra"] =           0.04f,
        ["Octopus Maya"] =      0.08f,
        ["Yellow Tuna"] =       0.03f,
        ["Jaiba"] =             0.1f,
    };

    public static Dictionary<string, float> strengths = new Dictionary<string, float>{
        ["Pacific Sardine"] =   0.05f,
        ["Mojarra"] =           0.1f,
        ["Octopus Maya"] =      0.2f,
        ["Yellow Tuna"] =       1.0f,
        ["Jaiba"] =             0.05f,
    };

    static Dictionary<string, string> descriptions = new Dictionary<string,string>{
        ["Pacific Sardine"] =   "Sardine is a pelagic fish (that lives in deep areas and far from the coast) of small size and saltwater, with a hydrodynamic body",
        ["Mojarra"] =           @"They inhabit shallow bottoms of coastal waters, as well as rivers, streams, and lagoons. It is prepared whole, fried in oil, mojo de ajo, in tamales, in different types of sauces, broths or soups, and with rice.
        They are found in rivers and lakes in the interior of the country. Tthere are 23 different species, of which 10 are endemic.",

        ["Octopus Maya"] =      @"An endemic specie of yucatan.
        The Octopus maya Octopus, is recognized for its body symmetry, its great capacity for learning and physical development.
        It is able to learn and memorize in the short and long term, as well as having very large eyes that allow him to visualize the color images of the environment around him.",
        
        ["Yellow Tuna"] =       "The tuna fishery in Mexico is one of the best established and represents an activity of great commercial value. Yellowfin tuna has constituted between 75 and 90% of the annual catch of the Mexican fleet in recent years",
        ["Jaiba"] =             @"The commercial presentation of the crab is varied, it can be frozen fresh whole, cooked in pulp, frozen whole fresh, frozen cooked whole and fresh whole, mainly.
        however, with the added value the product can be purchased in fingers, crab cakes and lolly pops",
    };

    public Fish(string name, float length, float mass, float stamina, float strength){
        this.name = name;
        this.length = length;
        this.mass = mass;
        this.stamina = stamina;
        this.strength = strength;
        this.description = Fish.descriptions[name];
        Debug.Log(description);
    }

}
