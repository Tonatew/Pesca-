using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour{
    [SerializeField]
    ARRaycastManager raycastManager;
    [SerializeField]
    GameObject placeHolderPlane;

    private void Start() {
        placeHolderPlane.SetActive(false);

    }

    private void Update() {
        //Shot a Raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width/2, Screen.height/2), hits, TrackableType.Planes);

        //If hits a plane
        if(hits.Count > 0){
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            //Fix the 180° rotation
            transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }
        
        if(!placeHolderPlane.activeInHierarchy){
            placeHolderPlane.SetActive(true);
        }
    }
}
