using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReelingManager : MonoBehaviour
{
    //Editor
    [SerializeField] Animator fishingRodAnimator;
    
    //public
    public Fish HookedFish;
    public float distance;
    public float tension;
    public ReelingState reelingState;

    //Private
    float startTime;
    FishingRod fishingRod;
    List<Pulse> PullTensionList;
    [SerializeField] float JTimeDuration;
    float nextJTension;
    bool overReelingEnabled;
    [SerializeField] float overReelingIncrementRatio;
    float nextReelingIncrement;
    float reelingIncrementCount;
    float startOfReeling;
    public int neededReleases;

    //SFX
    [SerializeField] AudioSource music;
    [SerializeField] AudioClip actionMusic;
    [SerializeField] AudioClip bgMusic;
    [SerializeField] AudioSource SFX;

    // Start is called before the first frame update
    public void Start(){
        music.clip = actionMusic;
        music.Play();
        fishingRod = GameMaster.selectedRod;
        HookedFish = GameMaster.selectedRegion.GenerateRandomFish();
        Debug.Log(HookedFish.name);
        Debug.Log(HookedFish.mass);
        startTime = Time.time;
        distance = Random.Range(100f, 150f);
        
        fishingRod = GameMaster.selectedRod;
        tension = 0f;
        reelingState = ReelingState.Capturing;

        PullTensionList = new List<Pulse>();
        overReelingEnabled = false;

        startOfReeling = Time.time;
    }

    // Update is called once per frame
    void Update(){
        if(gameObject.GetComponent<ReelingRateManager>().reelingRate > 2){
            overReelingEnabled = true;
            neededReleases = (int)(HookedFish.stamina * 100);
            Debug.Log(neededReleases);
        }
        if(tension > fishingRod.line.maxTension){
            reelingState = ReelingState.BrokenLine;
        }
        else if(distance > 400f){
            reelingState = ReelingState.FishHasGone;
        }
        else if(distance <= 0f){
            music.clip = bgMusic;
            music.Play();
            reelingState = ReelingState.Captured;
        }
        else{
            float newTension = 0f;

            //T_m
            newTension += HookedFish.mass;
            //T_pull
            newTension += CalculatePullTensions();
            //T_reeling
            newTension += CalculateReelingTension();
            //T_overReeling
            newTension += CalculateOverReeling();
            //Energy tension
            newTension += CalculateEnergyTension();

            tension = 1/HookedFish.strength * newTension;

            Dictionary<bool, float> boolProb = new Dictionary<bool, float>(){
                [true] = HookedFish.stamina,
                [false] = (1- HookedFish.stamina)
            };
        }
    }
    //T_pull
    float CalculatePullTensions(){
        float pullTension = 0f;

        for (int i = PullTensionList.Count -1; i>=0; i--){
            if(Time.time - PullTensionList[i].startTime > Mathf.PI/3){
                PullTensionList.RemoveAt(i);
            }
            else{
                pullTension += PullTensionList[i].CalculateTension(HookedFish.mass);
            }
        }

        return pullTension;
    }

    public void CreatePullTension(){
        PullTensionList.Add(new Pulse());
        HookedFish.strength *= (1f - 0.03f);
        fishingRodAnimator.Play("Armature|Pull");
    }

    //T_j
    float CalculateReelingTension(){
        if(Time.time < nextJTension){
            return HookedFish.mass;
        }
        else return 0;
    }

    //T_Reeling
    public void GenerateReelingTension(){
        nextJTension = Time.time + JTimeDuration;
        distance -= 5;
    }

    public void GenerateRealese(){
        if(neededReleases <= 0){
            neededReleases = 0;
            overReelingEnabled = false;
            reelingIncrementCount = 0;
        }
        else{
            neededReleases -= 1;
            distance += 10;
        }
    }

    float CalculateOverReeling(){
        if(overReelingEnabled){
            if(Time.time > nextReelingIncrement){
                reelingIncrementCount++;
                nextReelingIncrement = Time.time + overReelingIncrementRatio;
            }
            return reelingIncrementCount*HookedFish.mass;
        }
        else{
            nextReelingIncrement = Time.time + overReelingIncrementRatio;
            return 0;
        }

    }

    //Energy Tension
    float CalculateEnergyTension(){
        float energy = Mathf.Pow(2.7182f, -Mathf.Pow(HookedFish.stamina * (Time.time - startOfReeling), 2));
        return energy;
    }
}

public enum ReelingState{
    Capturing, Captured, BrokenLine, FishHasGone
}

public class Pulse{
    public float startTime;


    public Pulse(){
        startTime = Time.time;
    }

    public float CalculateTension(float mass){
        if(Time.time - startTime <= Mathf.PI/3){
            return 1.5f*mass*Mathf.Sin(3*(Time.time - startTime));
        }
        else{
            return 0;
        }
    }
}
