using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FishingManager : MonoBehaviour
{
    public FishingPhase fishingPhase;
    Fish caugthFish;

    //Setting fishingHole
    [SerializeField] PlacementIndicator placementIndicator;
    [SerializeField] ObjectSpawner objectSpawner;
    [SerializeField] GameObject Setter;
    FishingHole fishingHole;

    //CastingManager
    [SerializeField]
    CastingManager castingManager;
    public GameObject activeHook;
    [SerializeField]
    GameObject FishingRode;
    CableProceduralSimple cable;

    //WaitingManager
    [SerializeField] WaitingManager waitingManager;

    //ReelingManager
    [SerializeField] ReelingManager reelingManager;

    //VisualuazingManager
    [SerializeField]  VisualizingManager visualizingManager;
    Fish hookedFish;

    //UI
    [SerializeField]
    FishingUI fishingUI;

    // Start is called before the first frame update
    void Start(){
        fishingPhase = FishingPhase.Placement;
    }

    // Update is called once per frame
    void Update(){
        switch (fishingPhase){
            //Put the hole in the world
            case FishingPhase.Placement:
                if(!placementIndicator.transform.gameObject.activeInHierarchy && !objectSpawner.transform.gameObject.activeInHierarchy){
                    fishingUI.PutMessage("Wait a few seconds until see the image on the ground and tap the screen to set the fishing hole");
                    placementIndicator.transform.gameObject.SetActive(true);
                    objectSpawner.transform.gameObject.SetActive(true);
                }

                if(objectSpawner.isOnScene){
                    fishingHole = Setter.transform.GetChild(2).GetComponent<FishingHole>();

                    placementIndicator.transform.gameObject.SetActive(false);
                    objectSpawner.transform.gameObject.SetActive(false);
                    fishingUI.clearNotification();
                    fishingPhase = FishingPhase.Casting;
                }
            break;

            //Launch the hook
            case FishingPhase.Casting:
                if(!castingManager.gameObject.activeInHierarchy){
                    fishingUI.PutMessage("Throw the hook into the fishing hole, swipe the screen to cast");
                    castingManager.gameObject.SetActive(true);
                    castingManager.Start();
                }
                else if(castingManager.isOnTheHole){
                    activeHook = castingManager.activeHook;
                    activeHook.GetComponent<SwipeDetector>().enabled = false;
                    castingManager.gameObject.SetActive(false);
                    fishingPhase = FishingPhase.Waiting;
                }
            break;

            //Wait
            case FishingPhase.Waiting:
                if(!waitingManager.gameObject.activeInHierarchy){
                    fishingUI.PutMessage("Wait a bit until the fish bites the hook");
                    waitingManager.gameObject.SetActive(true);
                    waitingManager.Start();
                }
                else if(waitingManager.fishOntheHook){
                    waitingManager.gameObject.SetActive(false);
                    fishingPhase = FishingPhase.Reeling;
                }
            break;

            //Reeling
            case FishingPhase.Reeling:
                if(!reelingManager.gameObject.activeInHierarchy){
                    fishingUI.PutMessage("A FISH HAS BITTEN THE HOOK!!!!!");
                    reelingManager.gameObject.SetActive(true);
                    reelingManager.Start();
                    fishingUI.LoadReelingElements();
                }
                else{
                    if(reelingManager.reelingState == ReelingState.Captured){
                        hookedFish = reelingManager.HookedFish;
                        caugthFish = reelingManager.HookedFish;
                        fishingUI.PutMessage("You have caugth a " + caugthFish.name);
                        fishingUI.HideReelingElements();
                        reelingManager.gameObject.SetActive(false);
                        fishingPhase = FishingPhase.Visualazing;
                    }

                    if(reelingManager.reelingState == ReelingState.FishHasGone){
                        fishingUI.PutMessage("Ups, your fish has escaped");
                        fishingUI.HideReelingElements();
                        fishingPhase = FishingPhase.Casting;
                        reelingManager.gameObject.SetActive(false);
                    }

                    if(reelingManager.reelingState == ReelingState.BrokenLine){
                        fishingUI.PutMessage("Ups, the line is broken :c, waht a strong fish");
                        fishingUI.HideReelingElements();
                        fishingPhase = FishingPhase.Casting;
                        reelingManager.gameObject.SetActive(false);
                    }
                }
            break;

            //Visualazing
            case FishingPhase.Visualazing:
                if(!visualizingManager.gameObject.activeInHierarchy){
                    fishingUI.PutMessage("Stats");
                    visualizingManager.gameObject.SetActive(true);
                    visualizingManager.Start();
                    fishingUI.LoadVisualizingElements(hookedFish);
                }
                if(visualizingManager.clicOnClose){
                    fishingUI.HideVisualizingElements();
                    Destroy(activeHook.gameObject);
                    visualizingManager.gameObject.SetActive(false);
                    fishingPhase = FishingPhase.Casting;
                }
            break;

            default:
                Debug.Log("No phase");
            break;
        }
    }
}

public enum FishingPhase{
    Placement, Casting, Waiting, Reeling, Visualazing
}
