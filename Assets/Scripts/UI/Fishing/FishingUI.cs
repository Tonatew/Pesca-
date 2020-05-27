using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishingUI : MonoBehaviour
{
    [SerializeField] TMP_Text notificationText;   

    //Fishing Manager
    [SerializeField] FishingManager fishingManager;

    //Reeling
    [SerializeField] ReelingManager reeling;
    [SerializeField] GameObject DistancePanel;
    [SerializeField] GameObject TensionPanel;
    [SerializeField] Slider DistanceSlider;
    [SerializeField] Slider TensionSlider;
    [SerializeField] Button PullButton;
    [SerializeField] Button ReelButton;
    [SerializeField] Button ReleaseButton;

    //Visualizing
    [SerializeField] GameObject visualizingPanel;
    [SerializeField] TMP_Text fishName;
    [SerializeField] Image CapturedfishImage;
    [SerializeField] Slider fishMass;
    [SerializeField] Slider Strength;
    [SerializeField] Slider Stamina;
    [SerializeField] TMP_Text DescriptionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        switch (fishingManager.fishingPhase){
            case FishingPhase.Reeling:
                if(!TensionPanel && !DistancePanel){
                    TensionPanel.SetActive(true);
                    DistancePanel.SetActive(true);
                }
                else{
                    DistanceSlider.value = reeling.distance/400;
                    TensionSlider.value = reeling.tension/GameMaster.selectedRod.line.maxTension;
                }
            break;

            default:
            break;
        }
        
    }

    public void LoadReelingElements(){
        TensionPanel.SetActive(true);
        DistancePanel.SetActive(true);
        PullButton.gameObject.SetActive(true);
        ReelButton.gameObject.SetActive(true);
        ReleaseButton.gameObject.SetActive(true);

    }

    public void HideReelingElements(){
        TensionPanel.SetActive(false);
        DistancePanel.SetActive(false);
        PullButton.gameObject.SetActive(false);
        ReelButton.gameObject.SetActive(false);
        ReleaseButton.gameObject.SetActive(false);
    }

    //Visualizing Load - Hide
    public void LoadVisualizingElements(Fish fish){
        visualizingPanel.SetActive(true);

        string path = "Images/" + fish.name;
        CapturedfishImage.sprite = Resources.Load<Sprite>(path); //Pendiente
        fishMass.value = fish.mass/40;
        Stamina.value = fish.stamina/10f;
        Strength.value = fish.stamina;

        fishName.text = fish.name;
        DescriptionText.text = fish.description;
        Debug.Log(fish.description);
    }

    public void HideVisualizingElements(){
        visualizingPanel.SetActive(false);
    }

    public void clearNotification(){
        notificationText.text = "";
    }

    public void PutMessage(string message){
        notificationText.text = message;
    }

    public void ReturnMenu(){
        SceneManager.LoadScene("Menu");
    }

}
