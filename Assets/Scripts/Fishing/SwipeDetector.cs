using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    Vector2 startPos, endPos, screenSwapDirection;
    float touchTimeStart, touchTimeFinish, timeInterval;
    [SerializeField]
    float timeOut;
    bool isOnAir;
    public bool isOnTheHole;

    public float throwForce;

    [SerializeField]
    FishingUI fishingUI;
    public Animator fishingRodAnimator;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start(){
        isOnTheHole = false;
        isOnAir = false;
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        fishingUI = GameObject.FindObjectOfType<FishingUI>();
    }

    // Update is called once per frame
    void Update(){
        //Start Touch
        //if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
        if (Input.GetMouseButtonDown(0)){
            touchTimeStart = Time.time;
            //startPos = Input.GetTouch (0).position;
            startPos = Input.mousePosition;
        }

        //End Touch
        //if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
        if (Input.GetMouseButtonUp(0)){
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            //endPos = Input.GetTouch (0).position;
            endPos = Input.mousePosition;
            screenSwapDirection = endPos - startPos;
            
            //2D to 3D
            Vector3 WorldDirection = new Vector3(screenSwapDirection.x, 0.0f, screenSwapDirection.y);
            WorldDirection = Quaternion.Euler(new Vector3(-45f, 0f, 0f)) * WorldDirection;
            if(Input.deviceOrientation == DeviceOrientation.LandscapeRight){
                WorldDirection = Quaternion.Euler(new Vector3(-180f, 0f, 0f)) * WorldDirection;
                WorldDirection *= -1f;
                WorldDirection.Scale(new Vector3(-1f, 1, 1));
            }
            WorldDirection.Normalize();
            //Launching
            rb.useGravity = true;
            fishingRodAnimator.Play("Armature|launching");
            GetComponent<Rigidbody>().AddRelativeForce (WorldDirection / timeInterval * throwForce);
            isOnAir = true;
        }

        //TimeOut
        if(isOnAir && Time.time - touchTimeFinish > timeOut){
            Destroy(this.gameObject);
            fishingUI.PutMessage("You failed");
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Hole"){
            this.transform.SetParent(other.transform);
            fishingUI.PutMessage("Hit");
            Destroy(rb);
            isOnAir = false;
            isOnTheHole = true;
        }
    }
}
