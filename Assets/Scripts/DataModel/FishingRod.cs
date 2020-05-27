using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FishingRodBank{
    public static FishingRod LaViejaConfiable = new FishingRod("La Vieja Confiable", LineBank.defaultLine);
}

public static class LineBank{
    public static Line defaultLine = new Line(
        "DefaultLine",
        150.0f
    );
}

public class FishingRod{
    public string name;
    public string Description;
    public Line line;

    public FishingRod(string name, Line line){
        this.name = name;
        this.line = line;
    }
}

public class Line{
    string name;
    public float maxTension;

    public Line(string name, float maxTension){
        this.name = name;
        this.maxTension = maxTension;
    }
}
