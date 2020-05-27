using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //Prefab
    [SerializeField]
    public GameObject fishingHole;

    public GameObject activeFishingHole;
    [SerializeField]
    public bool isOnScene;
    [SerializeField] PlacementIndicator placementIndicator;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start(){
        isOnScene = false;
        musicAudioSource.clip = backgroundMusic;
        musicAudioSource.Play();
    }

    // Update is called once per frame
    void Update(){
        //if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
        if (Input.GetMouseButtonUp(0)){
            GameObject obj = Instantiate(fishingHole, placementIndicator.transform.position, placementIndicator.transform.rotation);
            obj.transform.SetParent(this.transform.parent.transform);
            isOnScene = true;
        }
    }
}
