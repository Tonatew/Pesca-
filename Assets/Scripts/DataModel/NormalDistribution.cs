using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is made with the unityPackage, this calss only provides an abstraccion for the class
//in the package "RandomFromDistributions"
public class NormalDistribution{
    float mean;
    float standardDeviation;
    public NormalDistribution(float mean, float stdDeviation){
        this.mean= mean;
        this.standardDeviation = stdDeviation;
    }

    public float GetRandomFloat(){
        return RandomFromDistribution.RandomNormalDistribution(mean, standardDeviation);
    }

}
