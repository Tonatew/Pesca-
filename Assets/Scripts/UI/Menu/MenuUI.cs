using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    //Region UI Elements
    Region ActiveRegion;
    LinkedListNode<Region> ActiveRegionNode;
    [SerializeField]
    Image regionImage;
    [SerializeField]
    Image regionNameImage;
    [SerializeField]
    TMP_Text regionDescription;
    //Fishing Rod UI Elements
    FishingRod activeFishingRod;
    LinkedListNode<FishingRod> activefishingRodNode;
    [SerializeField]
    Image fishingRodImage;
    [SerializeField]
    Image fishingRodName;
    [SerializeField]
    TMP_Text fishingRodDescription;

    //Awake is called after the object creation
    private void Awake() {
        GameMaster.LoadUser();
        ActiveRegionNode = GameMaster.player.unlockedRegions.First;
        ActiveRegion = ActiveRegionNode.Value;
        
        activefishingRodNode = GameMaster.player.unlockedRods.First;
        activeFishingRod = activefishingRodNode.Value;
        Debug.Log(ActiveRegion.name);
    }

    // Start is called before the first frame update
    void Start(){
        LoadRegionPanel(ActiveRegion);
        LoadFishingRodPanel(activeFishingRod);
    }

    void LoadRegionPanel(Region region){
        string path;

        path = "Images/" + region.name + "RegionImage";
        regionImage.sprite = Resources.Load<Sprite>(path);

        path = "Images/" + region.name + "TextImage";
        regionNameImage.sprite = Resources.Load<Sprite>(path);

        regionDescription.text = ActiveRegion.description;
    }

    void LoadFishingRodPanel(FishingRod fishingRod){
        string path;

        path = "Images/" + activeFishingRod.name;
        fishingRodImage.sprite = Resources.Load<Sprite>(path);

        path = "Images/" + activeFishingRod.name + " Name";
        fishingRodName.sprite = Resources.Load<Sprite>(path);

        fishingRodDescription.text = "Max tension Supported: \n" + activeFishingRod.line.maxTension;
    }
    
    //----------------------------     Buttons     --------------------------
    public void ClickOnPlay(){
        GameMaster.selectedRegion = ActiveRegion;
        GameMaster.selectedRod = activeFishingRod;
        SceneManager.LoadScene("FishingAR");
    }

    public void LoadNextUnlockedRegion(){
        ActiveRegionNode = ActiveRegionNode.Next;
        if(ActiveRegionNode == null)
            ActiveRegionNode = GameMaster.player.unlockedRegions.First;
        ActiveRegion = ActiveRegionNode.Value;
        LoadRegionPanel(ActiveRegion);
    }

    public void LoadPreviousUnlockedRegion(){
        ActiveRegionNode = ActiveRegionNode.Previous;
        if(ActiveRegionNode == null)
            ActiveRegionNode = GameMaster.player.unlockedRegions.Last;
        ActiveRegion = ActiveRegionNode.Value;
        LoadRegionPanel(ActiveRegion);
    }

    public void LoadNextUnlockedFishingRod(){
        activefishingRodNode = activefishingRodNode.Next;
        if(activefishingRodNode == null)
            activefishingRodNode = GameMaster.player.unlockedRods.First;
        activeFishingRod = activefishingRodNode.Value;
        LoadRegionPanel(ActiveRegion);
    }

    public void LoadPreviousUnlockedFishingRod(){
        activefishingRodNode = activefishingRodNode.Previous;
        if(activefishingRodNode == null)
            activefishingRodNode = GameMaster.player.unlockedRods.Last;
        activeFishingRod = activefishingRodNode.Value;
        LoadFishingRodPanel(activeFishingRod);
    }
}
