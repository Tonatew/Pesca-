using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingManager : MonoBehaviour{
    [SerializeField]
    GameObject hookPrefab;
    public GameObject activeHook;
    [SerializeField]
    Transform ARcameraTransform;
    public bool isOnTheHole;
    [SerializeField] CableProceduralSimple cable;
    [SerializeField] Object cablePreset;
    [SerializeField] Animator FishingRodAnimator;
    
    // Start is called before the first frame update
    public void Start(){
        activeHook = null;
        isOnTheHole = false;
        //Preset cable
        cable.sagAmplitude = 0.97f;
        cable.swayMultiplier = 0.33f;
        cable.swayXMultiplier = 0.1f;
        cable.swayYMultiplier = 0.5f;
    }

    // Update is called once per frame
    void Update(){
        if(activeHook == null){
            activeHook = Instantiate(hookPrefab, ARcameraTransform.position, ARcameraTransform.rotation);
            activeHook.transform.position += ARcameraTransform.forward*1;
            activeHook.transform.position += ARcameraTransform.up*-0.5f;
            activeHook.transform.SetParent(ARcameraTransform);
            if(!cable.gameObject.activeInHierarchy){
                cable.gameObject.SetActive(true);
            }
            cable.endPointTransform = activeHook.transform;

            activeHook.GetComponent<SwipeDetector>().fishingRodAnimator = FishingRodAnimator;
        }
        else{
            isOnTheHole = activeHook.GetComponent<SwipeDetector>().isOnTheHole;
        }
    }


}
