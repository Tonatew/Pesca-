using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingManager : MonoBehaviour
{
    [SerializeField] CableProceduralSimple cable;
    [SerializeField] Object CablePreset;
    [SerializeField] Animator fishingRodeAnimator;

    [SerializeField] float hitFrecuency;
    float nextHit;
    
    public bool fishOntheHook;

    Region region;

    //SFX
    [SerializeField] AudioSource SoundEffects;
    [SerializeField] AudioClip hookedFishEffect;

    // Start is called before the first frame update
    public void Start(){
        region = GameMaster.selectedRegion;
        nextHit = Time.time + Random.Range(5f, hitFrecuency);
        fishOntheHook = false;
    }

    // Update is called once per frame
    void Update(){
        if(nextHit < Time.time){
            cable.sagAmplitude = 0f;
            cable.swayMultiplier = 0.1f;
            cable.swayXMultiplier = 0.64f;
            cable.swayYMultiplier = 0.32f;
            SoundEffects.PlayOneShot(hookedFishEffect);
            fishOntheHook = true;
            Handheld.Vibrate();
            fishingRodeAnimator.Play("Armature|FishHit");
        }
    }
}



